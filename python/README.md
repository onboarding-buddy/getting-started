# Python SDK - Getting Started Instructions

To get started calling the OnboardingBuddy API using our Python library hosted in <a href="https://pypi.org/project/onboarding-buddy-client/">PyPi</a>, please use the following instructions.  These instructions assume the use of anaconda for your python virtual environment.

## 1 Clone the repository and navigate to the python folder
```
git clone https://github.com/onboarding-buddy/getting-started.git
cd getting-started
```

## 1.1 Create an OnboardingBuddy account 

If you have not already, navigate to the Onboarding Buddy website at https://www.onboardingbuddy.co.  Proceed to register and create an account.

## 1.2 Get you API credentials

Navigate to you application making note of the following:

```
APP_KEY (Application Key)
API_KEY (Api Key)
API_SECRET (Api Secret)
```

### 2.1 Create .env file

Create a .env file in the python folder with the following entries
```
OB_APP_KEY=REPLACE WITH YOUR APP KEY
OB_API_KEY=REPLACE WITH YOUR API KEY
OB_API_SECRET=REPLACE WITH YOUR API SECRET
```
Make sure to update the values with those from your Onboarding Buddy account.

## 3 Activate environment

```
conda activate venv
cd python
```

## 4 Install dependancies

### 4.1 Install the requirements.txt

```
pip install -r requirements.txt
```

### 4.2 Install Onboarding Buddy SDK

```
pip install onboarding-buddy-client
```

## 5 Run tests

### 5.1 Test - Email Address Validation

This code snippet from the email_validation_test.py file demonstrates how to validate a mobile number.

```python
# Create an email validation request
request = EmailAddressRequestM(
    email_address="email@domain.com"
)

response = validation_api.email(email_address_request_m=request)
```

To run this code example use the command:

```
python email_validation_test.py
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

### 5.2 Test - IP Address Validation

This code snippet from the ip_address_validation_test.py file demonstrates how to validate a mobile number.

```python
# Create an ip address validation request
request = IpAddressRequestM(
    ip_address="46.182.106.190"
)

response = validation_api.ipaddress(ip_address_request_m=request)

```

To run this code example use the command:

```
python ip_address_validation_test.py
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

### 5.3 Test - User Agent Validation

This code snippet from the user_agent_validation_test.py file demonstrates how to validate a user agent.

```python

    request = BrowserRequestM(
        user_agent="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36"
    )

    response = validation_api.browser(browser_request_m=request)
    
```

To run this code example use the command:

```
python user_agent_validation_test.py
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

### 5.4 Test - Mobile Number Validation

This code snippet from the mobile_number_validation_test.py file demonstrates how to validate a mobile number.

```python
# Create an mobile number validation request
request = MobileNumberRequestM(
    mobile_number=MobileNumberM(
        prefix="61",
        number="0422123456"
    )
)

response = validation_api.mobile(mobile_number_request_m=request)
```

To run this code example use the command:

```
python mobile_number_validation_test.py
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

### 5.5 Test - Sanctions Check Individual

This code snippet from the individual_sanctions_list_test.py file demonstrates how to submit a sanctions check for an individual.

```python
# Create an individual sanction request
request = IndividualSanctionsCheckRequestM(
    first_name="YEVGENIY",
    last_name="PRIGOZHIN",
    birth_year="1961"
)

response = sanctions_api.individual(individual_sanctions_check_request_m=request)
    
```

To run this code example use the command:

```
python individual_sanctions_list_test.py
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

### 5.6 Test - Sanctions Check Entity

This code snippet from the entity_sanctions_list_test.py file demonstrates how to submit a sanctions check for an entity.

```python
# Create an entity sanctions check request
request = EntitySanctionsCheckRequestM(
    name="INTERNET RESEARCH AGENCY LLC"
)

response = sanctions_api.entity(entity_sanctions_check_request_m=request)
    
```

To run this code example use the command:

```
python entity_sanctions_list_test.py
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

### 5.7 Test sanctions list aircraft validation

This code snippet from the aircraft_sanctions_list_test.py file demonstrates how to submit a sanctions check for an aircraft.

```python
# Create an aircraft sanctions request
request = AircraftSanctionsCheckRequestM(
    name="RA-02791"
)

response = sanctions_api.aircraft(aircraft_sanctions_check_request_m=request)
```

To run this code example use the command:

```
python aircraft_sanctions_list_test.py
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

### 5.8 Test sanctions list vessel validation

This code snippet from the vessel_sanctions_list_test.py file demonstrates how to submit a sanctions check for a vessel.

```python
# Create a vessel sanction check request
request = VesselSanctionsCheckRequestM(
    name="HWANG GUM SAN 2"
)

response = sanctions_api.vessel(vessel_sanctions_check_request_m=request)
```

To run this code example use the command:

```
python vessel_sanctions_list_test.py
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

### 5.9 Test sanctions list crypto wallet validation

This code snippet from the crypto_wallet_sanctions_list_test.py file demonstrates how to submit a sanctions check for a crypto wallet.

```python
# Create an crypto wallet sanctions request
request = CryptoWalletSanctionsCheckRequestM(
    address="0X098B716B8AAF21512996DC57EB0615E2383E2F96"
)

response = sanctions_api.crypto_wallet(crypto_wallet_sanctions_check_request_m=request)
```

To run this code example use the command:
```
python crypto_wallet_sanctions_list_test.py
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

### 5.10 Test email validation with sanction data

This code snippet from the email_sanctions_test.py file demonstrates how to submit a sanctions check for an email address.

```python
# Create an email validation with sanctions request
request = EmailAddressRequestM(
    email_address="AFRICONLINE@PROTONMAIL.COM"
)

response = validation_api.email(email_address_request_m=request)
```

To run this code example use the command:
```
python email_sanctions_test.py
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
