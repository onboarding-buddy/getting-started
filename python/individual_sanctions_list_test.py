from onboarding_buddy_client.api.sanctions_api import SanctionsApi
from onboarding_buddy_client.models.individual_sanctions_check_request_m import IndividualSanctionsCheckRequestM
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

# Submit the email validation request
try:
    # Create an individual sanction request
    request = IndividualSanctionsCheckRequestM(
        first_name="YEVGENIY",
        last_name="PRIGOZHIN",
        birth_year="1961"
    )

    response = sanctions_api.individual(individual_sanctions_check_request_m=request)
    
    print(f"Matched: {response.matched}")
    print(f"Full Name: {response.results[0].full_name}")
    print(f"First Name: {response.results[0].first_name}")
    print(f"Last Name: {response.results[0].last_name}")
    print(f"Program: {response.results[0].program}")
    print(f"Additional Information: {response.results[0].additional_info}")
    print(f"Date Of Birth: {response.results[0].date_of_birth}")
    print(f"Birth Year: {response.results[0].birth_year}")
    print(f"Nationality: {response.results[0].nationality}")
    print(f"Gender: {response.results[0].gender}")
    print(f"Linked To: {response.results[0].linked_to}")
    print(f"Secondary Sanctions Risk: {response.results[0].secondary_sanctions_risk}")
    
except Exception as e:
    print(f"Error: {e}")