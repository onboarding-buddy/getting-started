from onboarding_buddy_client.api.validation_api import ValidationApi
from onboarding_buddy_client.models.mobile_number_request_m import MobileNumberRequestM
from onboarding_buddy_client.models.mobile_number_m import MobileNumberM
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

# Submit the mobile number validation request
try:
    # Create an mobile number validation request
    request = MobileNumberRequestM(
        mobile_number=MobileNumberM(
            prefix="61",
            number="0422123456"
        )
    )
    
    response = validation_api.mobile(mobile_number_request_m=request)
    
    print(f"Mobile Number: {response.mobile_number}")
    print(f"Valid: {response.valid}")
    print(f"localFormat: {response.local_format}")
    print(f"internationalFormat: {response.international_format}")
    print(f"countryPrefix: {response.country_prefix}")
    print(f"countryCode: {response.country_code}")
    print(f"Carrier: {response.carrier}")
    print(f"Line Type: {response.line_type}")
    print(f"Check Status: {response.check_status}")
    print(f"Message Id: {response.message_id}")

except Exception as e:
    print(f"Error: {e}")