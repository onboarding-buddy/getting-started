# Python Getting Started Instructions

To get started calling the Onboarding Buddy API using the Python library hosted in PyPi please use the following instructions.  These instructions assume that python is installed and uses a virtual environment.

## Activate environment

```
python -m venv venv

```

## Install Onboarding Buddy SDK

```
pip install onboarding-buddy-client
```

## Set environment variables

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

