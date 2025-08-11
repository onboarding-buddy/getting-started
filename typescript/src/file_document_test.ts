import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
import * as path from 'path';
import { loadFile } from './loadFile';
import { SearchFileRequestM, RagQueryRequestM } from '@onboardingbuddy/onboardingbuddy-client/dist/client/api.js';
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const DOCUMENT_FILE = "T9_en.pdf";
const DOCUMENT_FILETYPEGROUPID = 4;
        
const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

async function runFileTests() {
    try {
        // 1 Upload file
        const filePath = path.join(__dirname, DOCUMENT_FILE);
        const file = await loadFile(filePath);

        let pdfFileGlobalId:string;
        console.log(`[1] starting upload for file: ${DOCUMENT_FILE}`);
        
        const uploadResponse = await client.fileUpload(file);

        pdfFileGlobalId = uploadResponse.data.globalId!;

        console.log("[1] upload response");
        console.log("------------------------");
        console.log(`pdfFileGlobalId: ${pdfFileGlobalId}`);

        // 2 Get File Records

        console.log("\n[2] Poll for completion of file processing")
        console.log("Please wait whilst tags, title, description and embeddings are created")
        console.log("This can take between 30-60 secs")
        console.log("----------------------------------")

        let fileRecordsResponse = await client.fileGetFileRecords();

        while(fileRecordsResponse.data.fileRecords!.some(u => u.fileStatus! == 'PROCESSING'))
        {
            console.log("Awaiting file processing.....")
            await new Promise(f => setTimeout(f, 5000))
            fileRecordsResponse = await client.fileGetFileRecords();
        }

        // 3 Get File Record(PDF)
        console.log("\n[3] Retrieve file")
        const fileRecordResponse = await client.fileGetFileRecord(pdfFileGlobalId!);

        console.log("[3] fileGetFileRecord response")
        console.log("------------------------")
        console.log(`fileRecordResponse: ${fileRecordResponse}`)

        // 4 Download(PDF)
        console.log("\n[4] download file")
        console.log("-----------------")
        const downloadResponse = await client.fileDownload(pdfFileGlobalId!);

        console.log("[4] download response")
        console.log("-----------------")
        console.log(`pre_signed_url: ${downloadResponse.data.preSignedUrl}`)

        // 5 Search Documents
        const searchString = "Methods for discovering exoplanets"
        const searchFileRequest:SearchFileRequestM = {
            searchString:searchString,
            fileTypeGroupId:DOCUMENT_FILETYPEGROUPID
        }

        console.log("\n[5] Searching documents with query")
        console.log("---------------------------")
        console.log(searchString)

        const searchFileResponse = await client.fileSearchFileRecords(searchFileRequest);

        console.log("[5] searchFileResponse response")
        console.log("------------------------")

        searchFileResponse.data.fileRecords!.forEach((fileRecord) => {
        console.log(fileRecord);
        });

        // 6 Document RAG
        const ragPrompt = "Summarise the methods that can be used for discovering exoplanets and their accuracy"
        const ragRequest:RagQueryRequestM = {
            fileGlobalId:pdfFileGlobalId!,
            searchString:searchString,
            fileTypeGroupId:DOCUMENT_FILETYPEGROUPID
        }

        console.log("[5] Performing a RAG Query for the following prompt")
        console.log("---------------------------")
        console.log(ragPrompt)

        const ragResponse = await client.fileDocumentRag(ragRequest);

        console.log("[5] RAG Response")
        console.log("----------------------------")
        console.log(ragResponse.data.generatedText!)
        
    } catch (error) {
        console.error("Error running file tests:", error);
    }
}

runFileTests();

