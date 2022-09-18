using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CloudConsole
{
    internal class Program
    {
        public static void banner()
        {
            Console.Title = "-> CloudForce - CLI | Credits to PolyProxy, Jansel & GNF-Team <-";
            writeCenter("╔═══════════════════════════════════════╗");
            writeCenter("║       Welcome to CloudForce - CLI     ║");
            writeCenter("║                                       ║");
            writeCenter("║                                       ║");
            writeCenter("║ [0] Download File    [1] Bypass Steam ║");
            writeCenter("║                                       ║");
            writeCenter("║ [2] App Launcher     [3] Exit         ║");
            writeCenter("║                                       ║");
            writeCenter("╚═══════════════════════════════════════╝");
            Console.WriteLine("");
            writeCharsCenter(">> ");
        }
        public static void download(string url, string path)
        {
            using (var progress = new ProgressBar())
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) => progress.Report(e.ProgressPercentage / 100.0);
                    client.DownloadFileAsync(new Uri(url), path);
                    while (client.IsBusy)
                        System.Threading.Thread.Sleep(500);
                }
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            writeCenter("Download Finished!");
            Console.ResetColor();
            writeCharsCenter("Open File [y/n]> ");
            string awnser = Console.ReadLine();
            if (awnser.ToLower() == "y")
            {
                System.Diagnostics.Process.Start(path);
            }
        }

        public static void writeCenter(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }

        public static void writeCharsCenter(string text)
        {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.Write(text);
        }

        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;

            while (true)
            {
                Console.Clear();
                banner();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        Console.Clear();
                        writeCenter("-> CloudForce's Downloader <-");
                        writeCharsCenter("> URL: ");
                        string url = Console.ReadLine();
                        writeCharsCenter("> Path: ");
                        string path = Console.ReadLine();
                        if (!Directory.Exists(path) && path != "~") { Console.Clear(); Console.ForegroundColor = ConsoleColor.Red; writeCenter("< Invalid Path >"); Thread.Sleep(1000); Console.ResetColor(); }
                        else
                        {
                            Console.WriteLine("");
                            if (path != "~")
                            {
                                path = path + @"\" + url.Split('/').Last();
                            }
                            else
                            {
                                path = url.Split('/').Last();
                            }
                            download(url, path);

                            Thread.Sleep(1000);
                        }
                        continue;
                    case "1":
                    case "2":
                        Console.Clear();
                        writeCenter("-> Coming Soon <-");
                        Thread.Sleep(2000);
                        continue;
                    case "exit":
                    case "quit":
                    case ";":
                    case "q":
                    case "3":
                        return;
                }
            }
        }
    }
}
