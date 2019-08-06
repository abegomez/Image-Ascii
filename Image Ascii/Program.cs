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
            string file = "C:\\Users\\ryuhyoko\\Source\\Repos\\Image Ascii\\Image Ascii\\ow.jpg";
            int imageWidth = 0;
            int imageHeight = 0;
            Image image = Image.FromFile(file);
            
            Console.WriteLine("image width: {0}", image.Width);
            Console.WriteLine("image height: {0}", image.Height);

            var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] photoByte = ms.ToArray();

            Color[,] pixelMatrix = To2dArray(photoByte, image.Width);
            Console.WriteLine("height: {0}", photoByte.Length / image.Width/3);
            
            string val;
            Console.Write("Enter integer: ");
            val = Console.ReadLine();
        }

        static int GetAverageRGB(Color c)
        {
            int avg = (c.R + c.G + c.B) / 3;
            return avg;
        }


        //TODO
        static int[,] ConvertToBrightnessMatrix(Color[,] mat)
        {
            int[,] = new int[mat.
        }
        static Color[,] To2dArray(byte[] source, int width)
        {
            int height = source.Length / width/3;
            Color[,] result = new Color[height,width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j+=3)
                {
                    int r = source[i * width + j];
                    int g = source[i * width + j+1];
                    int b = source[i * width + j+2];
                    Color c = Color.FromArgb(r, g, b);
                    result[i,j] = c;
                }
            }
            return result;
        }
    }
}
