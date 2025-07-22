import { Configuration, ValidationApi } from '@onboardingbuddy/onboardingbuddy-client/dist/client';
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

//const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);
const config = new Configuration({
      baseOptions: {
        headers: {
            'ob-app-key': obAppKey,
            'ob-api-key': obApiKey,
            'ob-api-secret': obApiSecret,
        }
      },
    });
const validationApi = new ValidationApi(config);
    
// Email Validation
const emailValidation = validationApi.email({
  emailAddress: 'email@domain.com',
});

emailValidation
    .then(response => 
        { 
            console.log(`Email Address: ${response.data.emailAddress}`)
            console.log(`Email Status: ${response.data.emailStatus}`)
            console.log(`Free Email: ${response.data.freeEmail}`)
            console.log(`Domain: ${response.data.domain}`)
            console.log(`MX Record: ${response.data.mxRecord}`)
            console.log(`MX Found: ${response.data.mxFound}`)
            console.log(`Check Status: ${response.data.checkStatus}`)
        })
    .catch(error=> console.log(error.response.data));
