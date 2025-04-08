from onboarding_buddy_client.api.validation_api import ValidationApi
from onboarding_buddy_client.models.email_address_request_m import EmailAddressRequestM
from onboarding_buddy_client import Configuration, ApiClient
from dotenv import load_dotenv
import os

load_dotenv()

ob_app_key = os.getenv("OB_APP_KEY")
ob_api_key = os.getenv("OB_API_KEY")
ob_api_secret = os.getenv("OB_API_SECRET")

print(f"ob_app_key: {ob_app_key}")
print(f"ob_api_key: {ob_api_key}")
print(f"ob_api_secret: {ob_api_secret}")

# Configure the client with authentication headers
config = Configuration(
    host="https://api.dev.onboardingbuddy.co/validation-service",
    api_key={
        "AppKey": ob_app_key,
        "ApiKey": ob_api_key,
        "ApiSecret": ob_api_secret
    }
)
# Ensure no prefix is added to headers
config.api_key_prefix["AppKey"] = ""
config.api_key_prefix["ApiKey"] = ""
config.api_key_prefix["ApiSecret"] = ""

# Initialize the API client
api_client = ApiClient(configuration=config)
validation_api = ValidationApi(api_client)

# Create an email validation request
request = EmailAddressRequestM(
    email_address="email@domain.com"
)

# Submit the email validation request
try:
    response = validation_api.email(email_address_request_m=request)
    print(f"Email Address: {response.email_address}")
    print(f"Email Status: {response.email_status}")
    print(f"Free Email: {response.free_email}")
    print(f"Domain: {response.domain}")
    print(f"MX Record: {response.mx_record}")
    print(f"MX Found: {response.mx_found}")
    print(f"Check Status: {response.check_status}")
    print(f"Has Sanction Match: {response.has_sanction_match}")
    if response.has_sanction_match:
        print(f"Sanction Match: {response.sanction_record}")
except Exception as e:
    print(f"Error: {e}")