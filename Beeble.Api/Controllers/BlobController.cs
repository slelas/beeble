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
	    private readonly BooksRepository booksRepo = null;
        private readonly LibrariesRepository librariesRepo = null;

        public BlobController()
	    {
		    booksRepo = new BooksRepository();
            librariesRepo = new LibrariesRepository();
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
                var containerName = formData["containerName"];

	            var blobUrl = SaveBlob(fileData, containerName).Url;

                if (containerName == "books")
                    booksRepo.AddNewBook(formData, blobUrl, UserId);
                else if (containerName == "members")
                    librariesRepo.AddNewLibraryMember(formData, blobUrl, UserId);
			}
            catch (Exception e)
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

	    private CustomBlob SaveBlob(MultipartFileData fileData, string containerName)
	    {
			var container = GetBlobContainer(containerName);
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

		private CloudBlobContainer GetBlobContainer(string containerName)
	    {
		    var blobStorageConnectionString = ConfigurationManager.AppSettings["BlobStorageConnectionString"];
		    var blobStorageContainerName = containerName;

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