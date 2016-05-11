using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace Gbmono.Common
{
    public class ImageHelper
    {
        /// <summary>
        /// create thumbnail with given with, height, quality and save on the disk
        /// </summary>
        /// <param name="sourceImageSrc"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mimeType"></param>
        /// <param name="pathToSave"></param>
        /// <param name="quality"></param>
        public static void CreateThumbnail(string sourceImageSrc, int width, int height, string mimeType, string pathToSave, int quality)
        {
            var image = Image.FromFile(sourceImageSrc); // load oringinal image file from specific path

            // the resized result bitmap
            using (var result = new Bitmap(width, height))
            {
                // get the graphics and draw the passed image to the result bitmap
                using (var grphs = Graphics.FromImage(result))
                {
                    grphs.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                    grphs.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    grphs.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    grphs.DrawImage(image, 0, 0, result.Width, result.Height);
                }

                // check the quality passed in
                if ((quality < 0) || (quality > 100))
                {
                    throw new ArgumentOutOfRangeException("quality", "quality must be 0, 100");
                }

                // The Quality category specifies the level of compression for an image. When used to construct an EncoderParameter, 
                // the range of useful values for the quality category is from 0 to 100. 
                // The lower the number specified, the higher the compression and therefore the lower the quality of the image. 
                // Zero would give you the lowest quality image and 100 the highest.
                var qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                var imageCode = ImageCodecInfo.GetImageEncoders().Where(i => i.MimeType.Equals(mimeType)).FirstOrDefault();

                //create a collection of EncoderParameters and set the quality parameter
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = qualityParam;
                //save the image using the codec and the encoder parameter
                result.Save(pathToSave, imageCode, encoderParams);
            }

            // dispose image
            image.Dispose();
        }
    }
}
