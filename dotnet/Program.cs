using OnboardingBuddyClient.Api;
using OnboardingBuddyClient.Client;
using OnboardingBuddyClient.Model;

namespace OnboardingBuddyConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var onboardingBuddyConfiguration = new OnboardingBuddyConfiguration()
            {
                AppKey = Environment.GetEnvironmentVariable("OB_APP_KEY"),
                ApiKey = Environment.GetEnvironmentVariable("OB_API_KEY"),
                ApiSecret = Environment.GetEnvironmentVariable("OB_API_SECRET"),
            }; 

            try
            {
                #region ValidationApi

                var validationApi = new ValidationApi(onboardingBuddyConfiguration);

                #region Validate Email Address
                var emailAddressRequest = new EmailAddressRequestM { EmailAddress = "email@domain.com" };
                var emailAddressResponse = await validationApi.EmailAsync(emailAddressRequest);

                Console.WriteLine("Email Address Validation Response: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(emailAddressResponse.ToString());
                #endregion

                #region Validate IP Address
                var ipAddressRequest = new IpAddressRequestM { IpAddress = "46.182.106.190" };
                var ipAddressResponse = await validationApi.IpaddressAsync(ipAddressRequest);

                Console.WriteLine("\nIP Address Validation Response: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(ipAddressResponse.ToString());
                #endregion

                #region Validate User Agent
                var browserRequest = new BrowserRequestM { UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36" };
                var browserResponse = await validationApi.BrowserAsync(browserRequest);

                Console.WriteLine("\nUser Agent Validation Response: ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(browserResponse.ToString());
                #endregion

                #region Validate Mobile Number

                var mobileNumberRequest = new MobileNumberRequestM
                {
                    MobileNumber = new MobileNumberM
                    {
                        Prefix = "61",
                        Number = "0422123456"
                    }
                };

                var mobileNumberResponse = await validationApi.MobileAsync(mobileNumberRequest);

                Console.WriteLine("\nMobile Number Validation Response: ");
                Console.WriteLine("-----------------------------------"); 
                Console.WriteLine(mobileNumberResponse.ToString());

                #endregion

                #endregion

                #region SanctionsApi

                #region Sanctions List Check - Individual

                var sanctionsApi = new SanctionsApi(onboardingBuddyConfiguration);

                var individualSanctionsCheckRequest = new IndividualSanctionsCheckRequestM { 
                    FirstName = "YEVGENIY",
                    LastName = "PRIGOZHIN",
                    BirthYear = "1961"
                };
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

                #region Email Validation with Sanctions List Check

                // Email Validation - With Sanctions
                var emailAddressSanctionRequest = new EmailAddressRequestM { EmailAddress = "AFRICONLINE@PROTONMAIL.COM" };
                var emailAddressSanctionResponse = await validationApi.EmailAsync(emailAddressSanctionRequest);

                Console.WriteLine("\nEmail Validation with Sanction List Response: ");
                Console.WriteLine("-----------------------------------");

                Console.WriteLine("Email Validation Response:");
                Console.WriteLine($"Message ID: {emailAddressSanctionResponse.MessageId}");
                Console.WriteLine($"Correlation ID: {emailAddressSanctionResponse.CorrelationId}");
                Console.WriteLine($"Email Address: {emailAddressSanctionResponse.EmailAddress}");
                Console.WriteLine($"Email Status: {emailAddressSanctionResponse.EmailStatus}");
                Console.WriteLine($"Free Email: {emailAddressSanctionResponse.FreeEmail}");
                Console.WriteLine($"Domain: {emailAddressSanctionResponse.Domain}");
                Console.WriteLine($"MX Found: {emailAddressSanctionResponse.MxFound}");
                Console.WriteLine($"Check Status: {emailAddressSanctionResponse.CheckStatus}");

                if (emailAddressSanctionResponse.SanctionRecord?.EntityMatch != null)
                {
                    Console.WriteLine($"Entity Name: {emailAddressSanctionResponse.SanctionRecord.EntityMatch[0].FullName}");
                    Console.WriteLine($"Program: {emailAddressSanctionResponse.SanctionRecord.EntityMatch[0].Program}");
                    Console.WriteLine($"Secondary Sanctions Risk: {emailAddressSanctionResponse.SanctionRecord.EntityMatch[0].SecondarySanctionsRisk}");

                    Console.WriteLine($"Alias List:");
                    emailAddressSanctionResponse.SanctionRecord.EntityMatch[0].AlsoKnownAs?.ForEach(Console.WriteLine);

                    Console.WriteLine($"Linked Individuals:");
                    emailAddressSanctionResponse.SanctionRecord.EntityMatch[0].LinkedIndividuals?.ForEach(z =>
                    {
                        Console.WriteLine($"Full Name: {z.FullName}");
                        Console.WriteLine($"Linked To: {z.LinkedTo}");
                    });
                }

                #endregion

                #endregion

                Console.WriteLine("");
                Console.WriteLine("All done please refer to the API documentation for further request/response information");
                Console.WriteLine("Press any key to exit");
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
