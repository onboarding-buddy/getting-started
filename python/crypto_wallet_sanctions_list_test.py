from onboarding_buddy_client.api.sanctions_api import SanctionsApi
from onboarding_buddy_client.models.crypto_wallet_sanctions_check_request_m import CryptoWalletSanctionsCheckRequestM
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

# Submit the crypto wallet request
try:
    # Create an crypto wallet sanctions request
    request = CryptoWalletSanctionsCheckRequestM(
        address="0X098B716B8AAF21512996DC57EB0615E2383E2F96"
    )

    response = sanctions_api.crypto_wallet(crypto_wallet_sanctions_check_request_m=request)
    
    print(f"Matched: {response.matched}")
    print(f"Entity Name: {response.entity_match[0].full_name}")
    print(f"Program: {response.entity_match[0].program}")
    print(f"Additional Info: {response.entity_match[0].additional_info}")
    print(f"Secondary Sanctions Risk: {response.entity_match[0].secondary_sanctions_risk}")

    print("\nAddress List:")        
    for x in response.entity_match[0].address_list:
        print(f"{x.address} {x.city_state_province_postal_code} {x.country}")

    print("\nCurrency List:")
    for y in response.entity_match[0].digital_currency_address:
        print(f"{y.instrument}: {y.address}")


    print("\nAlias List:")        
    for z in response.entity_match[0].also_known_as:
        print(f"{z}")

except Exception as e:
    print(f"Error: {e}")