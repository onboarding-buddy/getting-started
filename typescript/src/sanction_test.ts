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

    const cryptoWalletSanctions = client.checkCryptoWalletSanctions({
  address: '0X098B716B8AAF21512996DC57EB0615E2383E2F96'
});

cryptoWalletSanctions
    .then(response => 
        { 
            console.log(`Matched: ${response.data.matched}`)
            if (response.data.entityMatch)
            {
                console.log(`Entity Name: ${response.data.entityMatch[0].fullName}`)
                console.log(`Program: ${response.data.entityMatch[0].program}`)
                console.log(`Additional Info: ${response.data.entityMatch[0].additionalInfo}`)
                console.log(`Secondary Sanctions Risk: ${response.data.entityMatch[0].secondarySanctionsRisk}`)

                console.log(`\nAddress List:`)        
                if (response.data.entityMatch[0].addressList)
                {
                    response.data.entityMatch[0].addressList.forEach(x => {
                        console.log(`${x.address} ${x.cityStateProvincePostalCode} ${x.country}`);
                    });
                }
                
                console.log(`\nCurrency List:`)
                if (response.data.entityMatch[0].digitalCurrencyAddress)
                {
                    response.data.entityMatch[0].digitalCurrencyAddress.forEach(y => {
                        console.log(`${y.instrument}: ${y.address}`);
                    });
                }
                
                console.log(`\nAlias List:`)        
                if (response.data.entityMatch[0].alsoKnownAs)
                {
                    response.data.entityMatch[0].alsoKnownAs.forEach(z => {
                        console.log(z);
                    });
                }
            }
        })
    .catch(error=> console.log(error.response.data));
