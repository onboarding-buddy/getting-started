# Typescript SDK - Getting Started Instructions

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

## 4 Install dependancies

After cloning this repo to your local machine navigate to the typescript folder in the repo
```
cd typescript
```

### 4.1 Install default packages
Run the following to install test dependencies

```
npm install
```

### 4.2 Install Onboarding Buddy SDK

To install the Onboarding Buddy SDK hosted in <a href="https://www.npmjs.com/package/@onboardingbuddy/onboardingbuddy-client/">NPM</a> run the following:

```
npm install @onboardingbuddy/onboardingbuddy-client
```

## 5 Run tests

### 5.1 Validation Tests

These code snippets from the validation_test.ts file demonstrates how to validate email address, ip address, browser user agents and mobile numbers.

```typescript
// Email Validation
const emailValidation = client.validateEmail({
  emailAddress: 'email@domain.com',
});

emailValidation
    .then(response => 
        { 
            .....
        }
```

```typescript
// IP Address Validation
const ipAddressValidation = client.validateIpAddress({
  ipAddress: '46.182.106.190',
});

ipAddressValidation
    .then(response => 
        { 
            ...
        }
```


```typescript
// Browser User Agent
const browserValidation = client.validateBrowser({
  userAgent: 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36',
});

browserValidation
    .then(response => 
        { 
            ...
        }
```

```typescript
// Mobile Numbers
const mobileNumberValidation = client.validateMobile({
  mobileNumber: {
     prefix:'61',
     number:'0422123456'
  }
});

mobileNumberValidation
    .then(response => 
        { 
            ...
        }
```
To run this code example use the command:

```
npx ts-node src/validation_test.ts
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation">here</a> 

### 5.2 Sanction Tests

These code snippets from the sanction_test.ts file demonstrates how to submit a sanctions check for an individual, entity, aircraft, vessel and crypto wallet addresses.

```typescript
const individualSanctions = client.checkIndividualSanctions({
  firstName: 'YEVGENIY',
  lastName: 'PRIGOZHIN',
  birthYear: '1961',
});

individualSanctions
    .then(response => 
        { 
            ...
        }
```

```typescript
const entitySanctions = client.checkEntitySanctions({
  name: 'INTERNET RESEARCH AGENCY LLC'
});

entitySanctions
    .then(response => 
        { 
            ...
        }
```

```typescript
const aircraftSanctions = client.checkAircraftSanctions({
  name: 'RA-02791'
});

aircraftSanctions
    .then(response => 
        { 
            ...
        }
```

```typescript
const vesselSanctions = client.checkVesselSanctions({
  name: 'HWANG GUM SAN 2'
});

vesselSanctions
    .then(response => 
        { 
            ...
        }
```

```typescript
const cryptoWalletSanctions = client.checkCryptoWalletSanctions({
  address: '0X098B716B8AAF21512996DC57EB0615E2383E2F96'
});

cryptoWalletSanctions
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/sanction_test.ts
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions">here</a> 

### 5.3 File Document Tests

These code snippets take you through the steps required to:

- upload a pdf file
- retrieve file record
- download a file
- perform a semantic search 
- perform a RAG query

```typescript
// Upload File
const uploadResponse = await client.fileUpload(file);
```

```typescript
// Get File Record 
const fileRecordResponse = await client.fileGetFileRecord(pdfFileGlobalId!);
```

```typescript
// Download a File
const downloadResponse = await client.fileDownload(pdfFileGlobalId!);
```

```typescript
// Perform a semantic search
const searchString = "Methods for discovering exoplants"
const searchFileRequest:SearchFileRequestM = {
    searchString:searchString,
    fileTypeGroupId:DOCUMENT_FILETYPEGROUPID
}
const searchFileResponse = await client.fileSearchFileRecords(searchFileRequest);
```

```typescript
// Perform a RAG Query
const ragPrompt = "Summarise the methods that can be used for discovering exoplanets and their accuracy"
const ragRequest:RagQueryRequestM = {
    fileGlobalId:pdfFileGlobalId!,
    searchString:searchString,
    fileTypeGroupId:DOCUMENT_FILETYPEGROUPID
}
const ragResponse = await client.fileDocumentRag(ragRequest);
```

To run this code example use the command:

```
npx ts-node src/file_document_test.ts
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/file">here</a> 

### 5.4 File Image Tests

These code snippets take you through the steps required to:

- upload a jpg file
- retrieve file record
- download a file
- perform a semantic search 
- generate a video from image using VEO 2

```typescript
// Upload File
const uploadResponse = await client.fileUpload(file);
```

```typescript
// Get File Record 
const fileRecordResponse = await client.fileGetFileRecord(imageFileGlobalId!);
```

```typescript
// Download a File
const downloadResponse = await client.fileDownload(imageFileGlobalId!);
```

```typescript
// Perform a semantic search
const searchString = "Parisian Landmarks"
const searchFileRequest:SearchFileRequestM = {
    searchString:searchString,
    fileTypeGroupId:IMAGE_FILETYPEGROUPID
}
const searchFileResponse = await client.fileSearchFileRecords(searchFileRequest);
```

```typescript
// Generate Video from Image
const videoPrompt = `Animate the sky and traffic around the arc de triomphe`
let idempotencyKey: string = uuidv4();

const generateVideoRequest:GenerateVideoRequestM= {
    prompt:videoPrompt,
    imageFileGlobalId:imageFileGlobalId,
    idempotencyKey: idempotencyKey
}

const generateVideoResponse = await client.fileGenerateVideo(generateVideoRequest);
```

To run this code example use the command:

```
npx ts-node src/file_image_test.ts
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/file">here</a>

### 5.5 File Generation Tests

These code snippets take you through the steps required to:

- Generate a video using VEO 3
- Generate an image using Imagen

```typescript
// Generate a video using VEO 3
const videoPrompt = "Create a cinematic scene based on the following. **Droid Concept:** A Little Droid that comes to life. **Quirky Characteristics:** This droid is activated by pushing its nose. Once activated, it sprouts arms and legs and begins marching around uncontrollably, knocking things over with manic energy. It only reverts to its dormant state when its nose is pushed again. **Personality:** It has a chaotic, disruptive personality. **Prompt Details for Video Generation:***   **Subject:** A Little Droid*   **Action:** Being activated and marching around with jerky movements, knocking over various small objects in a cluttered junk shop.*   **Setting:** A cluttered but futuristic junk shop. **Lighting:** Bright, slightly harsh tron like lighting.  **Additional Details:** The droid should have a simple, almost toy-like design, with mismatched parts. **Camera movement:** Quick pan to the droid being activated, then shaky cam following its chaotic movements, ending with a snap zoom as it causes a small collision."
let idempotencyKey: string = uuidv4();

const generateVideoRequest:GenerateVideoRequestM= {
    prompt:videoPrompt,
    idempotencyKey: idempotencyKey
}

const generateVideoResponse = await client.fileGenerateVideo(generateVideoRequest);
```

```typescript
// Generate an image using Imagen
const imagePrompt = "Create an artwork depicting the Eiffel Tower in the style of Picasso"
idempotencyKey = uuidv4();

const generateImageRequest:GenerateImageRequestM= {
    prompt:imagePrompt,
    idempotencyKey: idempotencyKey
}

const generateImageResponse = await client.fileGenerateImage(generateImageRequest);

```

To run this code example use the command:

```
npx ts-node src/file_generation_test.ts
```

For further information about these api actions you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/file">here</a>