import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const aircraftSanctions = client.checkAircraftSanctions({
  name: 'RA-02791'
});

aircraftSanctions
    .then(response => 
        { 
            console.log(`Matched: ${response.data.matched}`)
            if (response.data.match)
            {
                console.log(`Full Name: ${response.data.match[0].fullName}`)
                console.log(`Program: ${response.data.match[0].program}`)
                console.log(`Manufacture Date: ${response.data.match[0].manufactureDate}`)
                console.log(`Model: ${response.data.match[0].model}`)
                console.log(`Operator: ${response.data.match[0].operator}`)
                console.log(`Mode S Transponder Code: ${response.data.match[0].modeSTransponderCode}`)
                console.log(`Serial Identification: ${response.data.match[0].serialIdentification}`)
                console.log(`Linked To: ${response.data.match[0].linkedTo}`)
                if (response.data.match[0].aliasList)
                {
                    console.log(`Alias: ${response.data.match[0].aliasList[0].fullName}`)
                }
            }
        })
    .catch(error=> console.log(error.response.data));
