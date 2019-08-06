using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

namespace Image_Ascii
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "C:\\Users\\ryuhyoko\\Source\\Repos\\Image Ascii\\Image Ascii\\black-square.jpg";
            int imageWidth = 0;
            int imageHeight = 0;
            Image image = Image.FromFile(file);
            byte[] photoBytes = File.ReadAllBytes(file);
            
            // Format is automatically detected though can be changed.
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            Size size = new Size(150, 0);
            //using (MemoryStream inStream = new MemoryStream(photoBytes))
            //{
            //    using (MemoryStream outStream = new MemoryStream())
            //    {
            //        // Initialize the ImageFactory using the overload to preserve EXIF metadata.
            //        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
            //        {
            //            // Load, resize, set the format and quality and save an image.
            //            imageFactory.Load(inStream)
            //                        .Format(format)
            //                        .Save(outStream);
            //            imageWidth = imageFactory.Image.Width;
            //            imageHeight = imageFactory.Image.Height;
            //            Console.WriteLine("Image width:" + imageFactory.Image.Width);
            //            Console.WriteLine("Image height:" + imageFactory.Image.Height);

            //        }
            //        // Do something with the stream.
            //        //Console.WriteLine( "byte length:{0}", outStream.Length);

            //        //pixelMatrix = ConvertArray(image, imageWidth);
            //        string val;
            //        Console.Write("Enter integer: ");
            //        val = Console.ReadLine();
            //    }
            //}
            Console.WriteLine("image width: {0}", image.Width);
            Console.WriteLine("image height: {0}", image.Height);
            ImageConverter imgCon = new ImageConverter();
            byte[] photoByte = (byte[])imgCon.ConvertTo(image, typeof(byte[]));

            byte[,] pixelMatrix = To2dArray(photoByte, image.Width);
            string val;
            Console.Write("Enter integer: ");
            val = Console.ReadLine();
        }
        static byte[,] To2dArray(byte[] source, int width)
        {
            int height = source.Length / width;
            byte[,] result = new byte[height,width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    result[i,j] = source[i * width + j];
                }
            }
            return result;
        }
        public static byte[,] ConvertArray(byte[] Input, int size)
        {
            byte[,] Output = new byte[(int)(Input.Length / size), size];
            for (int i = 0; i < Input.Length; i += size)
            {
                for (int j = 0; j < size; j++)
                {
                    //Output[(int)(i / size), j] = Input[i + j];
                }
            }
            return Output;
        }
    }
}
