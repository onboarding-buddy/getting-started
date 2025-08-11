from onboarding_buddy_client import Configuration, ApiClient
from onboarding_buddy_client.api.file_api import FileApi
from onboarding_buddy_client.models.search_file_request_m import SearchFileRequestM
from onboarding_buddy_client.models.rag_query_request_m import RagQueryRequestM
from dotenv import load_dotenv

import os
from pathlib import Path
import mimetypes
from datetime import datetime
import time

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
    api_key={
        "AppKey": os.getenv("OB_APP_KEY"),
        "ApiKey": os.getenv("OB_API_KEY"),
        "ApiSecret": os.getenv("OB_API_SECRET")
    }
)

# Initialize the API client
api_client = ApiClient(configuration=config)
file_api = FileApi(api_client)

DOCUMENT_FILE = "Star_Wars_The_Phantom_Menace.pdf"
DOCUMENT_FILETYPEGROUPID = 4

# 1 Upload PDF Document
file_path = os.path.join(os.getcwd(), DOCUMENT_FILE)
pdf_file_global_id = ""

try:
    file_like = load_file(file_path)
    
    print(f"[1] starting upload for file (name={file_like.name}, type={file_like.type}, size={file_like.size})")
    
    # Prepare file tuple
    file = (file_like.name, file_like.content)
    uploadResponse = file_api.upload(file)
    pdf_file_global_id = uploadResponse.global_id

    print(f"\n[1] upload response")
    print(f"------------------------")
    print(f"pdfFileGlobalId: {pdf_file_global_id}")
except Exception as e:
    print(f"Error: {e}")

# 2 Get File Records
try:

    print(f"\n[2] Poll for completion of file processing")
    print(f"\nPlease wait whilst tags, title, description and embeddings are created")
    print(f"\nThis can take between 30-60 secs")
    print(f"----------------------------------")

    fileRecordsResponse = file_api.get_file_records()
    
    while(any(x.file_status == "PROCESSING" for x in fileRecordsResponse.file_records)):
        print("Awaiting file processing.....")
        time.sleep(8)
        fileRecordsResponse = file_api.get_file_records()

except Exception as e:
    print(f"Error: {e}")


# 3 Get File Record(PDF)
try:
    print(f"\n[3] Retrieve file for fileid {pdf_file_global_id}")
    print("--------------------------------------------------------------------------")
    
    fileRecordResponse = file_api.get_file_record(pdf_file_global_id)
    
    print(f"\n[3] get_file_record response")
    print(f"------------------------")
    print(f"fileRecordResponse: {fileRecordResponse}")
except Exception as e:
    print(f"Error: {e}")

# 4 Download(PDF)
try:

    print(f"\n[4] Download file for fileid {pdf_file_global_id}")
    print(f"--------------------------------------------------------------------")
    
    downloadResponse = file_api.download(pdf_file_global_id)
    
    print(f"\n[4] download response")
    print(f"-----------------")
    print(f"pre_signed_url: {downloadResponse.pre_signed_url}")
except Exception as e:
    print(f"Error: {e}")

# 5 Search Documents

try:
    searchString = "Science Fiction movie scripts"
    searchFileRequest = SearchFileRequestM(
        searchString=searchString,
        fileTypeGroupId=DOCUMENT_FILETYPEGROUPID
    )
  
    print("\n[5] Semantic search of documents for query:")
    print("---------------------------")
    print(searchString)
    
    searchFileResponse = file_api.search_file_records(searchFileRequest)
    
    print("\n[5] search_file_records response")
    print("----------------------------------")

    for fileRecord in searchFileResponse.file_records:
        print(fileRecord)

except Exception as e:
    print(f"Error: {e}")

# 6 Document RAG
try:
    rag_prompt = "Describe a quirky robot or droid from the document.  Return prompt details that can be used to generate a video with additional information about the droid.  Highlight personality and characteristics improvising where necessary"
    ragRequest = RagQueryRequestM(
        file_global_id=pdf_file_global_id,
        searchString=rag_prompt,
        fileTypeGroupId=DOCUMENT_FILETYPEGROUPID
    )

    print("\n[6] Performing a RAG Query for prompt")
    print("---------------------------")
    print(rag_prompt)

    ragResponse = file_api.document_rag(ragRequest)

    print("[6] RAG Response")
    print("----------------------------")
    print(ragResponse.generated_text)
       
except Exception as e:
    print(f"Error: {e}")    

