from onboarding_buddy_client import Configuration, ApiClient
from onboarding_buddy_client.api.file_api import FileApi
from onboarding_buddy_client.models.search_file_request_m import SearchFileRequestM
from onboarding_buddy_client.models.generate_video_request_m import GenerateVideoRequestM

from dotenv import load_dotenv

import os
from pathlib import Path
import mimetypes
from datetime import datetime
import time
import uuid

class FileLike:
    def __init__(self, file_path: str, mime_type: str = "application/octet-stream"):
        self.file_path = Path(file_path)
        self.name = self.file_path.name
        self.type = mime_type
        with self.file_path.open("rb") as f:
            self.content = f.read()
        self.size = len(self.content)
        self.last_modified = int(self.file_path.stat().st_mtime * 1000)

def load_file(file_path: str) -> FileLike:
    mime_type, _ = mimetypes.guess_type(file_path)
    mime_type = mime_type or "application/octet-stream"
    try:
        return FileLike(file_path, mime_type)
    except FileNotFoundError:
        raise FileNotFoundError(f"File not found: {file_path}")
    except Exception as e:
        raise RuntimeError(f"Error loading file: {e}")

# Usage: Upload file using requests
load_dotenv()

ob_app_key = os.getenv("OB_APP_KEY")
ob_api_key = os.getenv("OB_API_KEY")
ob_api_secret = os.getenv("OB_API_SECRET")

# Configure the client with authentication headers
config = Configuration(
    host="https://api.dev.onboardingbuddy.co",  # From the servers section
    api_key={
        "AppKey": ob_app_key,
        "ApiKey": ob_api_key,
        "ApiSecret": ob_api_secret
    }
)
# Ensure no prefix is added to headers
config.api_key_prefix["AppKey"] = ""
config.api_key_prefix["ApiKey"] = ""
config.api_key_prefix["ApiSecret"] = ""

# Initialize the API client
api_client = ApiClient(configuration=config)
file_api = FileApi(api_client)

IMAGE_FILE = "colleseum.jpg"
IMAGE_FILETYPEGROUPID = 1

# Upload Image
file_path = os.path.join(os.getcwd(), IMAGE_FILE)
file_global_id = ""

try:
    file_like = load_file(file_path)
    
    print(f"starting upload for file (name={file_like.name}, type={file_like.type}, size={file_like.size})")
    
    # Prepare file tuple
    file = (file_like.name, file_like.content)
    uploadResponse = file_api.upload(file)
    file_global_id = uploadResponse.global_id

    print(f"upload response")
    print(f"------------------------")
    print(f"pdfFileGlobalId: {file_global_id}")
except Exception as e:
    print(f"Error: {e}")

# Get File Records
try:
    fileRecordsResponse = file_api.get_file_records()
    is_processing = any(x.file_status == "PROCESSING" for x in fileRecordsResponse.file_records)

    while(any(x.file_status == "PROCESSING" for x in fileRecordsResponse.file_records)):
        print("Awaiting file processing.....")
        time.sleep(5)
        fileRecordsResponse = file_api.get_file_records()

except Exception as e:
    print(f"Error: {e}")


# Get File Record
try:
    fileRecordResponse = file_api.get_file_record(file_global_id)
    
    print(f"get_file_record response")
    print(f"------------------------")
    print(f"fileRecordResponse: {fileRecordResponse}")
except Exception as e:
    print(f"Error: {e}")

# Download
try:
    downloadResponse = file_api.download(file_global_id)
    
    print(f"download response")
    print(f"-----------------")
    print(f"pre_signed_url: {downloadResponse.pre_signed_url}")
except Exception as e:
    print(f"Error: {e}")

# Semantic Search
try:
    searchString = "Italian Landmarks"
    searchFileRequest = SearchFileRequestM(
        searchString=searchString,
        fileTypeGroupId=IMAGE_FILETYPEGROUPID
    )
    searchFileResponse = file_api.search_file_records(searchFileRequest)
    
    print("Searching images with query")
    print("---------------------------")
    print(searchString)
    print("search_file_records response")
    print("----------------------------")

    for fileRecord in searchFileResponse.file_records:
        print(fileRecord)

except Exception as e:
    print(f"Error: {e}")

# Generate Video From Image
try:
    prompt = "Animate the clouds behind the colleseum"

    generateVideoRequest = GenerateVideoRequestM(
        prompt=prompt,
        image_file_global_id=file_global_id,
        idempotency_key=str(uuid.uuid4())
    )
    generateVideoResponse = file_api.generate_video(generateVideoRequest)
    
    print("Generate video for image with prompt")
    print("------------------------------------")
    print(prompt)

    print("poll until generation is complete")
    print("---------------------------------")
    
    pollFileResponse = file_api.poll_file(generateVideoResponse.file_global_id)

    while(pollFileResponse.file_status != "PROCESSED"):
        print("Awaiting file generation.....")
        time.sleep(8)
        pollFileResponse = file_api.poll_file(generateVideoResponse.file_global_id)

    downloadResponse = file_api.download(generateVideoResponse.file_global_id)
    
    print(f"download response")
    print(f"-----------------")
    print(f"pre_signed_url: {downloadResponse.pre_signed_url}")

except Exception as e:
    print(f"Error: {e}")    

