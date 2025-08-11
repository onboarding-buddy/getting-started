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

        const uploadResponse = await client.fileUpload(file);

        pdfFileGlobalId = uploadResponse.data.globalId!;

        console.log("upload response");
        console.log("------------------------");
        console.log(`pdfFileGlobalId: ${pdfFileGlobalId}`);

        // 2 Get File Records
        let fileRecordsResponse = await client.fileGetFileRecords();

        while(fileRecordsResponse.data.fileRecords!.some(u => u.fileStatus! == 'PROCESSING'))
        {
            console.log("Awaiting file processing.....")
            await new Promise(f => setTimeout(f, 5000))
            fileRecordsResponse = await client.fileGetFileRecords();
        }

        // 3 Get File Record(PDF)
        const fileRecordResponse = await client.fileGetFileRecord(pdfFileGlobalId!);

        console.log("fileRecordResponse response")
        console.log("------------------------")
        console.log(`fileRecordResponse: ${fileRecordResponse}`)

        // 4 Download(PDF)
        const downloadResponse = await client.fileDownload(pdfFileGlobalId!);

        console.log("download response")
        console.log("-----------------")
        console.log(`pre_signed_url: ${downloadResponse.data.preSignedUrl}`)

        // 5 Search Documents
        const searchString = "Methods for discovering exoplants"
        const searchFileRequest:SearchFileRequestM = {
            searchString:searchString,
            fileTypeGroupId:DOCUMENT_FILETYPEGROUPID
        }
        const searchFileResponse = await client.fileSearchFileRecords(searchFileRequest);

        console.log("Searching documents with query")
        console.log("---------------------------")
        console.log(searchString)
        console.log("searchFileResponse response")
        console.log("------------------------")

        searchFileResponse.data.fileRecords!.forEach((fileRecord) => {
        console.log(fileRecord);
        });

        // 6 Document RAG
        const ragPrompt = "Summarise the methods that can be used for discovering exoplants and their accuracy"
        const ragRequest:RagQueryRequestM = {
            fileGlobalId:pdfFileGlobalId!,
            searchString:searchString,
            fileTypeGroupId:DOCUMENT_FILETYPEGROUPID
        }
        const ragResponse = await client.fileDocumentRag(ragRequest);

        console.log("Performing a RAG Query for the following prompt")
        console.log("---------------------------")
        console.log(ragPrompt)
        console.log("RAG Response")
        console.log("----------------------------")
        console.log(ragResponse.data.generatedText!)
        
    } catch (error) {
        console.error("Error running file tests:", error);
    }
}

runFileTests();

