import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const browserValidation = client.validateBrowser({
  userAgent: 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36',
});

browserValidation
    .then(response => 
        { 
            console.log(`User Agent: ${response.data.userAgent}`)
            console.log(`Simple Software: ${response.data.simpleSoftware}`)
            console.log(`Software: ${response.data.software}`)
            console.log(`Software Name: ${response.data.softwareName}`)
            console.log(`Operating System: ${response.data.operatingSystem}`)
            console.log(`Operating System Flavour: ${response.data.operatingSystemFlavour}`)
            console.log(`Operating System Version: ${response.data.operatingSystemVersion}`)
            console.log(`Is Abusive: ${response.data.isAbusive}`)
            console.log(`Check Status: ${response.data.checkStatus}`)
            console.log(`Message Id: ${response.data.messageId}`)
        })
    .catch(error=> console.log(error.response.data));
