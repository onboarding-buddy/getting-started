import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

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
