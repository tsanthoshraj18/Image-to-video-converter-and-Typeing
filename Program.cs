using AForge.Video.FFMPEG;
using System;
using System.Drawing;
using System.IO;

namespace TimeLapseCreator
{
  class Program
  {


    static void Main(string[] args)
    {
      try
      {
        //Image Path
        const string basePath = @"images";

        //Video save path at same dir.
        var video = @"images\video";

        //If path not found 
        if (!Directory.Exists(basePath))
        {
          Console.WriteLine("Images folder is not found in the relative path...");
          return;
        }
        //To get image files
        var image = Directory.GetFiles(basePath, "*.png");
        
        //Will Check image is present or not
        if (image.Length <= 0)
        {
          Console.WriteLine("Images Not Found");
          return;        
        }
        
        //To create video folder
        if (!Directory.Exists(video))
        {
          Directory.CreateDirectory(video);
        }

        //To create video file
        using (var videoWriter = new VideoFileWriter())
          {
            //path to save video+ video formate ,image width, hight , number of image per sec,
            videoWriter.Open($"{video}/Output.avi", 1536, 742, 1, VideoCodec.MPEG4, 1000000);
            
                    //image sequence 
            foreach (var images in image)
            {

              using (Bitmap bitImage = Bitmap.FromFile(images) as Bitmap)
              {
                videoWriter.WriteVideoFrame(bitImage);
              }

            }
            videoWriter.Close();
          }
        Console.WriteLine("Video Saved Sucessfully...");

      }
      catch (Exception e)
      {

        Console.WriteLine(e);
      }

    }
  }
}