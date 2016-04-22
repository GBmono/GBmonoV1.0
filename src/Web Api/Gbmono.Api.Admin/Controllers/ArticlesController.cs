using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Admin.Models;

namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/Articles")]
    public class ArticlesController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;
        // images, static files host in seperate IIS app
        private readonly string _articleImageSaveFolder = ConfigurationManager.AppSettings["ImgPath"] + "\\articles";

        #region ctor
        public ArticlesController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        [Route("{from}/{to}/{type}")]
        public async Task<IEnumerable<Article>> GetByDate(DateTime from, DateTime to, short type)
        {
            return await _repositoryManager.ArticleRepository
                                           .Table
                                           .Where(m => m.ArticleId == type &&
                                                       m.ModifiedDate >= from &&
                                                       m.ModifiedDate < to)
                                           .OrderByDescending(m => m.ModifiedDate)
                                           .ToListAsync();
        }

        // return image list by article id 
        [Route("BrowseImages")]
        public IEnumerable<KendoUploadImg> BrowseFiles(int id)
        {
            // upload file path
            // check if upload folder exists
            var imgDirectory = Path.Combine(_articleImageSaveFolder, id.ToString());

            List<KendoUploadImg> images = new List<KendoUploadImg>();

            // get file list from the directory
            var files = Directory.EnumerateFiles(imgDirectory);

            // iterate each img file
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                int size = file.Length;
                string filename = Path.GetFileName(file);

                var image = new KendoUploadImg
                {
                    Name = filename,
                    Size = size,
                    Type = "f"
                };

                images.Add(image);
            }

            return images;
        }

        // upload image from kendo ui editor image browser
        [Route("Upload")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadImage(int id)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType)
                {
                    RequestMessage = Request,
                    Content = new StringContent("The request doesn't contain multipart/form-data.")
                });
            }

            // upload image root path
            var provider = new MultipartFormDataStreamProvider(_articleImageSaveFolder);

            // Read the form data
            await Request.Content.ReadAsMultipartAsync(provider);

            // load the files
            // at the moment, we only process one file
            var file = provider.FileData.FirstOrDefault();

            if (file == null)
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    RequestMessage = Request,
                    Content = new StringContent("Failed to upload file.")
                });
            }

            // todo: validate file extension to avoid files of other types being upload

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(file);

            // build the new name of the imag file by datetime stamp
            var newFileName = DateTime.Now.ToString("yyyyMMddhhmmss") + Path.GetExtension(originalFileName);

            // create sub directory (article id) if it doesn't exist
            // saving directory
            var imgDirectory = Path.Combine(_articleImageSaveFolder, id.ToString());
            // create if directory doesn't exist
            if (!Directory.Exists(imgDirectory))
            {
                Directory.CreateDirectory(imgDirectory);
            }

            // copy the uploaded img into article folder
            File.Copy(file.LocalFileName, Path.Combine(imgDirectory, newFileName), true);

            // delete the oringinal img file from 
            File.Delete(file.LocalFileName);

            return Ok();
        }

        /// <summary>
        /// get the deserialized file name (oringinal file name) from the upload file data
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = fileData.Headers.ContentDisposition.FileName;
            // IE fix
            // when using IE to upload file, the ContentDisposition.FileName contains the full local path of the file
            if (fileName.Contains("\\"))
            {
                // remove the path and only keep the file name in json format
                var lastIndexOfPathCharacter = fileName.LastIndexOf("\\");
                fileName = "\"" + fileName.Substring(lastIndexOfPathCharacter + 1); // json format
            }

            return JsonConvert.DeserializeObject(fileName).ToString();
        }
    }
}
