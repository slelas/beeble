using Beeble.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.Azure; //Namespace for CloudConfigurationManager
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.Storage.Auth;

namespace Beeble.Api.Controllers
{
     [RoutePrefix("blob")]
     public class BlobController : AuthorizationController
     {
        //public async Task<IHttpActionResult> UploadFile()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        return StatusCode(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

        //    foreach (var stream in filesReadToProvider.Contents)
        //    {
        //        var fileBytes = await stream.ReadAsByteArrayAsync();
        //    }


        //    return Ok();
        //}

        //private readonly IBlobService _service = new BlobService(new AuthContext());
        //private readonly IBlobCommands _blobCommands;
        //private readonly IBlobQueries _blobQueries;

        //[HttpPost]
        //[Route("upload")]
        //public async Task<IHttpActionResult> PostBlobUpload()
        //{
        //    try
        //    {
        //        //var provider = new BlobStorageUploadProvider();
        //        //await Request.Content.ReadAsMultipartAsync(provider);

        //        var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

        //        var provider = new MultipartMemoryStreamProvider();
        //        var fileData = provider.FileData.First();

        //        var blob = _blobCommands.SaveBlob(fileData);
        //        //var receipt = ExtractData.ExtractReceipt(provider.FormData);

        //        receipt.Username = Username;
        //        receipt.FilePath = blob.URI;

        //        _receiptCommands.CreateReceipt(Username, receipt, blob);

        //        return Ok(blob.Id);
        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError(ex);
        //    }
        //}

            private const string Container = "images";

        [HttpGet, Route("t")]
        public bool Test()
        {
            var a = 5;
            return true;

        }

            [HttpPost, Route("test")]
        public async Task<IHttpActionResult> UploadFile()
            {
                if (!Request.Content.IsMimeMultipartContent("form-data"))
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var accountName = ConfigurationManager.AppSettings["storage:account:name"];
                var accountKey = ConfigurationManager.AppSettings["storage:account:key"];
            //var storageAccount = new CloudStorageAccount(new StorageCredentials(accountName, accountKey), true);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                CloudBlobContainer imagesContainer = blobClient.GetContainerReference(Container);
            imagesContainer.CreateIfNotExists();
            var provider = new AzureStorageMultipartFormDataStreamProvider(imagesContainer);

                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);
                }
                catch (Exception ex)
                {
                    return BadRequest($"An error has occured. Details: {ex.Message}");
                }

                // Retrieve the filename of the file you have uploaded
                var filename = provider.FileData.FirstOrDefault()?.LocalFileName;
                if (string.IsNullOrEmpty(filename))
                {
                    return BadRequest("An error has occured while uploading your file. Please try again.");
                }

                return Ok($"File: {filename} has successfully uploaded");
            }
        }

        public class AzureStorageMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            private readonly CloudBlobContainer _blobContainer;
            private readonly string[] _supportedMimeTypes = { "image/png", "image/jpeg", "image/jpg" };

            public AzureStorageMultipartFormDataStreamProvider(CloudBlobContainer blobContainer) : base("azure")
            {
                _blobContainer = blobContainer;
            }

            public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
            {
                if (parent == null) throw new ArgumentNullException(nameof(parent));
                if (headers == null) throw new ArgumentNullException(nameof(headers));

                if (!_supportedMimeTypes.Contains(headers.ContentType.ToString().ToLower()))
                {
                    throw new NotSupportedException("Only jpeg and png are supported");
                }

                // Generate a new filename for every new blob
                var fileName = Guid.NewGuid().ToString();

                CloudBlockBlob blob = _blobContainer.GetBlockBlobReference(fileName);

                if (headers.ContentType != null)
                {
                    // Set appropriate content type for your uploaded file
                    blob.Properties.ContentType = headers.ContentType.MediaType;
                }

                this.FileData.Add(new MultipartFileData(headers, blob.Name));

                return blob.OpenWrite();
            }

        //isto kao tutorial
        public static class BlobHelper
        {
            public static CloudBlobContainer GetBlobContainer()
            {
                var blobStorageConnectionString = ConfigurationManager.AppSettings["BlobStorageConnectionString"];
                var blobStorageContainerName = ConfigurationManager.AppSettings["BlobStorageContainerName"];

                var blobStorageAccount = CloudStorageAccount.Parse(blobStorageConnectionString);
                var blobClient = blobStorageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(blobStorageContainerName);
                container.CreateIfNotExists();

                container.SetPermissions(
                    new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                return container;
            }
        }
    }
}
