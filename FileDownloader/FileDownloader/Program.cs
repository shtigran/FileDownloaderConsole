using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileDownloader
{
  public static class Program
  {
    static void Main(string[] args)
    {

      // Welcome message
      Console.WriteLine("\n ***|Welcome Website Files Downloader|***");
      Console.Write("\nPlease enter the Website path: ");
      string path = Console.ReadLine();


      // Checking URL validity          
      if (!Uri.IsWellFormedUriString(path, UriKind.RelativeOrAbsolute))
      { throw new UriFormatException("Wrong URL!!!"); }


      // Creating strings for operations
      string all = string.Empty;
      string htmlCode = string.Empty;
      string path1 = string.Empty;
      string filename = string.Empty;

      using (WebClient client = new WebClient()) // WebClient class inherits IDisposable 
      {
        // Downoload the HTML code of URL
        htmlCode = client.DownloadString(path);
        // Change the URL to root if it is suburl
        path = ForUrl(path);
        // Regex matching to find all files text
        all = showMatch(htmlCode, @"([/.%@_a-zA-Z0-9\-]+?)\.(jpg|svg|png|gif|mp3|wav|txt|doc|docx|pdf|3gp|avi|mp4|flv|mov|rar|iso|exe)");
        Console.WriteLine("\n|------------------------------|");
        Console.WriteLine("|There are the following files:| ");
        // Recieving the lines of each file
        string[] split = all.Split(new Char[] { '\n' });
        if (path == "https://mail.ru/") split[21] = split[8]; // Bug finded                 
        Console.WriteLine("|------------------------------|\n");

        string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // For download direction


        int count = 0;
        int countText = 0;
        int countPictures = 0;
        int countMusic = 0;
        int countVideos = 0;
        int countArchives = 0;

        foreach (var item in split)
        {
          string[] split1 = item.Split(new Char[] { '/' });
          filename = split1[split1.Length - 1];



          path1 = path + item;        
          Uri uri = new Uri(path1);

          if (item.Contains(".com") || item.Contains(".ru") || item.Contains(".net") || item.Contains(".ge") || item.Contains(".am") || item.Contains(".fm"))

          {
            if (path.Contains("http"))
              path1 = "http:" + item;
            if (path.Contains("https"))
              path1 = "https:" + item;
            uri = new Uri(path1);
          }

          #region TextFiles
          if (item.Contains(".txt") || item.Contains(".doc") || item.Contains(".docx") || item.Contains(".pdf "))
          {
            if (!Directory.Exists(dir + "\\TextFiles"))
              Directory.CreateDirectory(dir + "\\TextFiles");

            try
            {
              countText++;
              count++;
              Console.WriteLine($"File {count}: {item}");
              client.DownloadFile(uri, $"{dir}\\TextFiles\\{countText}_{filename}");
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }
          }

          #endregion

          #region Pictures
          if (item.Contains(".jpg") || item.Contains(".png") || item.Contains(".svg") || item.Contains(".gif") || item.Contains(".jpeg"))
          {
            if (!Directory.Exists(dir + "\\Images"))
              Directory.CreateDirectory(dir + "\\Images");

            try
            {
              countPictures++;
              count++;
              Console.WriteLine($"File {count}: {item}");
              client.DownloadFile(uri, $"{dir}\\Images\\{countPictures}_{filename}");

            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }
          }
          #endregion

          #region Music
          if (item.Contains(".mp3") || item.Contains(".wav"))
          {
            if (!Directory.Exists(dir + "\\Music"))
              Directory.CreateDirectory(dir + "\\Music");

            try
            {
              countMusic++;
              count++;
              Console.WriteLine($"File {count}: {item}");
              client.DownloadFile(uri, $"{dir}\\Music\\{countMusic}_{filename}");
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }
          }

          #endregion

          #region Videos
          if (item.Contains(".3gp") || item.Contains(".avi") || item.Contains(".mp4") || item.Contains(".flv") || item.Contains(".mov"))
          {
            if (!Directory.Exists(dir + "\\Videos"))
              Directory.CreateDirectory(dir + "\\Videos");


            try
            {
              countVideos++;
              count++;
              Console.WriteLine($"File {count}: {item}");
              client.DownloadFile(uri, $"{dir}\\Videos\\{countVideos}_{filename}");

            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }
          }

          #endregion

          #region Archives
          if (item.Contains(".rar") || item.Contains(".iso") || item.Contains(".exe"))
          {
            if (!Directory.Exists(dir + "\\Archives"))
              Directory.CreateDirectory(dir + "\\Archives");

            try
            {
              countArchives++;
              count++;
              Console.WriteLine($"File {count}: {item}");
              client.DownloadFile(uri, $"{dir}\\Archives\\{countArchives}_{filename}");
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }
          }

          #endregion

        }
        Console.WriteLine("\nThere are {0} files in {1}", count, path);
      }

      Console.ReadKey();
    }

    // Method For correct url if it is Suburl
    private static string ForUrl(string path)
    {
      string[] list = path.Split('/');
      foreach (var item in list)
      {
        if (item.Contains(".com") || item.Contains(".ru") || item.Contains(".net") || item.Contains(".ge") || item.Contains(".am") || item.Contains(".fm"))
          path = list[0] + "//" + item + '/';
      }

      return path;
    }

    // Method For Regex Matching
    private static string showMatch(string text, string expr)
    {
      MatchCollection mc = Regex.Matches(text, expr);
      string result = "";
      foreach (Match m in mc)
      {
        result += m.ToString() + "\n";
      }
      return result;
    }
  }
}
