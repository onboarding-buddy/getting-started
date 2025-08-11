from onboarding_buddy_client.api.validation_api import ValidationApi
from onboarding_buddy_client.models.email_address_request_m import EmailAddressRequestM
from onboarding_buddy_client.models.ip_address_request_m import IpAddressRequestM
from onboarding_buddy_client.models.browser_request_m import BrowserRequestM
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

try:
    request = BrowserRequestM(
        user_agent="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36"
    )

    response = validation_api.browser(browser_request_m=request)
    
    print(f"User Agent: {response.user_agent}")
    print(f"Simple Software: {response.simple_software}")
    print(f"Software: {response.software}")
    print(f"Software Name: {response.software_name}")
    print(f"Operating System: {response.operating_system}")
    print(f"Operating System Flavour: {response.operating_system_flavour}")
    print(f"Operating System Version: {response.operating_system_version}")
    print(f"Is Abusive: {response.is_abusive}")
    print(f"Check Status: {response.check_status}")
    print(f"Message Id: {response.message_id}")

except Exception as e:
    print(f"Error: {e}")

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