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
        static string ASCIIChars = "`^\",:;Il!i~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
        static void Main(string[] args)
        {
            string file = "C:\\Users\\ryuhyoko\\Source\\Repos\\Image Ascii\\Image Ascii\\ow.jpg";
            string ASCIIChars = "`^\",:;Il!i~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
            
            Image image = Image.FromFile(file);
            
            Console.WriteLine("image width: {0}", image.Width);
            Console.WriteLine("image height: {0}", image.Height);

            var ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] photoByte = ms.ToArray();

            Color[,] pixelMatrix = To2dArray(photoByte, image.Width);
            Console.WriteLine("height: {0}", photoByte.Length / image.Width/3);
            Console.WriteLine("width: {0}", image.Width);
            int[,] brightnessMatrix = ConvertToBrightnessMatrix(pixelMatrix);


            char[,] asciiMatrix = ConvertBrightnessToASCII(brightnessMatrix);
            for (int i = 0; i < asciiMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < asciiMatrix.GetLength(1); j++)
                {
                    Console.Write("{0}{0}{0}", asciiMatrix[i, j]);
                }
                Console.WriteLine();
            }
            string val;
            Console.Write("Enter integer: ");
            val = Console.ReadLine();
        }

        static int GetAverageRGB(Color c)
        {
            int avg = (c.R + c.G + c.B) / 3;
            return avg;
        }


        static  char[,] ConvertBrightnessToASCII(int[,] input)
        {
            int height = input.GetLength(0);
            int width = input.GetLength(1);
            char[,] result = new char[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    double val = (input[i, j] / 256.0) * 65.0;
                    //int val = (input[i, j] / 256) * 65;
                    
                    result[i, j] = ASCIIChars[(int)Math.Floor(val)];
                }
            }
            Console.WriteLine("Conversion to asciimatrix completed.");
            return result;
        }

        static int[,] ConvertToBrightnessMatrix(Color[,] mat)
        {           
            int[,] result = new int[mat.GetLength(0), mat.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    result[i, j] = GetAverageRGB(mat[i, j]);
                    //Console.Write("{0} ", result[i, j]);
                }
                //Console.WriteLine();
            }
            Console.WriteLine("Conversion to Brightness Matrix complete.");
            return result;
        }

        static Color[,] To2dArray(byte[] source, int width)
        {
            int height = source.Length / width/3;
            int w = width/3;
            Color[,] result = new Color[height,w];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j+=3)
                {
                    int r = source[i * width + j];
                    int g = source[i * width + j+1];
                    int b = source[i * width + j+2];
                    Color c = Color.FromArgb(r, g, b);
                    result[i, j / 4] = c;
                }
            }
            Console.WriteLine("Conversion to 2d Color array completed.");
            return result;
        }
    }
}
