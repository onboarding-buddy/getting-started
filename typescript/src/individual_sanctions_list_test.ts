import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const individualSanctions = client.checkIndividualSanctions({
  firstName: 'YEVGENIY',
  lastName: 'PRIGOZHIN',
  birthYear: '1961',
});

individualSanctions
    .then(response => 
        { 
            console.log(`Matched: ${response.data.matched}`)

            if (response.data.results)
            {
                console.log(`Full Name: ${response.data.results[0].fullName}`)
                console.log(`First Name: ${response.data.results[0].firstName}`)
                console.log(`Last Name: ${response.data.results[0].lastName}`)
                console.log(`Program: ${response.data.results[0].program}`)
                console.log(`Additional Information: ${response.data.results[0].additionalInfo}`)
                console.log(`Date Of Birth: ${response.data.results[0].dateOfBirth}`)
                console.log(`Birth Year: ${response.data.results[0].birthYear}`)
                console.log(`Nationality: ${response.data.results[0].nationality}`)
                console.log(`Gender: ${response.data.results[0].gender}`)
                console.log(`Linked To: ${response.data.results[0].linkedTo}`)
                console.log(`Secondary Sanctions Risk: ${response.data.results[0].secondarySanctionsRisk}`)
            }
        })
    .catch(error=> console.log(error.response.data));
