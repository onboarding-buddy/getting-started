using OnboardingBuddyClient.Api; // For API classes
using OnboardingBuddyClient.Client; // For Configuration and ApiClient
using OnboardingBuddyClient.Model; // For models

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Configure the API client
            var config = new OnboardingBuddyConfiguration
            {
                AppKey = Environment.GetEnvironmentVariable("OB_APP_KEY"),
                ApiKey = Environment.GetEnvironmentVariable("OB_API_KEY"),
                ApiSecret = Environment.GetEnvironmentVariable("OB_API_SECRET"),
            };

            // Initialize the client
            var apiInstance = new ValidationApi(config);

            // Create the request payload
            var request = new EmailAddressRequestM
            {
                EmailAddress = "support@onboardingbuddy.co"
            };

            // Call the POST /validation/email endpoint
            var response = await apiInstance.EmailAsync(request);

            // Display the response
            Console.WriteLine("Email Validation Response:");
            Console.WriteLine($"Message ID: {response.MessageId}");
            Console.WriteLine($"Correlation ID: {response.CorrelationId}");
            Console.WriteLine($"Email Address: {response.EmailAddress}");
            Console.WriteLine($"Email Status: {response.EmailStatus}"); // Enum: 0=Unknown, 1=Valid, etc.
            Console.WriteLine($"Free Email: {response.FreeEmail}");
            Console.WriteLine($"Domain: {response.Domain}");
            Console.WriteLine($"MX Found: {response.MxFound}");
            Console.WriteLine($"Check Status: {response.CheckStatus}"); // Enum: 1=Matched, 2=NotMatched, 3=Error
            if (response.HasSanctionMatch == true)
            {
                Console.WriteLine("Sanction Match Detected:");
                var sanction = response.SanctionRecord;
                if (sanction?.IndividualMatch != null && sanction.IndividualMatch.Count > 0)
                {
                    foreach (var individual in sanction.IndividualMatch)
                    {
                        Console.WriteLine($"- Individual: {individual.FullName}");
                    }
                }
            }
        }
        catch (ApiException ex)
        {
            Console.WriteLine($"API Error: {ex.ErrorCode} - {ex.Message}");
            if (ex.ErrorContent != null)
            {
                Console.WriteLine($"Error Details: {ex.ErrorContent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General Error: {ex.Message}");
        }
    }
}