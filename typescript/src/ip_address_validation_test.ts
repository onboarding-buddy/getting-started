import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

const ipAddressValidation = client.validateIpAddress({
  ipAddress: '46.182.106.190',
});

ipAddressValidation
    .then(response => 
        { 
            console.log(`IP Address: ${response.data.ipAddress}`)
            console.log(`ISO Code: ${response.data.isoCode}`)
            console.log(`Country: ${response.data.country}`)
            console.log(`Threat: ${response.data.threat}`)
            console.log(`Risk Level: ${response.data.riskLevel}`)
            console.log(`Is TOR Address: ${response.data.isTorAddress}`)
            console.log(`Is VPN Address: ${response.data.isVpnAddress}`)
            console.log(`Check Status: ${response.data.checkStatus}`)
            console.log(`Message Id: ${response.data.messageId}`)
        })
    .catch(error=> console.log(error.response.data));
