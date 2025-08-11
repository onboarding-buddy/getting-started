from onboarding_buddy_client.api.sanctions_api import SanctionsApi
from onboarding_buddy_client.models.individual_sanctions_check_request_m import IndividualSanctionsCheckRequestM
from onboarding_buddy_client.models.entity_sanctions_check_request_m import EntitySanctionsCheckRequestM
from onboarding_buddy_client.models.aircraft_sanctions_check_request_m import AircraftSanctionsCheckRequestM
from onboarding_buddy_client.models.vessel_sanctions_check_request_m import VesselSanctionsCheckRequestM
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