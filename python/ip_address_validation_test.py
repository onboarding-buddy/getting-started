from onboarding_buddy_client.api.validation_api import ValidationApi
from onboarding_buddy_client.models.ip_address_request_m import IpAddressRequestM
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

# Submit the ip address validation request
try:
    # Create an ip address validation request
    request = IpAddressRequestM(
        ip_address="46.182.106.190"
    )

    response = validation_api.ipaddress(ip_address_request_m=request)

    print(f"ipAddress: {response.ip_address}")
    print(f"ISO Code: {response.iso_code}")
    print(f"Country: {response.country}")
    print(f"Threat: {response.threat}")
    print(f"Risk Level: {response.risk_level}")
    print(f"Is TOR Address: {response.is_tor_address}")
    print(f"Is VPN Address: {response.is_vpn_address}")
    print(f"Check Status: {response.check_status}")
    print(f"Message Id: {response.message_id}")

except Exception as e:
    print(f"Error: {e}")