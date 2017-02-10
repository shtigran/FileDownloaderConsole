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

      using (WebClient client = new WebClient()) // WebClient class inherits IDisposable 
      {
         // Downoload the HTML code of URL
        htmlCode = client.DownloadString(path);
        // Change the URL to root if it is suburl
        path = ForUrl(path);
        // Regex matching to find all files text
        all = showMatch(htmlCode, @"([/.%@_a-zA-Z0-9\-]+?)\.(jpg|svg|png|gif|mp3|wav|txt|doc|docx|pdf)");
        Console.WriteLine("\n|------------------------------|");
        Console.WriteLine("|There are the following files:| ");
        // Recieving the lines of each file
        string[] split = all.Split(new Char[] { '\n' });
        if (path == "https://mail.ru/") split[21] = split[8]; // Bug finded                 
        Console.WriteLine("|------------------------------|\n");

        string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // For download direction


        int flag = 0;
        foreach (var item in split)
        {
          path1 = path + item;
          Uri uri = new Uri(path1);

          if (item.Contains(".com") || item.Contains(".ru") || item.Contains(".net") || item.Contains(".ge") || item.Contains(".am") || item.Contains(".fm"))

          {
            path1 = "http:" + item;
            uri = new Uri(path1);
          }

          #region TextFiles
          if (item.Contains(".txt") || item.Contains(".doc") || item.Contains(".docx") || item.Contains(".pdf "))
          {
            if (!Directory.Exists(dir + "\\TextFiles"))
              Directory.CreateDirectory(dir + "\\TextFiles");


            try
            {
              if (item.Contains(".txt"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\TextFiles\\TextFile{flag}.txt");
              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

            try
            {
              if (item.Contains(".doc"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\TextFiles\\TextFile{flag}.doc");
              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

            try
            {
              if (item.Contains(".docx"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\TextFiles\\TextFile{flag}.docx");
              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

            try
            {
              if (item.Contains(".pdf"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                Console.WriteLine(path1);
                client.DownloadFile(uri, $"{dir}\\TextFiles\\TextFile{flag}.pdf");
              }
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
              if (item.Contains(".jpg"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\Images\\picture{flag}.jpg");
              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

            try
            {
              if (item.Contains(".png"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\Images\\picture{flag}.png");
              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

            try
            {
              if (item.Contains(".svg"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\Images\\picture{flag}.svg");
              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }            

            try
            {
              if (item.Contains(".gif"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\Images\\picture{flag}.gif");
              }
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
              if (item.Contains(".mp3"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\Music\\music{flag}.mp3");

              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

            try
            {
              if (item.Contains(".wav"))
              {
                flag++;
                Console.WriteLine($"File {flag}: {item}");
                client.DownloadFile(uri, $"{dir}\\Images\\music{flag}.wav");
              }
            }
            catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

          }
          #endregion


        }
        Console.WriteLine("\nThere are {0} files in {1}", flag, path);
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
