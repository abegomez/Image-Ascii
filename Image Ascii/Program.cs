﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

namespace Image_Ascii
{
    static class Program
    {
        static string ASCIIChars = "`^\",:;Il!i~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
        [STAThread]
        static void Main(string[] args)
        {
            string file = "";
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Multiselect = false;
            OFD.Title = "Open Excel Document";
            OFD.Filter = "Image Document|*.bmp;*.jpg";
            OFD.ShowDialog();
            file= OFD.FileName;

            //string file = "C:\\Users\\ryuhyoko\\Source\\Repos\\Image Ascii\\Image Ascii\\OSP5BZ7QI3AF1558647405218.jpg";
            string ASCIIChars = "`^\",:;Il!i~+_-?][}{1)(|\\/tfjrxnuvczXYUJCLQ0OZmwqpdbkhao*#MW&8%B@$";
            
            byte[] photoBytes = File.ReadAllBytes(file);

            Bitmap bmp = ResizeBMP(photoBytes);
            Color[,] pixelMatrix = BmpTo2dArray(bmp);
            
            Console.WriteLine("image width: {0}", bmp.Width);
            Console.WriteLine("image height: {0}", bmp.Height);
                        
            int[,] brightnessMatrix = ConvertToBrightnessMatrix(pixelMatrix);

            char[,] asciiMatrix = ConvertBrightnessToASCII(brightnessMatrix);
            PrintASCII(asciiMatrix);
            
            Console.Write("Enter integer: ");
            string val = Console.ReadLine();
        }


        static Bitmap ResizeBMP(byte[] photoBytes)
        {            
            ISupportedImageFormat format = new JpegFormat { Quality = 70 };
            Size size = new Size(150, 0);
            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (MemoryStream outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                                    .Resize(size)
                                    .Save(outStream);                   
                    }
                    return new Bitmap(outStream);
                }
            }
        }

        static int GetAverageRGB(Color c)
        {
            int avg = (c.R + c.G + c.B) / 3;
            return avg;
        }

        static void PrintASCII(char[,] asciiMatrix)
        {
            for (int i = 0; i < asciiMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < asciiMatrix.GetLength(1); j++)
                {
                    Console.Write("{0}{0}", asciiMatrix[i, j]);
                }
                Console.WriteLine();
            }
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
        static Color[,] BmpTo2dArray(Bitmap bmp)
        {
            int height = bmp.Height;
            int width = bmp.Width;
            Color[,] result = new Color[height, width];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int r = bmp.GetPixel(i, j).R;
                    int g = bmp.GetPixel(i, j).G;
                    int b = bmp.GetPixel(i, j).B;
                    Color c = Color.FromArgb(r, g, b);
                    result[j, i] = c;
                }
            }
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
