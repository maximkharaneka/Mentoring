using System;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace FileProcessorService
{
    public class DriveConnector
    {
        private static readonly string[] Scopes = {DriveService.Scope.DriveReadonly};
        private static readonly string ApplicationName = "Drive API .NET Quickstart";

        private static void Execute()
        {
            var service = GetDriveService();

            //// Define parameters of request.
            //var listRequest = service.Files.List();
            //listRequest.PageSize = 10;
            //listRequest.Fields = "nextPageToken, files(id, name)";

            //// List files.
            //var files = listRequest.Execute()
            //    .Files;
            //Console.WriteLine("Files:");
            //if (files != null && files.Count > 0)
            //{
            //    foreach (var file in files)
            //    {
            //        Console.WriteLine("{0} ({1})", file.Name, file.Id);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No files found.");
            //}
            //Console.Read();
        }

        public static DriveService GetDriveService()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                var credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
            return service;
        }
    }
}