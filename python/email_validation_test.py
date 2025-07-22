from onboarding_buddy_client.api.validation_api import ValidationApi
from onboarding_buddy_client.models.email_address_request_m import EmailAddressRequestM
from onboarding_buddy_client import Configuration, ApiClient
from dotenv import load_dotenv
import os

load_dotenv()

# Configure the client with authentication headers
config = Configuration(
    api_key={
        "AppKey": os.getenv("OB_APP_KEY"),
        "ApiKey": os.getenv("OB_API_KEY"),
        "ApiSecret": os.getenv("OB_API_SECRET")
    }
)

# Initialize the API client
api_client = ApiClient(configuration=config)
validation_api = ValidationApi(api_client)

# Submit the email validation request
try:
    # Create an email validation request
    request = EmailAddressRequestM(
        email_address="email@domain.com"
    )

    response = validation_api.email(email_address_request_m=request)
    
    print(f"Email Address: {response.email_address}")
    print(f"Email Status: {response.email_status}")
    print(f"Free Email: {response.free_email}")
    print(f"Domain: {response.domain}")
    print(f"MX Record: {response.mx_record}")
    print(f"MX Found: {response.mx_found}")
    print(f"Check Status: {response.check_status}")
except Exception as e:
    print(f"Error: {e}")