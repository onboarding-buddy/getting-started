import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
import * as path from 'path';
import { loadFile } from './loadFile';
import { v4 as uuidv4 } from 'uuid';
import { SearchFileRequestM, GenerateVideoRequestM, GenerateImageRequestM } from '@onboardingbuddy/onboardingbuddy-client/dist/client/api.js';
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const IMAGE_FILE = "arc-de-triomphe.jpg";
const IMAGE_FILETYPEGROUPID = 1;
const POLL_WAIT = 8000;
        
const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

async function runFileTests() {
    try {
        // 1 Upload file
        const filePath = path.join(__dirname, IMAGE_FILE);
        const file = await loadFile(filePath);

        let imageFileGlobalId:string;
        console.log(`[1] starting upload for file: ${IMAGE_FILE}`);
        
        const uploadResponse = await client.fileUpload(file);

        if (uploadResponse.data.inError)
        {
            console.log("Upload image failed")
            console.log("---------------------------")
            console.log(`messageid: ${uploadResponse.data.messageId}`)
            console.log(uploadResponse.data.messages?.forEach(m=>{console.log(m)}))
            return
        }

        imageFileGlobalId = uploadResponse.data.globalId!;

        console.log("[1] upload response");
        console.log("------------------------");
        console.log(`imageFileGlobalId: ${imageFileGlobalId}`);

        // 2 Get File Records
        console.log("\n[2] Poll for completion of file processing")
        console.log("Please wait whilst tags, title, description and embeddings are created")
        console.log("This can take between 30-60 secs")
        console.log("----------------------------------")
        let fileRecordsResponse = await client.fileGetFileRecords();

        while(fileRecordsResponse.data.fileRecords!.some(u => u.fileStatus! == 'PROCESSING'))
        {
            console.log("Awaiting file processing.....")
            await new Promise(f => setTimeout(f, POLL_WAIT))
            fileRecordsResponse = await client.fileGetFileRecords();
        }

        // 3 Get File Record(Image)
        console.log("\n[3] Retrieve file")

        const fileRecordResponse = await client.fileGetFileRecord(imageFileGlobalId!);

        console.log("[3] fileGetFileRecord response")
        console.log("------------------------")
        console.log(`fileRecordResponse: ${fileRecordResponse}`)

        // 4 Download(Image)
        console.log("\n[4] download file")
        console.log("-----------------")
        const downloadResponse = await client.fileDownload(imageFileGlobalId!);

        console.log("[4] download response")
        console.log("-----------------")
        console.log(`pre_signed_url: ${downloadResponse.data.preSignedUrl}`)

        // 5 Search Documents
        const searchString = "Parisian Landmarks"
        const searchFileRequest:SearchFileRequestM = {
            searchString:searchString,
            fileTypeGroupId:IMAGE_FILETYPEGROUPID
        }
        const searchFileResponse = await client.fileSearchFileRecords(searchFileRequest);

        console.log("\n[5] Searching documents with query")
        console.log("---------------------------")
        console.log(searchString)
        console.log("searchFileResponse response")
        console.log("------------------------")

        searchFileResponse.data.fileRecords!.forEach((fileRecord) => {
            console.log(fileRecord);
        });

        // 7 Generate Video from Image
        const videoPrompt = `Animate the sky and traffic around the arc de triomphe`
        let idempotencyKey: string = uuidv4();

        const generateVideoRequest:GenerateVideoRequestM= {
            prompt:videoPrompt,
            imageFileGlobalId:imageFileGlobalId,
            idempotencyKey: idempotencyKey
        }

        console.log("\n[6] Generate video with prompt from existing image")
        console.log("---------------------------")
        console.log(videoPrompt)

        const generateVideoResponse = await client.fileGenerateVideo(generateVideoRequest);

        if (generateVideoResponse.data.inError)
        {
            console.log("Generate video failed")
            console.log("---------------------------")
            console.log(`messageid: ${generateVideoResponse.data.messageId}`)
            console.log(generateVideoResponse.data.messages?.forEach(m=>{console.log(m)}))
            return
        }

        console.log("Please wait whilst video, tags, title, description and embeddings are created")
        console.log("This can take between 60-90 secs")
        console.log("---------------------------------")

        let pollFileResponse = await client.filePollFile(generateVideoResponse.data.fileGlobalId!)

        while(pollFileResponse.data.fileStatus != "PROCESSED")
        {
            console.log("Awaiting file generation.....")
            await new Promise(f => setTimeout(f, POLL_WAIT))
            pollFileResponse = await client.filePollFile(generateVideoResponse.data.fileGlobalId!)
        }
    
        console.log("\n[7] download file")
        console.log("-----------------")
        const downloadResponseGenVideo = await client.fileDownload(generateVideoResponse.data.fileGlobalId!)

        console.log("[7] download response")
        console.log("-----------------")
        console.log(`pre_signed_url: ${downloadResponseGenVideo.data.preSignedUrl}`)

    } catch (error: unknown) {
        if (isAxiosError(error) && error.response) {
            const { status, data } = error.response;
            console.error(`Error status: ${status}, Data: ${JSON.stringify(data)}`);
        } else if (error instanceof Error) {
            console.error(`Generic error: ${error.message}`);
        } else {
            console.error('An unknown error occurred.');
        }
    }
}

function isAxiosError(error: unknown): error is import('axios').AxiosError {
  return (error as import('axios').AxiosError).isAxiosError !== undefined;
}

runFileTests();

