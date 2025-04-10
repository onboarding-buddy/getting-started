from onboarding_buddy_client.api.sanctions_api import SanctionsApi
from onboarding_buddy_client.models.aircraft_sanctions_check_request_m import AircraftSanctionsCheckRequestM
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

# Submit the aircraft request
try:
    # Create an aircraft sanctions request
    request = AircraftSanctionsCheckRequestM(
        name="RA-02791"
    )

    response = sanctions_api.aircraft(aircraft_sanctions_check_request_m=request)
    
    print(f"Matched: {response.matched}")
    print(f"Full Name: {response.match[0].full_name}")
    print(f"Program: {response.match[0].program}")
    print(f"Manufacture Date: {response.match[0].manufacture_date}")
    print(f"Model: {response.match[0].model}")
    print(f"Operator: {response.match[0].operator}")
    print(f"Mode S Transponder Code: {response.match[0].mode_s_transponder_code}")
    print(f"Serial Identification: {response.match[0].serial_identification}")
    print(f"Linked To: {response.match[0].linked_to}")
    print(f"Alias: {response.match[0].alias_list[0].full_name}")
    
except Exception as e:
    print(f"Error: {e}")