using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

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
    }
}
