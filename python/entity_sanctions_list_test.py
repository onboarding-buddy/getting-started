from onboarding_buddy_client.api.sanctions_api import SanctionsApi
from onboarding_buddy_client.models.entity_sanctions_check_request_m import EntitySanctionsCheckRequestM
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

# Submit the entity request
try:
    # Create an entity sanctions check request
    request = EntitySanctionsCheckRequestM(
        name="INTERNET RESEARCH AGENCY LLC"
    )
    
    response = sanctions_api.entity(entity_sanctions_check_request_m=request)
    
    print(f"Matched: {response.matched}")
    print(f"Full Name: {response.match[0].full_name}")
    print(f"Program: {response.match[0].program}")
    print("Alias List:")
    for x in response.match[0].alias_list:
        print(x.full_name)

    print("\nAddress List:")        
    for y in response.match[0].address_list:
        print(f"{y.address} {y.city_state_province_postal_code} {y.country}")

    print("\nLinked Individuals:")        
    for z in response.match[0].linked_individuals:
        print(f"{z.full_name}")

    print(f"\nSecondary Sanctions Risk: {response.match[0].secondary_sanctions_risk}")
    
except Exception as e:
    print(f"Error: {e}")