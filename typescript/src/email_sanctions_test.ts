import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const emailValidation = client.validateEmail({
  emailAddress: 'AFRICONLINE@PROTONMAIL.COM',
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

            if (response.data.sanctionRecord?.entityMatch)
            {
                console.log(`Entity Name: ${response.data.sanctionRecord.entityMatch[0].fullName}`)
                console.log(`Program: ${response.data.sanctionRecord.entityMatch[0].program}`)
                console.log(`Secondary Sanctions Risk: ${response.data.sanctionRecord.entityMatch[0].secondarySanctionsRisk}`)

                console.log(`\nAlias List:`)        
                if (response.data.sanctionRecord.entityMatch[0].alsoKnownAs)
                {
                    response.data.sanctionRecord.entityMatch[0].alsoKnownAs.forEach(y => {
                        console.log(y);
                    });
                }

                console.log(`\nLinked Individuals:`)
                if (response.data.sanctionRecord.entityMatch[0].linkedIndividuals)
                {
                    response.data.sanctionRecord.entityMatch[0].linkedIndividuals.forEach(z => {
                        console.log(`Full Name: ${z.fullName}`)
                        console.log(`Linked To: ${z.linkedTo}`)
                    });
                }
                
            }
        })
    .catch(error=> console.log(error.response.data));
