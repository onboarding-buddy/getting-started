from onboarding_buddy_client.api.validation_api import ValidationApi
from onboarding_buddy_client.models.browser_request_m import BrowserRequestM
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