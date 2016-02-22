﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Admin.Models;


namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/ProductImages")]
    public class ProductImagesController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly string _productImageSaveFolder;

        #region ctor
        public ProductImagesController()
        {
            _repositoryManager = new RepositoryManager();
            _productImageSaveFolder = AppDomain.CurrentDomain.BaseDirectory + "\\Files\\Products";
        }
        #endregion

        // get product images by product id
        [Route("Products/{productId}")]
        public IEnumerable<ProductImage> Get(int productId)
        {
            return _repositoryManager.ProductImageRepository
                                           .Table
                                           .Where(m => m.ProductId == productId)
                                           .OrderBy(m => m.FileName)
                                           .ToList();

        }

        // upload image and create new ProductImage record
        [Route("Upload/{productId}/{imageTypeId}")]
        [HttpPost]
        public async Task<IHttpActionResult> UploadImage(int productId, short imageTypeId)
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

            // upload file root path
            // var root = AppDomain.CurrentDomain.BaseDirectory +  "\\Files\\Products";
            var provider = new MultipartFormDataStreamProvider(_productImageSaveFolder);

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

            // On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            // so this is how you can get the original file name
            var originalFileName = GetDeserializedFileName(file);
            var newFileName = GenerateImageFileName(productId) + Path.GetExtension(originalFileName);

            // todo: then we can rename the file into the originalfile name
            File.Copy(file.LocalFileName, Path.Combine(_productImageSaveFolder, newFileName), true);

            // delete the curent file
            File.Delete(file.LocalFileName);

            // create product image
            var newProductImage = new ProductImage
            {
                ProductId = productId,
                FileName = newFileName,
                ProductImageTypeId = imageTypeId // todo:                
            };

            _repositoryManager.ProductImageRepository.Create(newProductImage);
            await _repositoryManager.ProductImageRepository.SaveAsync();
                
            return Ok();
        }

        // remove product image and delete record from db
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var entityToDelete = await _repositoryManager.ProductImageRepository.GetAsync(id);

            // delete file
            File.Delete(Path.Combine(_productImageSaveFolder, entityToDelete.FileName));

            // delete recornd form db
            _repositoryManager.ProductImageRepository.Delete(entityToDelete);
            _repositoryManager.ProductImageRepository.Save();

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

        /// <summary>
        /// generate image file name based on the product id,
        /// sample: product id :12, product image file name: 12_1， 12_2....12_n
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        private string GenerateImageFileName(int productId)
        {
            // file name pattern
            string fileName = string.Format("{0}_", productId);

            // lookup last product image
            var lastProductImage = _repositoryManager.ProductImageRepository
                                                     .Table
                                                     .Where(m => m.ProductId == productId)
                                                     .OrderByDescending(m => m.ProductImageId)
                                                     .FirstOrDefault();

            if (lastProductImage == null)
            {
                return fileName + "1";
            }

            // extract the index from last product image
            var startIndex = lastProductImage.FileName.IndexOf("_") + 1;
            var endIndex = lastProductImage.FileName.LastIndexOf(".");

            var extractPart = lastProductImage.FileName.Substring(startIndex, (endIndex - startIndex));

            int i = 1;
            if (int.TryParse(extractPart, out i))
            {
                i += 1;
            }


            return fileName + i.ToString();
        }
    }
}
