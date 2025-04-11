import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const vesselSanctions = client.checkVesselSanctions({
  name: 'HWANG GUM SAN 2'
});

vesselSanctions
    .then(response => 
        { 
            console.log(`Matched: ${response.data.matched}`)
            if (response.data.match)
            {
                console.log(`Full Name: ${response.data.match[0].fullName}`)
                console.log(`Program: ${response.data.match[0].program}`)
                console.log(`Additional Info: ${response.data.match[0].additionalInfo}`)
                console.log(`Secondary Sanctions Risk: ${response.data.match[0].secondarySanctionsRisk}`)
                console.log(`Vessel Type: ${response.data.match[0].vesselType}`)
                console.log(`Vessel Flag: ${response.data.match[0].vesselFlag}`)
            }
        })
    .catch(error=> console.log(error.response.data));
