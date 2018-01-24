using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.Azure; //Namespace for CloudConfigurationManager
using System.Configuration;
using System.IO;
using System.Net.Http.Headers;

namespace Beeble.Api.Controllers
{
    [RoutePrefix("blob")]
    public class BlobController : AuthorizationController
    {

        [HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var provider = new BlobStorageMultipartStreamProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                var fileData = provider.FileData.FirstOrDefault();
                var b = provider.FormData;
                var blobUrl = BlobStorageMultipartStreamProvider.BlobUrl;


            }
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }

    public class BlobStorageMultipartStreamProvider : MultipartFormDataStreamProvider
    {

        public static string BlobUrl { get; set; }

        public BlobStorageMultipartStreamProvider() : base(Path.GetTempPath())
        {

        }

        public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
        {
            Stream stream = null;
            ContentDispositionHeaderValue contentDisposition = headers.ContentDisposition;

            if (contentDisposition != null)
            {
                if (!String.IsNullOrWhiteSpace(contentDisposition.FileName) || true)
                {
                    string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
                    string containerName = "test22";
                    CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
                    //CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer blobContainer = blobClient.GetContainerReference(containerName);
                    blobContainer.CreateIfNotExists();

                    //zbog ovoga nije bilo radilo
                    blobContainer.SetPermissions(
        new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                    //contentDisposition.FileName = contentDisposition.FileName.Trim('"');
                    contentDisposition.FileName = contentDisposition.Name;
                    CloudBlockBlob blob = blobContainer.GetBlockBlobReference(contentDisposition.FileName);
                    stream = blob.OpenWrite();
                    var blobUrl = blob.Uri.AbsoluteUri;
                        BlobUrl = blobUrl;

                }
            }
            return stream;
        }
    }
}