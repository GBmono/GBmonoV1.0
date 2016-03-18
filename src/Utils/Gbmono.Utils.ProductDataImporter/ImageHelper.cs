using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.Infrastructure;

namespace Gbmono.Utils.ProductDataImporter
{
    public static class ImageHelper
    {
        public static bool ValidateImageQualityByPixel(Stream imageStream)
        {
            var imagePixel = 0;
            try
            {
                Image pic = Image.FromStream(imageStream);
                imagePixel = pic.Width * pic.Height;
            }
            catch (Exception ex)
            {
                imagePixel = -1;
            }
            if (imagePixel > 0 && imagePixel < 100)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static void RemoveWrongImage(RepositoryManager _repositoryManager)
        {
            var images = _repositoryManager.ProductImageRepository.Table.ToList();
            var imageFileFolder = ConfigurationManager.AppSettings["imageFolder"];

            Console.WriteLine("all images:" + images.Count());
            foreach (var productImage in images)
            {
                Console.WriteLine("strat image:" + productImage.ProductImageId);
                var imagePath = $@"{imageFileFolder}/{productImage.FileName}";

                bool imageValidated = true;
                using (Stream stream = new FileStream(imagePath, FileMode.Open))
                {
                    imageValidated = ImageHelper.ValidateImageQualityByPixel(stream);
                }
                if (!imageValidated)
                {
                    try
                    {
                        File.Delete(imagePath);
                        _repositoryManager.ProductImageRepository.Delete(productImage);
                        _repositoryManager.ProductImageRepository.Save();
                        Console.WriteLine("Remove Image:" + imagePath);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Remove Image Error:" + ex);
                    }
                }

            }


        }

    }
}
