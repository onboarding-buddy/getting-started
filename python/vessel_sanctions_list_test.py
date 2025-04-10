from onboarding_buddy_client.api.sanctions_api import SanctionsApi
from onboarding_buddy_client.models.vessel_sanctions_check_request_m import VesselSanctionsCheckRequestM
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
sanctions_api = SanctionsApi(api_client)

# Submit the vessel sanction request
try:
    # Create a vessel sanction check request
    request = VesselSanctionsCheckRequestM(
        name="HWANG GUM SAN 2"
    )

    response = sanctions_api.vessel(vessel_sanctions_check_request_m=request)
    
    print(f"Matched: {response.matched}")
    print(f"Full Name: {response.match[0].full_name}")
    print(f"Program: {response.match[0].program}")
    print(f"Additional Info: {response.match[0].additional_info}")
    print(f"Secondary Sanctions Risk: {response.match[0].secondary_sanctions_risk}")
    print(f"Vessel Type: {response.match[0].vessel_type}")
    print(f"Vessel Flag: {response.match[0].vessel_flag}")
    
except Exception as e:
    print(f"Error: {e}")