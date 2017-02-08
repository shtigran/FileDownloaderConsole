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
            Console.Write("\n     Please enter the Website path: ");
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
                htmlCode = client.DownloadString(path);
                all = showMatch(htmlCode, @"([/.@_a-zA-Z0-9\-]+?)\.(jpg|svg|png|jpeg|gif)");

                Console.WriteLine("\n|------------------------------|");
                Console.WriteLine("|There are the following files:| ");
                string[] split = all.Split(new Char[] { '\n' });
                if (path == "https://mail.ru/") split[17] = split[18]; // Bug finded 

                Console.WriteLine("|------------------------------|\n");

                string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // For download direction


                int flag = 0;
                foreach (var item in split)
                {
                    path1 = path + item;
                    Uri uri = new Uri(path1);

                    if (item.Contains(".com") || item.Contains(".ru") || item.Contains(".net"))

                    {
                        path1 = "http:" + item;
                        uri = new Uri(path1);
                    }



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
                                client.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.jpg");
                            }
                        }
                        catch (FileNotFoundException) { Console.WriteLine("This file not found!");}

                        try
                        {
                            if (item.Contains(".png"))
                        {
                            flag++;
                            Console.WriteLine($"File {flag}: {item}");
                            client.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.png");
                        }
                        }
                        catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

                        try
                        {
                            if (item.Contains(".svg"))
                        {
                            flag++;
                            Console.WriteLine($"File {flag}: {item}");
                            client.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.svg");
                        }
                        }
                        catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

                        try
                        {
                            if (item.Contains(".jpeg"))
                            {
                                flag++;
                                Console.WriteLine($"File {flag}: {item}");
                                client.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.jpeg");
                            }
                        }
                        catch (FileNotFoundException) { Console.WriteLine("This file not found!"); }

                        try
                        {
                            if (item.Contains(".gif"))
                        {
                            flag++;
                            Console.WriteLine($"File {flag}: {item}");
                            client.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.gif");
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
