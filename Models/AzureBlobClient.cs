using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WorkApp.Models
{
    public class AzureBlobClient
    {
        public static async Task UploadBlob()
        {
            var connectionString = "<Enter the connection string here>";
            string containerName = "blobcontainer";
            var serviceClient = new BlobServiceClient(connectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            var path = @"c:\temp";
            var fileName = "Testfile.txt";
            var localFile = Path.Combine(path, fileName);
            await File.WriteAllTextAsync(localFile, "This is a test message");
            var blobClient = containerClient.GetBlobClient(fileName);
            Console.WriteLine("Uploading to Blob storage");
            using FileStream uploadFileStream = File.OpenRead(localFile);
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();
        }
    }
}
