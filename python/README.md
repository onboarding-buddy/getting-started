# Python Getting Started Instructions

To get started calling the OnboardingBuddy API using our Python library hosted in <a href="https://pypi.org/project/onboarding-buddy-client/">PyPi</a>, please use the following instructions.  These instructions assume the use of anaconda for your python virtual environment.

## 1 Clone the repository and navigate to the python folder
```
git clone https://github.com/onboarding-buddy/getting-started.git
cd getting-started
```

## 2 Set environment variables

In order to set the environment variables you will need to create an Onboarding Buddy account.  If you need to do this please follow the instructions in <a href="https://pypi.org/project/onboarding-buddy-client/">this video</a> to locate and copy the required keys.

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

### 5.1 Test email validation

To test email validation use the command

```
python email_validation_test.py
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

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation/operation/email">here</a> 

### 5.2 Test ip address validation

To test ip address validation use the command

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

### 5.3 Test user agent validation

To test user_agent validation use the command

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

### 5.4 Test mobile number validation

To test mobile number validation use the command

```
python mobile_number_validation_test.py
```

Your should see output similar to the below for a successful call

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

### 5.5 Test sanctions list individual validation

To test sanctions list individual use the command

```
python individual_sanctions_list_test.py
```

Your should see output similar to the below for a successful call

```
Matched: True
Full Name: PRIGOZHIN, Yevgeniy Viktorovich
First Name: Yevgeniy Viktorovich
Last Name: PRIGOZHIN
Program: [UKRAINE-EO13661] [CYBER2] [ELECTION-EO13848] [RUSSIA-EO14024]
Additional Information: ['alt. Secondary sanctions risk: See Section 11 of Executive Order 14024.']
Date Of Birth: 01 Jun 1961
Birth Year: 1961
Nationality: RUSSIA
Gender: MALE
Linked To: INTERNET RESEARCH AGENCY LLC.
Secondary Sanctions Risk: UKRAINE-/RUSSIA-RELATED SANCTIONS REGULATIONS, 31 CFR 589.201 AND/OR 589.209
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/individual">here</a> 

### 5.6 Test sanctions list entity validation

To test sanctions list entity use the command

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
NOVINFO LLC
GLAVSET, OOO
MEDIASINTEZ, OOO
NOVINFO, OOO
LAKHTA INTERNET RESEARCH

Address List:
55 Savushkina Street St. Petersburg Russia
d. 4 korp. 3 litera A pom. 9-N, ofis 238, ul. Optikov St. Petersburg Russia
d. 4 litera B pom. 22-N, ul. Starobelskaya St. Petersburg Russia
d. 79 litera A. pom 1-N, ul. Planernaya St. Petersburg Russia
4 Optikov Street, Building 3 St. Petersburg Russia

Linked Individuals:
PRIGOZHIN, Yevgeniy Viktorovich
BYSTROV, Mikhail Ivanovich
BURCHIK, Mikhail Leonidovich
KRYLOVA, Aleksandra Yuryevna
BOGACHEVA, Anna Vladislavovna
POLOZOV, Sergey Pavlovich
BOVDA, Maria Anatolyevna
BOVDA, Robert Sergeyevich
ASLANOV, Dzheykhun Nasimi Ogly
PODKOPAEV, Vadim Vladimirovich
VASILCHENKO, Gleb Igorevich
KAVERZINA, Irina Viktorovna
VENKOV, Vladimir Dmitriyevich
NESTEROV, Igor Vladimirovich
KUZMIN, Denis Igorevich
PRIBYSHIN, Taras Kirillovich

Secondary Sanctions Risk: UKRAINE-/RUSSIA-RELATED SANCTIONS REGULATIONS, 31 CFR 589.201.
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/entity">here</a> 

### 5.7 Test sanctions list aircraft validation

To test sanctions list aircraft use the command

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

To test sanctions list vessel use the command

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

To test sanctions list crypto wallet use the command

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
ETH: 0X35FB6F6DB4FB05E6A4CE86F2C93691425626D4B1
ETH: 0XF7B31119C2682C88D88D455DBB9D5932C65CF1BE
ETH: 0X3E37627DEAA754090FBFBB8BD226C1CE66D255E9
ETH: 0X08723392ED15743CC38513C4925F5E6BE5C17243

Alias List:
'HIDDEN COBRA'
'OFFICE 91'
'GUARDIANS OF PEACE'
'THE NEW ROMANTIC CYBER ARMY TEAM'
'WHOIS HACKIN'
```

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/sanctions/operation/crypto-wallet">here</a> 

### 5.10 Test email validation with sanction data

To test email validation with sanctions use the command

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
