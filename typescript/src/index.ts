import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import {v4 as uuidv4} from 'uuid'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient('https://api.dev.onboardingbuddy.co/validation-service', obAppKey, obApiKey, obApiSecret);

const idempotencyKey = uuidv4();

// Email Validation - single
const emailValidation = client.validateEmail({
  emailAddress: 'test@example.com',
});

emailValidation
    .then(response => 
        { 
            console.log("email validation result");
            console.log(response.data);
        })
    .catch(error=> console.log(error.response.data));

// Email Validation - batch
const emailBatch = client.submitEmailAddressBatch({
  idempotencyKey: idempotencyKey.toString(),
  itemList: ['test@example.com'],
});

emailBatch
    .then(response => 
        { 
            if (response && response.data)
            {
                console.log("email validation batch response");
                console.log(response.data);
            }
            else{
                console.log(response);
            }
        })
    .catch(error=> console.log(error.response.data));

