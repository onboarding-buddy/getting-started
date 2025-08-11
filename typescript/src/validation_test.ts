import OnboardingBuddyClient from '@onboardingbuddy/onboardingbuddy-client'
import * as dotenv from 'dotenv'
dotenv.config()

const obAppKey = process.env.OB_APP_KEY || '';
const obApiKey = process.env.OB_API_KEY || '';
const obApiSecret = process.env.OB_API_SECRET || '';

const client = new OnboardingBuddyClient(obAppKey, obApiKey, obApiSecret);

// Email Validation
const emailValidation = client.validateEmail({
  emailAddress: 'email@domain.com',
});

emailValidation
    .then(response => 
        { 
            console.log("\n[1] Email Address Validation")
            console.log("----------------------------")
            console.log(`Email Address: ${response.data.emailAddress}`)
            console.log(`Email Status: ${response.data.emailStatus}`)
            console.log(`Free Email: ${response.data.freeEmail}`)
            console.log(`Domain: ${response.data.domain}`)
            console.log(`MX Record: ${response.data.mxRecord}`)
            console.log(`MX Found: ${response.data.mxFound}`)
            console.log(`Check Status: ${response.data.checkStatus}`)
            console.log(`Message Id: ${response.data.messageId}`)
        })
    .catch(error=> console.log(error.response.data));

const ipAddressValidation = client.validateIpAddress({
  ipAddress: '46.182.106.190',
});

ipAddressValidation
    .then(response => 
        { 
            console.log("\n[2] IP Address Validation")
            console.log("----------------------------")
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

const browserValidation = client.validateBrowser({
  userAgent: 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36',
});

browserValidation
    .then(response => 
        { 
            console.log("\n[3] Browser Validation")
            console.log("----------------------------")
            console.log(`User Agent: ${response.data.userAgent}`)
            console.log(`Simple Software: ${response.data.simpleSoftware}`)
            console.log(`Software: ${response.data.software}`)
            console.log(`Software Name: ${response.data.softwareName}`)
            console.log(`Operating System: ${response.data.operatingSystem}`)
            console.log(`Operating System Flavour: ${response.data.operatingSystemFlavour}`)
            console.log(`Operating System Version: ${response.data.operatingSystemVersion}`)
            console.log(`Is Abusive: ${response.data.isAbusive}`)
            console.log(`Check Status: ${response.data.checkStatus}`)
            console.log(`Message Id: ${response.data.messageId}`)
        })
    .catch(error=> console.log(error.response.data));

const mobileNumberValidation = client.validateMobile({
  mobileNumber: {
     prefix:'61',
     number:'0422123456'
  }
});

mobileNumberValidation
    .then(response => 
        { 
            console.log("\n[4] Mobile Number Validation")
            console.log("----------------------------")
            console.log(`Mobile Number: ${response.data.mobileNumber}`)
            console.log(`Valid: ${response.data.valid}`)
            console.log(`Local Format: ${response.data.localFormat}`)
            console.log(`International Format: ${response.data.internationalFormat}`)
            console.log(`Country Prefix: ${response.data.countryPrefix}`)
            console.log(`Country Code: ${response.data.countryCode}`)
            console.log(`Carrier: ${response.data.carrier}`)
            console.log(`Line Type: ${response.data.lineType}`)
            console.log(`Check Status: ${response.data.checkStatus}`)
            console.log(`Message Id: ${response.data.messageId}`)
        })
    .catch(error=> console.log(error.response.data));