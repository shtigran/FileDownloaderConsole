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
            Console.WriteLine("\n    |Welcome Website Files Downloader|");
            Console.Write("\nPlease enter the Website path: ");
            string path = Console.ReadLine();

            // Checking URL validity          
            if (!Uri.IsWellFormedUriString(path, UriKind.RelativeOrAbsolute))
            { Console.WriteLine("Wrong URL path!!!"); }

            else
            {   // Creating strings for operations
                string all = string.Empty;
                string htmlCode = string.Empty;
                string path1 = string.Empty;

                using (WebClient client = new WebClient()) // WebClient class inherits IDisposable 
                {
                    htmlCode = client.DownloadString(path);
                    all = showMatch(htmlCode, @"([/.@_a-zA-Z0-9\-]+?)\.(jpg|svg|png)");
                    //Console.WriteLine("\n"+ all);
                    Console.WriteLine("-------------");
                    Console.WriteLine("\nThere are the following files: ");
                    string[] split = all.Split(new Char[] { '\n' });
                    if (path == "https://mail.ru/") split[12] = split[11];

                    Console.WriteLine("-------------");
                   
                    string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


                    int flag = 1;
                    foreach (var item in split)
                    {
                        WebClient client1;
                        client1 = new WebClient();
                        path1 = path + item;
                        Uri uri = new Uri(path1);

                        if (item.Contains(".com") || item.Contains(".ru") || item.Contains(".net"))

                        {
                            client1 = new WebClient();
                            path1 = "http:" + item;
                            uri = new Uri(path1);
                        }

                        Console.WriteLine(item);

                        if (item.Contains(".jpg") || item.Contains(".png") || item.Contains(".svg"))
                        {
                            if (!Directory.Exists(dir + "\\Images"))
                                Directory.CreateDirectory(dir + "\\Images");


                            if (item.Contains(".jpg"))
                            { client1.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.jpg");
                                flag++;
                            }
                            if (item.Contains(".png") )
                            { client1.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.png");
                                flag++;
                            }
                            if (item.Contains(".svg"))
                            {
                                client1.DownloadFile(uri, $"{dir}\\Images\\Picture{flag}.svg");
                                flag++;
                            }



                            // //    if (item.Contains(".svg"))
                            // //    { client1.DownloadFileAsync(uri, $"{dir}\\Images\\Picture{flag}.svg"); }
                            // //    flag++;
                            //    // Console.WriteLine(item);
                            ////}

                            //    if (item.Contains(".png"))
                            //    {
                            //        client1 = new WebClient();
                            //        Uri uri = new Uri(path + item);
                            //        client1.DownloadFileAsync(uri, $"{dir}\\Picture{flag}.png");
                            //        flag++;
                            //        Console.WriteLine(item);
                            //    }

                            //    if (item.Contains(".svg"))
                            //    {
                            //        client1 = new WebClient();
                            //        Uri uri = new Uri(path + item);
                            //        client1.DownloadFileAsync(uri, $"{dir}\\Picture{flag}.svg");
                            //        flag++;
                            //        Console.WriteLine(item);
                            //    }
                        }
                    }
                }
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
