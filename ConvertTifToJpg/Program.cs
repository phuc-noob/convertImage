using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConvertTifToJpg
{
   class Program
   {
      static void Main(string[] args)
      {
         
         string dir = @"C:\Users\84826\source\repos\ConvertTifToJpg\Data\tif";
         string[] files = Directory.GetFiles(dir);
         foreach (string file in files)
         {
            // function convert .tif to jpg 
            ConvertTiffToJpeg(file,"converted");

            // function resize file .jgp from folder converted to width = 256
            ConvertResizeImage(file);

         }
      }

      public static void ConvertResizeImage(string path)
      {
         // save direction 
         string dir = @"C:\Users\84826\source\repos\ConvertTifToJpg\Data\tif\resize\";
         Image img = Image.FromFile(path);
         if (img.Width > 256)
         {
            if(img.Width > img.Height)
            {
               
               Bitmap imgbitmap = new Bitmap(img);
               Image Convert = (Image)(new Bitmap(img, new Size(256, img.Height / (img.Width / 256))));
               Convert.Save(dir + Path.GetFileNameWithoutExtension(path) + "_256" + ".jpg", ImageFormat.Tiff);
               
               Console.WriteLine(dir);
            }
            else
            {
               
               Console.WriteLine("--------------------");
               Bitmap imgbitmap = new Bitmap(img);
               Image Convert = (Image)(new Bitmap(img, new Size(img.Width / (img.Height / 256), 256)));
               Convert.Save(dir + Path.GetFileNameWithoutExtension(path) + "_256" + ".jpg", ImageFormat.Tiff);
               
               Console.WriteLine(dir);
            }
            
         }
         else
         {
            Console.WriteLine("width : " + img.Width);
            Console.WriteLine("height : " + img.Height);
            Console.WriteLine(img.Width);
         }
      }

      public static string[] ConvertTiffToJpeg(string fileName,string saveFile)
      {
         using (Image imageFile = Image.FromFile(fileName))
         {
            FrameDimension frameDimensions = new FrameDimension(
                imageFile.FrameDimensionsList[0]);

            // Gets the number of pages from the tiff image (if multipage) 
            int frameNum = imageFile.GetFrameCount(frameDimensions);
            string[] jpegPaths = new string[frameNum];

            for (int frame = 0; frame < frameNum; frame++)
            {
               // Selects one frame at a time and save as jpeg. 
               imageFile.SelectActiveFrame(frameDimensions, frame);
               using (Bitmap bmp = new Bitmap(imageFile))
               {
                  jpegPaths[frame] = String.Format("{0}\\{1}\\{2}{3}.jpg",
                      Path.GetDirectoryName(fileName),
                      saveFile,
                      Path.GetFileNameWithoutExtension(fileName),
                      frame);
                  bmp.Save(jpegPaths[frame], ImageFormat.Jpeg);
               }
            }

            return jpegPaths;
         }
      }
   }
}
