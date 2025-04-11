import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const entitySanctions = client.checkEntitySanctions({
  name: 'INTERNET RESEARCH AGENCY LLC'
});

entitySanctions
    .then(response => 
        { 
            console.log(`Matched: ${response.data.matched}`)
            if (response.data.match)
            {
                console.log(`Full Name: ${response.data.match[0].fullName}`)
                console.log(`Program: ${response.data.match[0].program}`)
                
                console.log(`\nAlias List:`)     
                
                if (response.data.match[0].aliasList)
                {
                    response.data.match[0].aliasList.forEach(x => {
                        console.log(x.fullName);
                    });
                }
                
                console.log(`\nAddress List:`)        
                if (response.data.match[0].addressList)
                {
                    response.data.match[0].addressList.forEach(y => {
                        console.log(`${y.address} ${y.cityStateProvincePostalCode} ${y.country}`);
                    });
                }
                
                console.log(`\nLinked Individuals:`)        
                if (response.data.match[0].linkedIndividuals)
                {
                    response.data.match[0].linkedIndividuals.forEach(z => {
                        console.log(z.fullName);
                    });
                }
    
                console.log(`\nSecondary Sanctions Risk: ${response.data.match[0].secondarySanctionsRisk}`)
            }
            
        })
    .catch(error=> console.log(error.response.data));
