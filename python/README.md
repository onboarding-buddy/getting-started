# Python SDK - Getting Started Instructions

## 1 Create an OnboardingBuddy account 

If you have not done so already, navigate to the Onboarding Buddy website at https://www.onboardingbuddy.co.  Proceed to register and create an account.

## 2 Get your API credentials

Navigate to your application by clicking "View" on the dashboard.  making note of the following:

On the "Details" Tab make a note of your application key:
```
APP_KEY (Application Key)
```

And on the "Access Tokens" Tab make a note of your Api Key and Api Secret:
```
API_KEY (Api Key)
API_SECRET (Api Secret)
```

## 3 Save credentials to environment variables

Next create the following environment variables:

Windows
```
setx OB_APP_KEY <APP_KEY>
setx OB_API_KEY <API_KEY>
setx OB_API_SECRET <API_SECRET>
```

Linux
```
export OB_APP_KEY='<APP_KEY>'
export OB_API_KEY='<API_KEY>'
export OB_API_SECRET='<API_SECRET>'
```

## 4 Activate python environment

This guide assumes the use of anaconda for running the python tests on your local machine.  For help installing and getting started <a href="https://www.anaconda.com/docs/getting-started/getting-started">click here</a>

To activate your python environment run the following:
```
conda create -n venv python=3.9
conda activate venv
```

## 5 Install dependancies

After cloning this repo to your local machine navigate to the python folder in the repo
```
cd python
```

### 5.1 Install the requirements.txt

To install the required depedencies run the following:

```
pip install -r requirements.txt
```

### 5.2 Install Onboarding Buddy SDK

To install the Onboarding Buddy SDK hosted in <a href="https://pypi.org/project/onboarding-buddy-client/">PyPi</a> run the following:

```
pip install onboarding-buddy-client
```

## 6 Run tests

### 6.1 Validation Tests

These code snippets from the validation_test.py file demonstrates how to validate email address, ip address, browser user agents and mobile numbers.

```python
# Create an email validation request
request = EmailAddressRequestM(
    email_address="email@domain.com"
)

print("\nEmail Validation Test")
print("---------------------")

response = validation_api.email(email_address_request_m=request)
```

```python
request = IpAddressRequestM(
    ip_address="46.182.106.190"
)

print("\nIP Address Test")
print("---------------------")

response = validation_api.ipaddress(ip_address_request_m=request)
```

```python
request = BrowserRequestM(
    user_agent="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36"
)

print("\nBrowser Test")
print("---------------------")

response = validation_api.browser(browser_request_m=request)

```

```python
# Create an mobile number validation request
request = MobileNumberRequestM(
    mobile_number=MobileNumberM(
        prefix="61",
        number="0422123456"
    )
)

print("\nMobile Number Test")
print("---------------------")

response = validation_api.mobile(mobile_number_request_m=request)
```
To run this code example use the command:

```
python validation_test.py
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation">here</a> 

### 6.2 Sanction Tests

These code snippets from the sanction_test.py file demonstrates how to submit a sanctions check for an individual, entity, aircraft, vessel and crypto wallet addresses.

```python
# Create an individual sanction request
request = IndividualSanctionsCheckRequestM(
    first_name="YEVGENIY",
    last_name="PRIGOZHIN",
    birth_year="1961"
)

print("\nIndividual Sanction Test")
print("--------------------------")

response = sanctions_api.individual(individual_sanctions_check_request_m=request)
```

```python
# Create an entity sanction request
request = EntitySanctionsCheckRequestM(
    name="INTERNET RESEARCH AGENCY LLC"
)

print("\nEntity Sanction Test")
print("----------------------")

response = sanctions_api.entity(entity_sanctions_check_request_m=request)

```

```python
# Create an aircraft sanction request
request = AircraftSanctionsCheckRequestM(
        name="RA-02791"
    )

    print("\nAircraft Sanction Test")
    print("------------------------")
    
    response = sanctions_api.aircraft(aircraft_sanctions_check_request_m=request)
```

```python
# Create a vessel sanction request
request = VesselSanctionsCheckRequestM(
    name="HWANG GUM SAN 2"
)

print("\nVessel Sanction Test")
print("----------------------")

response = sanctions_api.vessel(vessel_sanctions_check_request_m=request)

```

```python
# Create a crypto wallet sanction request
request = CryptoWalletSanctionsCheckRequestM(
    address="0X098B716B8AAF21512996DC57EB0615E2383E2F96"
)

print("\nCrypto Wallet Sanction Test")
print("-----------------------------")

response = sanctions_api.crypto_wallet(crypto_wallet_sanctions_check_request_m=request)

```

To run this code example use the command:

```
python sanction_test.py
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions">here</a> 

### 6.3 File Document Tests

These code snippets take you through the steps required to:

- upload a pdf file
- retrieve file record
- download a file
- perform a semantic search 
- perform a RAG query

```python
# Upload File
file = (file_like.name, file_like.content)
uploadResponse = file_api.upload(file)
pdf_file_global_id = uploadResponse.global_id
```

```python
# Get File Record 
fileRecordResponse = file_api.get_file_record(pdf_file_global_id)
```

```python
# Download a File
downloadResponse = file_api.download(pdf_file_global_id)   
```

```python
# Perform a semantic search
searchString = "Science Fiction movie scripts"
searchFileRequest = SearchFileRequestM(
    searchString=searchString,
    fileTypeGroupId=DOCUMENT_FILETYPEGROUPID
)
searchFileResponse = file_api.search_file_records(searchFileRequest)
    
```

```python
# Perform a RAG Query
rag_prompt = "Describe a quirky robot or droid from the document.  Return prompt details that can be used to generate a video with additional information about the droid.  Highlight personality and characteristics improvising where necessary"
ragRequest = RagQueryRequestM(
    file_global_id=pdf_file_global_id,
    searchString=rag_prompt,
    fileTypeGroupId=DOCUMENT_FILETYPEGROUPID
)

ragResponse = file_api.document_rag(ragRequest)
```

To run this code example use the command:

```
python file_document_test.py
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/file">here</a> 

### 6.4 File Image Tests

These code snippets take you through the steps required to:

- upload a jpg file
- retrieve file record
- download a file
- perform a semantic search 
- generate a video from image using VEO 2

```python
# Upload File
file = (file_like.name, file_like.content)
uploadResponse = file_api.upload(file)
file_global_id = uploadResponse.global_id
```

```python
# Get File Record 
fileRecordResponse = file_api.get_file_record(file_global_id)
```

```python
# Download a File
downloadResponse = file_api.download(file_global_id)
```

```python
#Perform a semantic search
searchString = "Italian Landmarks"
searchFileRequest = SearchFileRequestM(
    searchString=searchString,
    fileTypeGroupId=IMAGE_FILETYPEGROUPID
)
searchFileResponse = file_api.search_file_records(searchFileRequest)
```

```python
# Generate Video from Image
prompt = "Animate the clouds behind the colosseum"

generateVideoRequest = GenerateVideoRequestM(
    prompt=prompt,
    image_file_global_id=file_global_id,
    idempotency_key=str(uuid.uuid4())
)
generateVideoResponse = file_api.generate_video(generateVideoRequest)
```

To run this code example use the command:

```
python file_image_test.py
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/file">here</a>

### 6.5 File Document Tests

These code snippets take you through the steps required to:

- Generate a video using VEO 3
- Generate an image using Imagen

```python
# Generate a video using VEO 3
video_prompt = "Create a cinematic scene based on the following. **Droid Concept:** A Little Droid that comes to life. **Quirky Characteristics:** This droid is activated by pushing its nose. Once activated, it sprouts arms and legs and begins marching around uncontrollably, knocking things over with manic energy. It only reverts to its dormant state when its nose is pushed again. **Personality:** It has a chaotic, disruptive personality. **Prompt Details for Video Generation:***   **Subject:** A Little Droid*   **Action:** Being activated and marching around with jerky movements, knocking over various small objects in a cluttered junk shop.*   **Setting:** A cluttered, dusty junk shop. **Lighting:** Bright, slightly harsh lighting.  **Additional Details:** The droid should have a simple, almost toy-like design, with mismatched parts. **Camera movement:** Quick pan to the droid being activated, then shaky cam following its chaotic movements, ending with a snap zoom as it causes a small collision."

generateVideoRequest = GenerateVideoRequestM(
    prompt=video_prompt,
    idempotency_key=str(uuid.uuid4())
)

generateVideoResponse = file_api.generate_video(generateVideoRequest)
 ```

```python
# Generate an image using Imagen
image_prompt = "Create a vibrant, pop-art style image inspired by Andy Warhol, featuring the Eiffel Tower. Use bold, contrasting colors like neon pink, electric blue, bright yellow, and lime green in a grid of four or more panels, each with a different color scheme. Emphasize flat, simplified shapes and high contrast, mimicking Warhol's iconic silkscreen aesthetic. Keep the Eiffel Tower's recognizable silhouette central in each panel, with minimal background details to highlight the structure. Ensure the overall composition feels repetitive yet varied, capturing the essence of Warhol’s repetitive pop-art style."

generateImageRequest = GenerateImageRequestM(
    prompt=image_prompt,
    idempotency_key=str(uuid.uuid4())
)

generateImageResponse = file_api.generate_image(generateImageRequest)

```

To run this code example use the command:

```
python file_generation_test.py
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/file">here</a>