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
