# Typescript SDK - Getting Started Instructions

To get started calling the OnboardingBuddy API using our Typescript library hosted in <a href="https://www.npmjs.com/org/onboardingbuddy">NPM</a>, please use the following instructions.  

## 1 Clone the repository and navigate to the npx ts-node src/folder
```
git clone https://github.com/onboarding-buddy/getting-started.git
cd getting-started
```

## 2 Set environment variables

In order to set the environment variables you will need to create an Onboarding Buddy account.  If you need to do this please follow the instructions in <a href="https://video.link">this video</a> to locate and copy the required keys.

### 2.1 Create .env file

Create a .env file in the typescript folder with the following entries
```
OB_APP_KEY=REPLACE WITH YOUR APP KEY
OB_API_KEY=REPLACE WITH YOUR API KEY
OB_API_SECRET=REPLACE WITH YOUR API SECRET
```
Make sure to update the values with those from your Onboarding Buddy account.

## 3 Install dependancies

### 3.1 Install default packages

```
npm install
```

### 3.2 Install Onboarding Buddy SDK

```
npm install @onboardingbuddy/onboardingbuddy-client
```

## 4 Run tests

### 4.1 Test - Email Address Validation

This code snippet from the email_validation_test.ts file demonstrates how to validate a mobile number.

```typescript
// Email Validation
const emailValidation = client.validateEmail({
  emailAddress: 'email@domain.com',
});

emailValidation
    .then(response => 
        { 
            .....
        }
```

To run this code example use the command:

```
npx ts-node src/email_validation_test.ts
```

Your should see output similar to the below for a successful call.

```
Email Address: email@domain.com
Email Status: Caution
Free Email: False
Domain: domain.com
MX Record: domain-com.mail.protection.outlook.com
MX Found: True
Check Status: Matched
Has Sanction Match: False
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation/operation/email">here</a> 

### 4.2 Test - IP Address Validation

This code snippet from the ip_address_validation_test.ts file demonstrates how to validate a mobile number.

```typescript
const ipAddressValidation = client.validateIpAddress({
  ipAddress: '46.182.106.190',
});

ipAddressValidation
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/ip_address_validation_test.ts
```

Your should see output similar to the below for a successful call

```
ipAddress: 46.182.106.190
ISO Code: NL
Country: Netherlands
Threat: honeypot_tracker
Risk Level: 5
Is TOR Address: True
Is VPN Address: False
Check Status: Matched
Message Id: c45d379d-242b-4d7c-94bc-4309098b68f3
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation/operation/ipaddress">here</a> 

### 4.3 Test - User Agent Validation

This code snippet from the user_agent_validation_test.ts file demonstrates how to validate a user agent.

```typescript
const browserValidation = client.validateBrowser({
  userAgent: 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36',
});

browserValidation
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/user_agent_validation_test.ts
```

Your should see output similar to the below for a successful call

```
Email Address: email@domain.com
Email Status: Caution
Free Email: False
Domain: domain.com
MX Record: domain-com.mail.protection.outlook.com
MX Found: True
Check Status: Matched
Has Sanction Match: False
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation/operation/browser">here</a> 

### 4.4 Test - Mobile Number Validation

This code snippet from the mobile_number_validation_test.ts file demonstrates how to validate a mobile number.

```typescript
const mobileNumberValidation = client.validateMobile({
  mobileNumber: {
     prefix:'61',
     number:'0422123456'
  }
});

mobileNumberValidation
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/mobile_number_validation_test.ts
```

Your should see output similar to the below for a successful call.

```
Mobile Number: 0422123456
Valid: True
localFormat: 0422123456
internationalFormat: +61422123456
countryPrefix: +61
countryCode: AU
Carrier: Optus Mobile Pty Ltd
Line Type: mobile
Check Status: Matched
Message Id: e9dc7728-f13b-47ff-8280-71dfabf152dd
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation/operation/mobile">here</a> 

### 4.5 Test - Sanctions Check Individual

This code snippet from the individual_sanctions_list_test.ts file demonstrates how to submit a sanctions check for an individual.

```typescript
const individualSanctions = client.checkIndividualSanctions({
  firstName: 'YEVGENIY',
  lastName: 'PRIGOZHIN',
  birthYear: '1961',
});

individualSanctions
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/individual_sanctions_list_test.ts
```

Your should see output similar to the below for a successful call.

```
Matched: True
Full Name: PRIGOZHIN, Yevgeniy Viktorovich
First Name: Yevgeniy Viktorovich
Last Name: PRIGOZHIN
Program: [UKRAINE-EO136w61] [CYBER2] [ELECTION-EO13848] [RUSSIA-EO14024]
Additional Information: ['alt. Secondary sanctions risk: See Section 11 of Executive Order 14024.']
Date Of Birth: 01 Jun 1961
Birth Year: 1961
Nationality: RUSSIA
Gender: MALE
Linked To: INTERNET RESEARCH AGENCY LLC.
Secondary Sanctions Risk: UKRAINE-/RUSSIA-RELATED SANCTIONS REGULATIONS, 31 CFR 589.201 AND/OR 589.209
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/individual">here</a> 

### 4.6 Test - Sanctions Check Entity

This code snippet from the entity_sanctions_list_test.ts file demonstrates how to submit a sanctions check for an entity.

```typescript
const entitySanctions = client.checkEntitySanctions({
  name: 'INTERNET RESEARCH AGENCY LLC'
});

entitySanctions
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/entity_sanctions_list_test.ts
```

Your should see output similar to the below for a successful call

```
Matched: True
Full Name: INTERNET RESEARCH AGENCY LLC
Program: [CYBER2] [ELECTION-EO13848]
Alias List:
MEDIASINTEZ LLC
GLAVSET LLC
MIXINFO LLC
AZIMUT LLC
.........

Address List:
55 Savushkina Street St. Petersburg Russia
d. 4 korp. 3 litera A pom. 9-N, ofis 238, ul. Optikov St. Petersburg Russia
.........

Linked Individuals:
PRIGOZHIN, Yevgeniy Viktorovich
BYSTROV, Mikhail Ivanovich
BURCHIK, Mikhail Leonidovich
KRYLOVA, Aleksandra Yuryevna
BOGACHEVA, Anna Vladislavovna
........

Secondary Sanctions Risk: UKRAINE-/RUSSIA-RELATED SANCTIONS REGULATIONS, 31 CFR 589.201.
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/entity">here</a> 

### 4.7 Test sanctions list aircraft validation

This code snippet from the aircraft_sanctions_list_test.ts file demonstrates how to submit a sanctions check for an aircraft.

```typescript
const aircraftSanctions = client.checkAircraftSanctions({
  name: 'RA-02791'
});

aircraftSanctions
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/aircraft_sanctions_list_test.ts
```

Your should see output similar to the below for a successful call

```
Matched: True
Full Name: RA-02791
Program: [UKRAINE-EO13661] [CYBER2] [ELECTION-EO13848]
Manufacture Date: 01 DEC 2000
Model: HAWKER 800XP
Operator: BERATEX GROUP LIMITED
Mode S Transponder Code: 140AE7
Serial Identification: 258512
Linked To: BERATEX GROUP LIMITED.
Alias: M-VITO
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/aircraft">here</a> 

### 4.8 Test sanctions list vessel validation

This code snippet from the vessel_sanctions_list_test.ts file demonstrates how to submit a sanctions check for a vessel.

```typescript
const vesselSanctions = client.checkVesselSanctions({
  name: 'HWANG GUM SAN 2'
});

vesselSanctions
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:

```
npx ts-node src/vessel_sanctions_list_test.ts
```

Your should see output similar to the below for a successful call

```
Matched: True
Full Name: HWANG GUM SAN 2
Program: [DPRK]
Additional Info: ['Transactions Prohibited For Persons Owned or Controlled By U.S. Financial Institutions: North Korea Sanctions Regulations section 510.214'] 
Secondary Sanctions Risk: NORTH KOREA SANCTIONS REGULATIONS, SECTIONS 510.201 AND 510.210
Vessel Type: General Cargo
Vessel Flag: Democratic People's Republic of Korea
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/vessel">here</a> 

### 4.9 Test sanctions list crypto wallet validation

This code snippet from the crypto_wallet_sanctions_list_test.ts file demonstrates how to submit a sanctions check for a crypto wallet.

```typescript
const cryptoWalletSanctions = client.checkCryptoWalletSanctions({
  address: '0X098B716B8AAF21512996DC57EB0615E2383E2F96'
});

cryptoWalletSanctions
    .then(response => 
        { 
            ...
        }
```

To run this code example use the command:
```
npx ts-node src/crypto_wallet_sanctions_list_test.ts
```

Your should see output similar to the below for a successful call

```
Matched: True
Entity Name: LAZARUS GROUP
Program: [DPRK3]
Additional Info: ['Transactions Prohibited For Persons Owned or Controlled By U.S. Financial Institutions: North Korea Sanctions Regulations section 510.214'] 
Secondary Sanctions Risk: NORTH KOREA SANCTIONS REGULATIONS, SECTIONS 510.201 AND 510.210

Address List:
Potonggang District Pyongyang Korea, North

Currency List:
ETH: 0X098B716B8AAF21512996DC57EB0615E2383E2F96
ETH: 0XA0E1C89EF1A489C9C7DE96311ED5CE5D32C20E4B
ETH: 0X3CFFD56B47B7B41C56258D9C7731ABADC360E073
ETH: 0X53B6936513E738F44FB50D2B9476730C0AB3BFC1
......

Alias List:
'HIDDEN COBRA'
'OFFICE 91'
'GUARDIANS OF PEACE'
'THE NEW ROMANTIC CYBER ARMY TEAM'
'WHOIS HACKIN'
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/crypto-wallet">here</a> 

### 4.10 Test email validation with sanction data

This code snippet from the email_sanctions_test.ts file demonstrates how to submit a sanctions check for an email address.

```typescript
const emailValidation = client.validateEmail({
  emailAddress: 'AFRICONLINE@PROTONMAIL.COM',
});

emailValidation
    .then(response => 
        { 
            ...
        }

```

To run this code example use the command:
```
npx ts-node src/email_sanctions_test.ts
```

Your should see output similar to the below for a successful call

```
Email Address: africonline@protonmail.com
Email Status: Invalid
Free Email: True
Domain: protonmail.com
MX Record: mail.protonmail.ch
MX Found: True
Check Status: Matched
Has Sanction Match: True
Entity Name: ASSOCIATION FOR FREE RESEARCH AND INTERNATIONAL COOPERATION
Program: [UKRAINE-EO13661] [CYBER2] [ELECTION-EO13848]
Secondary Sanctions Risk: UKRAINE-/RUSSIA-RELATED SANCTIONS REGULATIONS, 31 CFR 589.201 AND/OR 589.209

Alias List:
'AFRIC'

Linked Individuals:
Full Name: PRIGOZHIN, Yevgeniy Viktorovich
Linked To: INTERNET RESEARCH AGENCY LLC.
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation/operation/email">here</a> 
