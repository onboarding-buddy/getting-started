from onboarding_buddy_client import Configuration, ApiClient
from onboarding_buddy_client.api.file_api import FileApi
from onboarding_buddy_client.models.generate_video_request_m import GenerateVideoRequestM
from onboarding_buddy_client.models.generate_image_request_m import GenerateImageRequestM
from dotenv import load_dotenv

import os
from pathlib import Path
import mimetypes
from datetime import datetime
import time
import uuid

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

# Generate Video
try:
    video_prompt = "Create a cinematic scene based on the following. **Droid Concept:** A Little Droid that comes to life. **Quirky Characteristics:** This droid is activated by pushing its nose. Once activated, it sprouts arms and legs and begins marching around uncontrollably, knocking things over with manic energy. It only reverts to its dormant state when its nose is pushed again. **Personality:** It has a chaotic, disruptive personality. **Prompt Details for Video Generation:***   **Subject:** A Little Droid*   **Action:** Being activated and marching around with jerky movements, knocking over various small objects in a cluttered junk shop.*   **Setting:** A cluttered, dusty junk shop. **Lighting:** Bright, slightly harsh lighting.  **Additional Details:** The droid should have a simple, almost toy-like design, with mismatched parts. **Camera movement:** Quick pan to the droid being activated, then shaky cam following its chaotic movements, ending with a snap zoom as it causes a small collision."

    generateVideoRequest = GenerateVideoRequestM(
        prompt=video_prompt,
        idempotency_key=str(uuid.uuid4())
    )
    generateVideoResponse = file_api.generate_video(generateVideoRequest)
    
    print("\n[1] Generate video with prompt:")
    print("---------------------------")
    print(video_prompt)

    print("\n[1] poll until generation is complete")
    print("---------------------------------------")
    print("\nThis can take between 60-120 secs")
    print("\nPlease wait whilst video, tags, title, description are created")

    pollFileResponse = file_api.poll_file(generateVideoResponse.file_global_id)

    while(pollFileResponse.file_status != "PROCESSED" and pollFileResponse.file_status != "FAILED"):
        print("Awaiting file generation.....")
        time.sleep(8)
        pollFileResponse = file_api.poll_file(generateVideoResponse.file_global_id)

    if pollFileResponse.file_status == "PROCESSED":
        print(f"\n[2] Download FileId {generateVideoResponse.file_global_id}")
        print("-------------------------------------------------------------")
        downloadResponse = file_api.download(generateVideoResponse.file_global_id)
        print(f"\n[2] download response")
        print(f"-----------------")
        print(f"pre_signed_url: {downloadResponse.pre_signed_url}")
    else:
        print(f"\nprocessing failed please try again")

except Exception as e:
    print(f"Error: {e}")   

# Generate Image
try:
    image_prompt = "Create a vibrant, pop-art style image inspired by Andy Warhol, featuring the Eiffel Tower. Use bold, contrasting colors like neon pink, electric blue, bright yellow, and lime green in a grid of four or more panels, each with a different color scheme. Emphasize flat, simplified shapes and high contrast, mimicking Warhol's iconic silkscreen aesthetic. Keep the Eiffel Tower's recognizable silhouette central in each panel, with minimal background details to highlight the structure. Ensure the overall composition feels repetitive yet varied, capturing the essence of Warhol’s repetitive pop-art style."

    generateImageRequest = GenerateImageRequestM(
        prompt=image_prompt,
        idempotency_key=str(uuid.uuid4())
    )
    generateImageResponse = file_api.generate_image(generateImageRequest)
    
    print("\n[3] Generate image with prompt")
    print("---------------------------")
    print(image_prompt)

    print("\n[3] poll until generation is complete")
    print("---------------------------------")
    print("\nThis can take between 30-60 secs")
    print("\nPlease wait whilst image, tags, title, description and embeddings are created")
    pollFileResponse = file_api.poll_file(generateImageResponse.file_global_id)

    while(pollFileResponse.file_status != "PROCESSED" and pollFileResponse.file_status != "FAILED"):
        print("Awaiting file generation.....")
        time.sleep(8)
        pollFileResponse = file_api.poll_file(generateImageResponse.file_global_id)

    if pollFileResponse.file_status == "PROCESSED":
        print(f"\n[4] Download FileId {generateImageResponse.file_global_id}")
        print("-------------------------------------------------------------")
        downloadResponse = file_api.download(generateImageResponse.file_global_id)
        print(f"\n[4] download response")
        print(f"-----------------")
        print(f"pre_signed_url: {downloadResponse.pre_signed_url}")
    else:
        print(f"\nprocessing failed please try again")

except Exception as e:
    print(f"Error: {e}")    

