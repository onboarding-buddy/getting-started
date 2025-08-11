import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
import { v4 as uuidv4 } from 'uuid';
import { GenerateVideoRequestM, GenerateImageRequestM } from '@onboardingbuddy/onboardingbuddy-client/dist/client/api.js';
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const DOCUMENT_FILE = "T9_en.pdf";
const DOCUMENT_FILETYPEGROUPID = 4;
        
const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

async function runFileTests() {
    try {
        // 1 Generate Video 
        const videoPrompt = "Create a cinematic scene based on the following. **Droid Concept:** A Little Droid that comes to life. **Quirky Characteristics:** This droid is activated by pushing its nose. Once activated, it sprouts arms and legs and begins marching around uncontrollably, knocking things over with manic energy. It only reverts to its dormant state when its nose is pushed again. **Personality:** It has a chaotic, disruptive personality. **Prompt Details for Video Generation:***   **Subject:** A Little Droid*   **Action:** Being activated and marching around with jerky movements, knocking over various small objects in a cluttered junk shop.*   **Setting:** A cluttered but futuristic junk shop. **Lighting:** Bright, slightly harsh tron like lighting.  **Additional Details:** The droid should have a simple, almost toy-like design, with mismatched parts. **Camera movement:** Quick pan to the droid being activated, then shaky cam following its chaotic movements, ending with a snap zoom as it causes a small collision."
        let idempotencyKey: string = uuidv4();

        const generateVideoRequest:GenerateVideoRequestM= {
            prompt:videoPrompt,
            idempotencyKey: idempotencyKey
        }

        console.log("Generate video with prompt")
        console.log("---------------------------")
        console.log(videoPrompt)

        const generateVideoResponse = await client.fileGenerateVideo(generateVideoRequest);

        console.log("poll until generation is complete")
        console.log("---------------------------------")

        let pollFileResponse = await client.filePollFile(generateVideoResponse.data.fileGlobalId!)

        while(pollFileResponse.data.fileStatus != "PROCESSED")
        {
            console.log("Awaiting file generation.....")
            await new Promise(f => setTimeout(f, 8000))
            pollFileResponse = await client.filePollFile(generateVideoResponse.data.fileGlobalId!)
        }
    
        const downloadResponseGenVideo = await client.fileDownload(generateVideoResponse.data.fileGlobalId!)

        console.log("download response")
        console.log("-----------------")
        console.log(`pre_signed_url: ${downloadResponseGenVideo.data.preSignedUrl}`)

        // 8 Generate Image based on the RAG Response
        const imagePrompt = "Create an artwork depicting the Eiffel Tower in the style of Picasso"
        idempotencyKey = uuidv4();

        const generateImageRequest:GenerateImageRequestM= {
            prompt:imagePrompt,
            idempotencyKey: idempotencyKey
        }

        console.log("Generate image with prompt")
        console.log("---------------------------")
        console.log(imagePrompt)

        const generateImageResponse = await client.fileGenerateImage(generateImageRequest);

        console.log("poll until generation is complete")
        console.log("---------------------------------")

        pollFileResponse = await client.filePollFile(generateImageResponse.data.fileGlobalId!)

        while(pollFileResponse.data.fileStatus != "PROCESSED")
        {
            console.log("Awaiting file generation.....")
            await new Promise(f => setTimeout(f, 8000))
            pollFileResponse = await client.filePollFile(generateImageResponse.data.fileGlobalId!)
        }
    
        const downloadResponseGenImage = await client.fileDownload(generateImageResponse.data.fileGlobalId!)

        console.log("download response")
        console.log("-----------------")
        console.log(`pre_signed_url: ${downloadResponseGenImage.data.preSignedUrl}`)
    } catch (error) {
        console.error("Error running file tests:", error);
    }
}

runFileTests();

