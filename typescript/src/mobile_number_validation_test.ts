import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const mobileNumberValidation = client.validateMobile({
  mobileNumber: {
     prefix:'61',
     number:'0422123456'
  }
});

mobileNumberValidation
    .then(response => 
        { 
            console.log(`Mobile Number: ${response.data.mobileNumber}`)
            console.log(`Valid: ${response.data.valid}`)
            console.log(`Local Format: ${response.data.localFormat}`)
            console.log(`International Format: ${response.data.internationalFormat}`)
            console.log(`Country Prefix: ${response.data.countryPrefix}`)
            console.log(`Country Code: ${response.data.countryCode}`)
            console.log(`Carrier: ${response.data.carrier}`)
            console.log(`Line Type: ${response.data.lineType}`)
            console.log(`Check Status: ${response.data.checkStatus}`)
            console.log(`Message Id: ${response.data.messageId}`)
        })
    .catch(error=> console.log(error.response.data));
