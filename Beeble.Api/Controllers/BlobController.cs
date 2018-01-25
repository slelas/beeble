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
using System.Runtime.CompilerServices;
using Beeble.Data.Models;
using Beeble.Domain.Repositories;

namespace Beeble.Api.Controllers
{
    [RoutePrefix("blob")]
    public class BlobController : AuthorizationController
    {
	    private readonly BooksRepository repo = null;

		public BlobController()
	    {
		    repo = new BooksRepository();
	    }

		[HttpPost]
        [Route("")]
        public async Task<HttpResponseMessage> PostFormData()
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("BlobStorageConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            try
            {
                var provider = new MultipartFormDataStreamProvider(Path.GetTempPath());
                await Request.Content.ReadAsMultipartAsync(provider);

                var fileData = provider.FileData.FirstOrDefault();

                var formData = provider.FormData;

	            var blobUrl = SaveBlob(fileData).Url;

	            repo.AddNewBook(formData, blobUrl);
			}
            catch (Exception)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

	    private CustomBlob SaveBlob(MultipartFileData fileData)
	    {
			var container = GetBlobContainer();
		    var guid = Guid.NewGuid();
		    var fileName = guid.ToString();
		    var extension = Path.GetExtension(fileData.Headers.ContentDisposition.FileName.Trim('"')).ToLower();

			var blob = container.GetBlockBlobReference(fileName + extension);

			using (var fs = File.OpenRead(fileData.LocalFileName))
		    {
			    blob.UploadFromStream(fs);
		    }

		    var newBlob = new CustomBlob()
		    {
			    Url = blob.Uri.AbsoluteUri,
		    };

		    return newBlob;
	    }

		private CloudBlobContainer GetBlobContainer()
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