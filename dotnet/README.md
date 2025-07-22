# Get started with the OnboardingBuddyClient

## 1 Create an OnboardingBuddy account 

If you have not already, navigate to the Onboarding Buddy website at https://www.onboardingbuddy.co.  Proceed to register and create an account

## 2 Get you API credentials

Navigate to you application making not of the following:

```
APP_KEY
API_KEY
API_SECRET
```

## 3 Save credentials to environment variables

Use the following commands to create your environment variables:

Windows
```
setx OB_APP_KEY <APP_KEY_VALUE>
setx OB_API_KEY <API_KEY_VALUE>
setx OB_API_SECRET <API_SECRET_VALUE>
```

## 3 Create a test application 
Proceed to create a simple console application toinvoke the service using the following steps

### 3.1 First create a new console application

```
dotnet new console -n OnboardingBuddyTest -f net8.0
cd OnboardingBuddyTest
```
### 3.2 Add the OnboardingBuddyClient package

Add the OnboardingBuddyClient to your project use the following command:
```
dotnet add package OnboardingBuddyClient
```

### 3.3 Write the Test Code

Replace Program.cs with the following code to test the POST /validation/email endpoint:

```csharp
using OnboardingBuddyClient.Api;
using OnboardingBuddyClient.Client;
using OnboardingBuddyClient.Model;

namespace OnboardingBuddyConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var appKey = Environment.GetEnvironmentVariable("OB_APP_KEY");
            var apiKey = Environment.GetEnvironmentVariable("OB_API_KEY");
            var apiSecret = Environment.GetEnvironmentVariable("OB_API_SECRET");

            Configuration config = new Configuration();
            
            // Configure API key authorization: ApiKey
            config.ApiKey.Add("ob-api-key", apiKey);

            // Configure API key authorization: AppKey
            config.ApiKey.Add("ob-app-key", appKey);

            // Configure API key authorization: ApiSecret
            config.ApiKey.Add("ob-api-secret", apiSecret);

            try
            {
                #region ValidationApi

                var validationApi = new ValidationApi(config);

                #region Validate Email Address
                
                var emailAddressRequest = new EmailAddressRequestM(emailAddress: "email@domain.com");
                var emailAddressResponse = await validationApi.EmailAsync(emailAddressRequest);

                Console.WriteLine("Email Address Validation Response: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(emailAddressResponse.ToString());
                #endregion

                #region Validate IP Address
                var ipAddressRequest = new IpAddressRequestM(ipAddress: "46.182.106.190");
                var ipAddressResponse = await validationApi.IpaddressAsync(ipAddressRequest);

                Console.WriteLine("\nIP Address Validation Response: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(ipAddressResponse.ToString());
                #endregion

                #region Validate User Agent
                var browserRequest = new BrowserRequestM(userAgent: "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36" );
                var browserResponse = await validationApi.BrowserAsync(browserRequest);

                Console.WriteLine("\nUser Agent Validation Response: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(browserResponse.ToString());
                #endregion

                #region Validate Mobile Number
                var mobileNumber = new MobileNumberM(prefix: "61", number: "0422123456");
                var mobileNumberRequest = new MobileNumberRequestM(mobileNumber: mobileNumber);

                var mobileNumberResponse = await validationApi.MobileAsync(mobileNumberRequest);

                Console.WriteLine("\nMobile Number Validation Response: ");
                Console.WriteLine("-----------------------------------"); 
                Console.WriteLine(mobileNumberResponse.ToString());

                #endregion

                #endregion

                #region SanctionsApi

                #region Sanctions List Check - Individual

                var sanctionsApi = new SanctionsApi(config);

                var individualSanctionsCheckRequest = new IndividualSanctionsCheckRequestM( 
                    firstName: "YEVGENIY",
                    lastName: "PRIGOZHIN",
                    birthYear: "1961"
                );

                var individualSanctionsCheckResponse = await sanctionsApi.IndividualAsync(individualSanctionsCheckRequest);

                Console.WriteLine("\nIndividual Sanction List Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine($"Matched: { individualSanctionsCheckResponse.Matched}");

                if (individualSanctionsCheckResponse.Results != null)
                {
                    Console.WriteLine($"Full Name: {individualSanctionsCheckResponse.Results[0].FullName}");
                    Console.WriteLine($"First Name: {individualSanctionsCheckResponse.Results[0].FirstName}");
                    Console.WriteLine($"Last Name: {individualSanctionsCheckResponse.Results[0].LastName}");
                    Console.WriteLine($"Program: {individualSanctionsCheckResponse.Results[0].Program}");
                    Console.WriteLine($"Additional Information: {individualSanctionsCheckResponse.Results[0].AdditionalInfo}");
                    Console.WriteLine($"Date Of Birth: {individualSanctionsCheckResponse.Results[0].DateOfBirth}");
                    Console.WriteLine($"Birth Year: {individualSanctionsCheckResponse.Results[0].BirthYear}");
                    Console.WriteLine($"Nationality: {individualSanctionsCheckResponse.Results[0].Nationality}");
                    Console.WriteLine($"Gender: {individualSanctionsCheckResponse.Results[0].Gender}");
                    Console.WriteLine($"Linked To: {individualSanctionsCheckResponse.Results[0].LinkedTo}");
                    Console.WriteLine($"Secondary Sanctions Risk: {individualSanctionsCheckResponse.Results[0].SecondarySanctionsRisk}");
                }

                #endregion

                #region Sanctions List Check - Entity

                var entitySanctionsCheckRequest = new EntitySanctionsCheckRequestM
                {
                    Name = "INTERNET RESEARCH AGENCY LLC"
                };
                var entitySanctionsCheckResponse = await sanctionsApi.EntityAsync(entitySanctionsCheckRequest);

                Console.WriteLine("\nEntity Sanction List Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine($"Matched: { entitySanctionsCheckResponse.Matched}");
                if (entitySanctionsCheckResponse.Match != null)
                {
                    Console.WriteLine($"Full Name: {entitySanctionsCheckResponse.Match[0].FullName}");
                    Console.WriteLine($"Program: {entitySanctionsCheckResponse.Match[0].Program}");

                    Console.WriteLine($"Alias List:");
                    entitySanctionsCheckResponse.Match[0].AliasList?.ForEach(x =>
                    {
                        Console.WriteLine(x.FullName);
                    });

                    Console.WriteLine($"Address List:");
                    entitySanctionsCheckResponse.Match[0].AddressList?.ForEach(y =>
                    {
                        Console.WriteLine($"{y.Address} {y.CityStateProvincePostalCode} {y.Country}");
                    });

                    Console.WriteLine($"Linked Individuals:");
                    entitySanctionsCheckResponse.Match[0].LinkedIndividuals?.ForEach(z =>
                    {
                        Console.WriteLine(z.FullName);
                    });

                    Console.WriteLine($"\nSecondary Sanctions Risk: {entitySanctionsCheckResponse.Match[0].SecondarySanctionsRisk}");
                }

                #endregion

                #region Sanctions List Check - Aircraft

                var aircraftSanctionsCheckRequest = new AircraftSanctionsCheckRequestM
                {
                    Name = "RA-02791"
                };
                var aircraftSanctionsCheckResponse = await sanctionsApi.AircraftAsync(aircraftSanctionsCheckRequest);

                Console.WriteLine("\nAircraft Sanction List Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine($"Matched: {aircraftSanctionsCheckResponse.Matched}");
                if (aircraftSanctionsCheckResponse.Match != null)
                {
                    Console.WriteLine($"Full Name: {aircraftSanctionsCheckResponse.Match[0].FullName}");
                    Console.WriteLine($"Program: {aircraftSanctionsCheckResponse.Match[0].Program}");
                    Console.WriteLine($"Manufacture Date: {aircraftSanctionsCheckResponse.Match[0].ManufactureDate}");
                    Console.WriteLine($"Model: {aircraftSanctionsCheckResponse.Match[0].Model}");
                    Console.WriteLine($"Operator: {aircraftSanctionsCheckResponse.Match[0].Operator}");
                    Console.WriteLine($"Mode S Transponder Code: {aircraftSanctionsCheckResponse.Match[0].ModeSTransponderCode}");
                    Console.WriteLine($"Serial Identification: {aircraftSanctionsCheckResponse.Match[0].SerialIdentification}");
                    Console.WriteLine($"Linked To: {aircraftSanctionsCheckResponse.Match[0].LinkedTo}");
                    if (aircraftSanctionsCheckResponse.Match[0].AliasList != null)
                    {
                        Console.WriteLine($"Alias: {aircraftSanctionsCheckResponse.Match[0].AliasList[0].FullName}");
                    }
                }

                #endregion

                #region Sanctions List Check - Vessel

                // Vessel Sanctions

                var vesselSanctionsCheckRequest = new VesselSanctionsCheckRequestM
                {
                    Name = "HWANG GUM SAN 2"
                };
                var vesselSanctionsCheckResponse = await sanctionsApi.VesselAsync(vesselSanctionsCheckRequest);

                Console.WriteLine("\nVessel Sanction List Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine($"Matched: {vesselSanctionsCheckResponse.Matched}");
                if (vesselSanctionsCheckResponse.Match != null)
                {
                    Console.WriteLine($"Full Name: {vesselSanctionsCheckResponse.Match[0].FullName}");
                    Console.WriteLine($"Program: {vesselSanctionsCheckResponse.Match[0].Program}");
                    Console.WriteLine($"Additional Info: {vesselSanctionsCheckResponse.Match[0].AdditionalInfo}");
                    Console.WriteLine($"Secondary Sanctions Risk: {vesselSanctionsCheckResponse.Match[0].SecondarySanctionsRisk}");
                    Console.WriteLine($"Vessel Type: {vesselSanctionsCheckResponse.Match[0].VesselType}");
                    Console.WriteLine($"Vessel Flag: {vesselSanctionsCheckResponse.Match[0].VesselFlag}");
                }

                #endregion

                #region Sanctions List Check - Crypto Wallet

                // Crypto Wallet Sanctions
                var cryptoWalletSanctionsCheckRequest = new CryptoWalletSanctionsCheckRequestM
                {
                    Address = "0X098B716B8AAF21512996DC57EB0615E2383E2F96"
                };
                var cryptoWalletSanctionsCheckResponse = await sanctionsApi.CryptoWalletAsync(cryptoWalletSanctionsCheckRequest);

                Console.WriteLine("\nCrypto Wallet Sanction List Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine($"Matched: {cryptoWalletSanctionsCheckResponse.Matched}");
                if (cryptoWalletSanctionsCheckResponse.EntityMatch != null)
                {
                    Console.WriteLine($"Entity Name: {cryptoWalletSanctionsCheckResponse.EntityMatch[0].FullName}");
                    Console.WriteLine($"Program: {cryptoWalletSanctionsCheckResponse.EntityMatch[0].Program}");
                    Console.WriteLine($"Additional Info: {cryptoWalletSanctionsCheckResponse.EntityMatch[0].AdditionalInfo}");
                    Console.WriteLine($"Secondary Sanctions Risk: {cryptoWalletSanctionsCheckResponse.EntityMatch[0].SecondarySanctionsRisk}");

                    Console.WriteLine($"Address List:");
                    if (cryptoWalletSanctionsCheckResponse.EntityMatch[0].AddressList != null)
                    {
                        cryptoWalletSanctionsCheckResponse.EntityMatch[0].AddressList.ForEach(x =>
                        {
                            Console.WriteLine($"{x.Address} {x.CityStateProvincePostalCode} {x.Country}");
                        });
                    }

                    Console.WriteLine($"Currency List:");
                    if (cryptoWalletSanctionsCheckResponse.EntityMatch[0].DigitalCurrencyAddress != null)
                    {
                        cryptoWalletSanctionsCheckResponse.EntityMatch[0].DigitalCurrencyAddress.ForEach(y =>
                        {
                            Console.WriteLine($"{y.Instrument}: {y.Address}");
                        });
                    }


                    Console.WriteLine($"Alias List:");
                    cryptoWalletSanctionsCheckResponse.EntityMatch[0].AlsoKnownAs?.ForEach(Console.WriteLine);
                }

                #endregion

                #endregion

                #region File Service Operations

                var fileApi = new FileApi(config);
                
                #endregion

                #region Upload File (PDF)
                var pdfFilePath = Directory.GetCurrentDirectory() + "\\exo-planets.pdf";
                var pdfFileGlobalId = string.Empty;
                using (var pdfFile = new FileStream(pdfFilePath, FileMode.Open))
                {
                    pdfFile.Position = 0;
                    var uploadResponse = await fileApi.UploadAsync(file: pdfFile);
                    pdfFileGlobalId = uploadResponse.GlobalId;
                    Console.WriteLine("\nUpload Response: ");
                    Console.WriteLine("-----------------------------------");

                    Console.WriteLine(uploadResponse.ToString());
                }
                #endregion

                #region Upload File (Image)
                var imageFilePath = Directory.GetCurrentDirectory() + "\\eiffel-tower.jpg";
                var imageFileGlobalId = string.Empty;
                using (var imageFile = new FileStream(imageFilePath, FileMode.Open))
                {
                    imageFile.Position = 0;
                    var uploadResponse = await fileApi.UploadAsync(file: imageFile);
                    imageFileGlobalId = uploadResponse.GlobalId;
                    Console.WriteLine("\nUpload Response: ");
                    Console.WriteLine("-----------------------------------");

                    Console.WriteLine(uploadResponse.ToString());
                }
                #endregion

                #region GetFileRecords

                var fileRecordsResponse = await fileApi.GetFileRecordsAsync();

                Console.WriteLine("\nGetFileRecord Response: ");
                Console.WriteLine("-----------------------------------");

                while(fileRecordsResponse.FileRecords.Any(c=>c.FileStatus == "PROCESSING"))
                {
                    Console.WriteLine("Awaing file processing.....");
                    await Task.Delay(2000);
                    fileRecordsResponse = await fileApi.GetFileRecordsAsync();
                }

                foreach (var fileRecord in fileRecordsResponse.FileRecords)
                {
                    Console.WriteLine(fileRecord.ToString());
                }

                #endregion

                #region GetFileRecord (Image)

                var imageFileRecordResponse = await fileApi.GetFileRecordAsync(imageFileGlobalId);

                Console.WriteLine("\nGetFileRecord Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine(imageFileRecordResponse.ToString());

                #endregion

                #region GetFileRecord (PDF)

                var pdfFileRecordResponse = await fileApi.GetFileRecordAsync(imageFileGlobalId);

                Console.WriteLine("\nGetFileRecord Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine(imageFileRecordResponse.ToString());

                #endregion

                #region Download (Image)

                var imageFileStream = await fileApi.DownloadAsync(imageFileGlobalId);

                Console.WriteLine("\nDownload Response (Image): ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"file GlobalId: {imageFileStream.GlobalId}");
                Console.WriteLine($"file ContentType: {imageFileStream.ContentType}");
                Console.WriteLine($"file Base64: {imageFileStream.Base64.Substring(0, 10)}.....");

                #endregion

                #region Download (PDF)

                var pdfFileStream = await fileApi.DownloadAsync(pdfFileGlobalId);

                Console.WriteLine("\nDownload Response (PDF): ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"file GlobalId: {imageFileStream.GlobalId}");
                Console.WriteLine($"file ContentType: {imageFileStream.ContentType}");
                Console.WriteLine($"file Base64: {imageFileStream.Base64.Substring(0, 10)}.....");

                #endregion

                #region Search Images
                var imageSearchFileRequest = new SearchFileRequestM(searchString: "Parisian Landmarks",fileTypeGroupId: 1);
                var imageSearchResults = await fileApi.SearchFileRecordsAsync(imageSearchFileRequest);

                Console.WriteLine("\nSearchFileRecords Response: ");
                Console.WriteLine("-----------------------------------");

                foreach (var fileRecord in imageSearchResults.FileRecords)
                {
                    Console.WriteLine(fileRecord.ToString());
                }
                #endregion

                #region Search Documents
                var documentSearchFileRequest = new SearchFileRequestM(searchString: "Information about space and the solar system", fileTypeGroupId: 4);
                var documentSearchResults = await fileApi.SearchFileRecordsAsync(documentSearchFileRequest);

                Console.WriteLine("\nSearchFileRecords Response: ");
                Console.WriteLine("-----------------------------------");

                foreach (var fileRecord in documentSearchResults.FileRecords)
                {
                    Console.WriteLine(fileRecord.ToString());
                }
                #endregion

                #region Document RAG

                var prompt = "Provide a list of the exoplanets covered in the document including their size and distance from earth where available";
                var ragRequest = new RagQueryRequestM
                {
                    FileGlobalId = pdfFileGlobalId,
                    FileTypeGroupId = 4,
                    SearchString = "Provide a list of the exoplanets covered in the document including their size and distance from earth where available"
                };

                var ragResponse = await fileApi.DocumentRagAsync(ragRequest);

                Console.WriteLine("\nDocument RAG Prompt: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(prompt);

                Console.WriteLine("\nDocument RAG Response: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(ragResponse.GeneratedText);


                #endregion

                Console.WriteLine("All done.  Press any key to close the console");
                Console.ReadLine();
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
}

```

### 3.4 Download sample files

To test the file upload service you will need to download the two sample files from the following locations.


```
https://docs.onboardingbuddy.co/sample-files/eiffel-tower.jpg
```

```
https://docs.onboardingbuddy.co/sample-files/exo-planets.pdf
```

Add these to the project folder and ensure that 'Copy to Output Directory' is set to 'Copy Always' 


### 3.5 Test the console app

After running the application you should see results similar to the following output:

```
Email Validation Response:
Message ID: <some-guid>
Correlation ID: <your-guid>
Email Address: support@onboardingbuddy.co
Email Status: 1
Free Email: False
Domain: onboardingbuddy.co
MX Found: True
Check Status: 1

....
```

## 4 Next Steps

For further information about this api action you can inspect the api documentation <a href="https://docs.onboardingbuddy.co/#tag/validation/operation/email">here</a> 