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
    # Create an email validation with sanctions request
    request = EmailAddressRequestM(
        email_address="AFRICONLINE@PROTONMAIL.COM"
    )

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
        print(f"Entity Name: {response.sanction_record.entity_match[0].full_name}")
        print(f"Program: {response.sanction_record.entity_match[0].program}")
        print(f"Secondary Sanctions Risk: {response.sanction_record.entity_match[0].secondary_sanctions_risk}")

        print("\nAlias List:")        
        for x in response.sanction_record.entity_match[0].also_known_as:
            print(f"{x}")

        print("\nLinked Individuals:")
        for y in response.sanction_record.entity_match[0].linked_individuals:
            print(f"Full Name: {y.full_name}")
            print(f"Linked To: {y.linked_to}")

except Exception as e:
    print(f"Error: {e}")