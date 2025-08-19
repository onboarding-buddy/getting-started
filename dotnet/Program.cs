using OnboardingBuddyClient.Api;
using OnboardingBuddyClient.Client;
using OnboardingBuddyClient.Model;

namespace OnboardingBuddyConsole
{
    internal class Program
    {
        private const string PDF_FILE = "exo-planets.pdf";
        private const string IMAGE_FILE = "eiffel-tower.jpg";
        private const int DOCUMENT_FILETYPEGROUPID = 4;
        private const int IMAGE_FILETYPEGROUPID = 1;
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
                var choice = string.Empty;

                while (true)
                {
                    Console.WriteLine("Please select the test suite you would like to run.  Enter");
                    Console.WriteLine("1 for Validation Service");
                    Console.WriteLine("2 for Sanction Service");
                    Console.WriteLine("3 for File API (document processing)");
                    Console.WriteLine("4 for File API (image processing)");
                    Console.WriteLine("5 for File API (image/video generation)");

                    choice = Console.ReadLine();

                    _ = int.TryParse(choice, out var result);

                    try
                    {
                        switch (result)
                        {
                            case 1:
                                Console.WriteLine("\nRunning Validation Service Tests");
                                await CallValidationOperations(config);
                                break;
                            case 2:
                                Console.WriteLine("\nRunning Sanction Service Tests");
                                await CallSanctionOperations(config);
                                break;
                            case 3:
                                Console.WriteLine("\nRunning File API (document processing) Tests");
                                await CallFileOperations_Document(config);
                                break;
                            case 4:
                                Console.WriteLine("\nRunning File API (image processing) Tests");
                                await CallFileOperations_Image(config);
                                break;
                            case 5:
                                Console.WriteLine("\nRunning File API (image/video generation) Tests");
                                await CallFileOperations_FileGeneration(config);
                                break;
                            default:
                                break;
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
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

        public static async Task CallValidationOperations(Configuration config)
        {
            #region ValidationApi

            var validationApi = new ValidationApi(config);

            #region Validate Email Address

            var emailAddressRequest = new EmailAddressRequestM(emailAddress: "email@domain.com");

            Console.WriteLine("\n[1] Email Address Validation Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(emailAddressRequest.ToString());

            var emailAddressResponse = await validationApi.EmailAsync(emailAddressRequest);

            Console.WriteLine("\n[1] Email Address Validation Response: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(emailAddressResponse.ToString());
            #endregion

            #region Validate IP Address

            var ipAddressRequest = new IpAddressRequestM(ipAddress: "46.182.106.190");

            Console.WriteLine("\n[2] IP Address Validation Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(ipAddressRequest.ToString());
            
            var ipAddressResponse = await validationApi.IpaddressAsync(ipAddressRequest);

            Console.WriteLine("\n[2] IP Address Validation Response: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(ipAddressResponse.ToString());
            #endregion

            #region Validate User Agent

            var browserRequest = new BrowserRequestM(userAgent: "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36");

            Console.WriteLine("\n[3] User Agent Validation Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(browserRequest.ToString());
            var browserResponse = await validationApi.BrowserAsync(browserRequest);

            Console.WriteLine("\n[3] User Agent Validation Response: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(browserResponse.ToString());
            #endregion

            #region Validate Mobile Number
            var mobileNumber = new MobileNumberM(prefix: "61", number: "0422123456");
            var mobileNumberRequest = new MobileNumberRequestM(mobileNumber: mobileNumber);

            Console.WriteLine("\n[4] Mobile Number Validation Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(mobileNumberRequest.ToString());

            var mobileNumberResponse = await validationApi.MobileAsync(mobileNumberRequest);

            Console.WriteLine("\n[4] Mobile Number Validation Response: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(mobileNumberResponse.ToString());

            #endregion

            #endregion
        }

        public static async Task CallSanctionOperations(Configuration config)
        {
            #region SanctionsApi

            #region Sanctions List Check - Individual

            var sanctionsApi = new SanctionsApi(config);

            var individualSanctionsCheckRequest = new IndividualSanctionsCheckRequestM(
                firstName: "YEVGENIY",
                lastName: "PRIGOZHIN",
                birthYear: "1961"
            );

            Console.WriteLine("\n[1] Individual Sanction Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(individualSanctionsCheckRequest.ToString());

            var individualSanctionsCheckResponse = await sanctionsApi.IndividualAsync(individualSanctionsCheckRequest);

            Console.WriteLine("\n[1] Individual Sanction List Response: ");
            Console.WriteLine("-----------------------------------");

            Console.WriteLine($"Matched: {individualSanctionsCheckResponse.Matched}");

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

            Console.WriteLine("\n[2] Entity Sanction Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(entitySanctionsCheckRequest.ToString());

            var entitySanctionsCheckResponse = await sanctionsApi.EntityAsync(entitySanctionsCheckRequest);

            Console.WriteLine("\n[2] Entity Sanction List Response: ");
            Console.WriteLine("-----------------------------------");

            Console.WriteLine($"Matched: {entitySanctionsCheckResponse.Matched}");
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

            Console.WriteLine("\n[3] Aircraft Sanction Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(aircraftSanctionsCheckRequest.ToString());

            var aircraftSanctionsCheckResponse = await sanctionsApi.AircraftAsync(aircraftSanctionsCheckRequest);

            Console.WriteLine("\n[3] Aircraft Sanction List Response: ");
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

            Console.WriteLine("\n[4] Vessel Sanction Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(vesselSanctionsCheckRequest.ToString());

            var vesselSanctionsCheckResponse = await sanctionsApi.VesselAsync(vesselSanctionsCheckRequest);

            Console.WriteLine("\n[4] Vessel Sanction List Response: ");
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

            Console.WriteLine("\n[5] Crypto Wallet Sanction Request: ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(cryptoWalletSanctionsCheckRequest.ToString());

            var cryptoWalletSanctionsCheckResponse = await sanctionsApi.CryptoWalletAsync(cryptoWalletSanctionsCheckRequest);

            Console.WriteLine("\n[5] Crypto Wallet Sanction List Response: ");
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
        }

        public static async Task CallFileOperations_Document(Configuration config)
        {
            var fileApi = new FileApi(config);

            var pdfFilePath = Directory.GetCurrentDirectory() + "\\" + PDF_FILE;
            var pdfFileGlobalId = string.Empty;

            #region Upload File (PDF)

            Console.WriteLine($"\n[1] UploadAsync for file {PDF_FILE}");
            Console.WriteLine("--------------------------------------");

            using (var pdfFile = new FileStream(pdfFilePath, FileMode.Open))
            {
                pdfFile.Position = 0;
                var uploadResponse = await fileApi.UploadAsync(file: pdfFile);
                pdfFileGlobalId = uploadResponse.GlobalId;

                Console.WriteLine("\n[1] UploadAsync Response:");
                Console.WriteLine("---------------------------");
                Console.WriteLine(uploadResponse.ToString());
            }
            #endregion

            #region GetFileRecords

            Console.WriteLine("\n[2] Poll for completion of file processing");
            Console.WriteLine("\nPlease wait whilst tags, title, description and embeddings are created");
            Console.WriteLine("\nThis can take between 30-60 secs");
            Console.WriteLine("----------------------------------");

            var fileRecordsResponse = await fileApi.GetFileRecordsAsync();

            Console.WriteLine("\n[2] GetFileRecordsAsync Response:");
            Console.WriteLine("-----------------------------------");
            
            while (fileRecordsResponse.FileRecords.Any(c => c.FileStatus == "PROCESSING"))
            {
                Console.WriteLine("-Awaiting file processing.....");
                await Task.Delay(8000);
                fileRecordsResponse = await fileApi.GetFileRecordsAsync();
            }

            #endregion

            #region GetFileRecord (PDF)

            Console.WriteLine("\n[3] Retrieve file using GetFileRecordAsync for fileid {pdfFileGlobalId}");
            Console.WriteLine("------------------------------");
            var pdfFileRecordResponse = await fileApi.GetFileRecordAsync(pdfFileGlobalId);

            Console.WriteLine("\n[3] GetFileRecordAsync Response:");
            Console.WriteLine("------------------------------");
            Console.WriteLine(pdfFileRecordResponse.ToString());

            #endregion

            #region Download (PDF)

            Console.WriteLine($"\n[4] Download file using DownloadAsync for fileid {pdfFileGlobalId}");
            Console.WriteLine("--------------------------------------------------------------------");

            var pdfFileDownloadRecord = await fileApi.DownloadAsync(pdfFileGlobalId);

            Console.WriteLine("\n[4] Download Response (PDF):");
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Presigned Url: {pdfFileDownloadRecord.PreSignedUrl}");

            #endregion

            #region Search Documents
            var documentSearchFileRequest = new SearchFileRequestM(searchString: "Information about space and the solar system", fileTypeGroupId: DOCUMENT_FILETYPEGROUPID);

            Console.WriteLine("\n[5] Semantic search of documents for query:");
            Console.WriteLine(documentSearchFileRequest.SearchString);

            var documentSearchResults = await fileApi.SearchFileRecordsAsync(documentSearchFileRequest);

            Console.WriteLine("\n[5] SearchFileRecords Response:");
            Console.WriteLine("---------------------------------");

            foreach (var fileRecord in documentSearchResults.FileRecords)
            {
                Console.WriteLine(fileRecord.ToString());
            }
            #endregion

            #region Document RAG

            var ragPrompt = "Provide a list of the exoplanets covered in the document including their size and distance from earth where available.  Provide one technology that would be required to facilitate the journey using external sources if required.";
            var ragRequest = new RagQueryRequestM
            {
                FileGlobalId = pdfFileGlobalId,
                FileTypeGroupId = DOCUMENT_FILETYPEGROUPID,
                SearchString = ragPrompt
            };

            Console.WriteLine("\n[6] Performing a RAG Query for prompt:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(ragPrompt);

            var ragResponse = await fileApi.DocumentRagAsync(ragRequest);

            Console.WriteLine("\n[6] RAG Query Response:");
            Console.WriteLine("-------------------------");
            Console.WriteLine(ragResponse.GeneratedText);

            #endregion
        }

        public static async Task CallFileOperations_Image(Configuration config)
        {
            var fileApi = new FileApi(config);
            var imageFilePath = Directory.GetCurrentDirectory() + "\\" + IMAGE_FILE;
            var imageFileGlobalId = string.Empty;

            #region Upload File (Image)

            Console.WriteLine($"\n[1] UploadAsync for file {IMAGE_FILE}");
            Console.WriteLine("--------------------------------------");

            using (var imageFile = new FileStream(imageFilePath, FileMode.Open))
            {
                imageFile.Position = 0;
                var uploadResponse = await fileApi.UploadAsync(file: imageFile);
                imageFileGlobalId = uploadResponse.GlobalId;

                Console.WriteLine("\n[1] UploadAsync Response:");
                Console.WriteLine("-----------------------");
                Console.WriteLine(uploadResponse.ToString());
            }
            #endregion

            #region GetFileRecords

            Console.WriteLine("\n[2] Poll for completion of file processing");
            Console.WriteLine("\nPlease wait whilst tags, title, description and embeddings are created");
            Console.WriteLine("\nThis can take between 30-60 secs");
            Console.WriteLine("----------------------------------");

            var fileRecordsResponse = await fileApi.GetFileRecordsAsync();

            Console.WriteLine("\n[2] GetFileRecordsAsync Response:");
            Console.WriteLine("-----------------------------------");

            while (fileRecordsResponse.FileRecords.Any(c => c.FileStatus == "PROCESSING"))
            {
                Console.WriteLine("-Awaiting file processing.....");
                await Task.Delay(3000);
                fileRecordsResponse = await fileApi.GetFileRecordsAsync();
            }

            #endregion

            #region GetFileRecord (Image)

            Console.WriteLine("\n[3] Retrieve file using GetFileRecordAsync for fileid {imageFileGlobalId}");
            Console.WriteLine("------------------------------");

            var imageFileRecordResponse = await fileApi.GetFileRecordAsync(imageFileGlobalId);

            Console.WriteLine("\n[3] GetFileRecord Response:");
            Console.WriteLine("-------------------------");

            Console.WriteLine(imageFileRecordResponse.ToString());

            #endregion

            #region Download (Image)


            Console.WriteLine($"\n[4] Download file using DownloadAsync for fileid {imageFileGlobalId}");
            Console.WriteLine("--------------------------------------------------------------------");

            var imageFileDownloadRecord = await fileApi.DownloadAsync(imageFileGlobalId);

            Console.WriteLine("\n[4] Download Response (Image):");
            Console.WriteLine("--------------------------");
            Console.WriteLine($"Presigned Url: {imageFileDownloadRecord.PreSignedUrl}");

            #endregion

            #region Search Images
            var imageSearchFileRequest = new SearchFileRequestM(searchString: "Parisian Landmarks", fileTypeGroupId: IMAGE_FILETYPEGROUPID);
            
            Console.WriteLine("\n[5] Semantic search of images for query:");
            Console.WriteLine(imageSearchFileRequest.SearchString);
           
            var imageSearchResults = await fileApi.SearchFileRecordsAsync(imageSearchFileRequest);

            Console.WriteLine("\n[5] SearchFileRecords Response:");
            Console.WriteLine("---------------------------------");

            foreach (var fileRecord in imageSearchResults.FileRecords)
            {
                Console.WriteLine(fileRecord.ToString());
            }
            #endregion

            #region Generate Video From Image
            var prompt = "Animate the clouds behind the eiffel tower";
            var generateVideoRequest = new GenerateVideoRequestM(prompt: prompt, imageFileGlobalId: imageFileGlobalId, idempotencyKey:Guid.NewGuid().ToString());

            Console.WriteLine("\n[6] Generate video from image with prompt:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(prompt);

            var generateVideoResponse = await fileApi.GenerateVideoAsync(generateVideoRequest);

            Console.WriteLine("\n[6] Poll until generation complete:");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("\nThis can take between 60-90 secs");
            Console.WriteLine("\nPlease wait whilst video, tags, title, description are created");

            var pollFileResponse = await fileApi.PollFileAsync(generateVideoResponse.FileGlobalId);

            while (pollFileResponse.FileStatus != "PROCESSED" && pollFileResponse.FileStatus != "FAILED")
            {
                Console.WriteLine("- Awaiting file processing.....");
                await Task.Delay(8000);
                pollFileResponse = await fileApi.PollFileAsync(generateVideoResponse.FileGlobalId);
            }

            if (pollFileResponse.FileStatus == "PROCESSED")
            {
                var generatedFileDownloadRecord = await fileApi.DownloadAsync(generateVideoResponse.FileGlobalId);

                Console.WriteLine("\nDownload Response:");
                Console.WriteLine("--------------------------");
                Console.WriteLine($"Presigned Url: {generatedFileDownloadRecord.PreSignedUrl}");
            }
            else
            {
                Console.WriteLine("\nProcessing Failed Please Try Again");

            }

            #endregion

        }

        public static async Task CallFileOperations_FileGeneration(Configuration config)
        {
            var fileApi = new FileApi(config);
            
            #region Generate Video
            var videoPrompt = "Create a cinematic scene based on the following **Droid Concept:** A Little Droid that comes to life.**Quirky Characteristics:** This droid is activated by pushing its nose. Once activated, it sprouts arms and legs and begins marching around uncontrollably, knocking things over with manic energy. It only reverts to its dormant state when its nose is pushed again.**Personality:** It has a chaotic, disruptive personality. **Prompt Details for Video Generation:***   **Subject:** A Little Droid*   **Action:** Being activated and marching around with jerky movements, knocking over various small objects in a cluttered junk shop.*   **Setting:** A cluttered, dusty junk shop (similar to Watto is shop).*   **Lighting:** Bright, slightly harsh lighting.*   **Personality:** Chaotic, erratic, destructive.*   **Additional Details:** The droid should have a simple, almost toy-like design, with mismatched parts. **Camera movement:** Quick pan to the droid being activated, then shaky cam following its chaotic movements, ending with a snap zoom as it causes a small collision.";
            var generateVideoRequest = new GenerateVideoRequestM(prompt: videoPrompt, idempotencyKey: Guid.NewGuid().ToString());
            var generateVideoResponse = await fileApi.GenerateVideoAsync(generateVideoRequest);

            Console.WriteLine("\n[1] Generate video with prompt:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine(videoPrompt);

            Console.WriteLine("\n[1] Poll until generation complete:");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("\nThis can take between 60-90 secs");
            Console.WriteLine("\nPlease wait whilst video, tags, title, description are created");

            var pollFileResponse = await fileApi.PollFileAsync(generateVideoResponse.FileGlobalId);

            while (pollFileResponse.FileStatus != "PROCESSED" && pollFileResponse.FileStatus != "FAILED")
            {
                Console.WriteLine("Awaiting file processing.....");

                await Task.Delay(8000);
                pollFileResponse = await fileApi.PollFileAsync(generateVideoResponse.FileGlobalId);
            }

            if (pollFileResponse.FileStatus == "PROCESSED")
            {
                Console.WriteLine($"\n[2] Download FileId {generateVideoResponse.FileGlobalId}");
                Console.WriteLine("-----------------------------------------------------------");

                var generatedFileDownloadRecord = await fileApi.DownloadAsync(generateVideoResponse.FileGlobalId);

                Console.WriteLine("\n[2] Download Response:");
                Console.WriteLine("------------------------");
                Console.WriteLine($"Presigned Url: {generatedFileDownloadRecord.PreSignedUrl}");
            }
            else
            {
                Console.WriteLine("\nProcessing Failed Please Try Again");

            }

            #endregion

            #region Generate Image
            var imagePrompt = "Create an artwork depicting the Eiffel Tower in the style of Henri Matisse";
            var generateImageRequest = new GenerateImageRequestM(prompt: imagePrompt, idempotencyKey: Guid.NewGuid().ToString());
            var generateImageResponse = await fileApi.GenerateImageAsync(generateImageRequest);

            Console.WriteLine("\n[3] Generate image with prompt:");
            Console.WriteLine("---------------------------------");
            Console.WriteLine(imagePrompt);

            Console.WriteLine("\n[3]Poll until generation complete:");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("\nThis can take between 45-60 secs");
            Console.WriteLine("\nPlease wait whilst images, tags, title, description and embeddings are created");

            var pollFileResponseImage = await fileApi.PollFileAsync(generateImageResponse.FileGlobalId);

            while (pollFileResponseImage.FileStatus != "PROCESSED" && pollFileResponseImage.FileStatus != "FAILED")
            {
                Console.WriteLine("Awaiting file processing.....");
                await Task.Delay(8000);
                pollFileResponseImage = await fileApi.PollFileAsync(generateImageResponse.FileGlobalId);
            }

            if (pollFileResponseImage.FileStatus == "PROCESSED")
            {
                Console.WriteLine($"\n[4] Download FileId {generateImageResponse.FileGlobalId}");
                Console.WriteLine("-----------------------------------------------------------");

                var generatedFileDownloadRecord = await fileApi.DownloadAsync(generateImageResponse.FileGlobalId);

                Console.WriteLine("\n[4] Download Response:");
                Console.WriteLine("------------------------");
                Console.WriteLine($"Presigned Url: {generatedFileDownloadRecord.PreSignedUrl}");
            }
            else
            {
                Console.WriteLine("\nProcessing Failed Please Try Again");

            }

            #endregion
        }
    }
}