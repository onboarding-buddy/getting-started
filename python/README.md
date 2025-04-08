# Python Getting Started Instructions

To get started calling the OnboardingBuddy API using our Python library hosted in <a href="https://pypi.org/project/onboarding-buddy-client/">PyPi</a>, please use the following instructions.  These instructions assumes that python is installed on your local machine and uses a virtual environment.

## 1 Activate environment

```
python -m venv venv

```

## Install Onboarding Buddy SDK

```
pip install onboarding-buddy-client
```

## Set environment variables

In order to set the environment variables you will need to create an Onboarding Buddy account.  If you need to do this please follow the instructions in <a href="https://pypi.org/project/onboarding-buddy-client/">this video</a> to locate and copy the required keys.

Linux based operating systems
```
EXPORT OB_APP_KEY=VALUE
EXPORT OB_API_KEY=VALUE
EXPORT OB_API_SECRET=VALUE
```

Windows based operating systems
```
SETX OB_APP_KEY "VALUE"
SETX OB_API_KEY "VALUE"
SETX OB_API_SECRET "VALUE"
```

## Test email validation

To test email validation use the command

```
python email_validation_test.py
```

