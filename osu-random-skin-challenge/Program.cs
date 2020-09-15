using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace osu_random_skin_challenge
{
    class Program
    {
        static Queue<string> skins;

        static string[] ChoosePhrases = new[]
        {
            "And the next skin will be",
            "Hmm, your nex skin is",
            "Wow! This skin is so cool -",
            "Next skin:",
            "LOL I hate this skin but u can try to FC something with it:",
            "Good luck! Your skin is",
            "Another good skin:",
        };

        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "osu! Random Skin FC Challenge";
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Hey! Welcome to the osu! Random skin FC Challenge!");
            Console.WriteLine("The essence of this challenge is that you must FC any map (which corresponds to your level of play) with a random skin, which my program will help you choose");
            Console.Write("If you don't have enough skins, you can download big skin pack here: ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("https://www.reddit.com/r/OsuSkins/comments/ermcpl/");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Please, choose your osu! folder");

            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() != DialogResult.OK)
                Environment.Exit(0);

            skins = GetSkins(dialog.SelectedPath + "\\Skins");
            Console.Title = $"osu! Random Skin FC Challenge - {skins.Count} skins left";

            Random random = new Random();

            while (skins.Count != 0)
            {
                Console.WriteLine();

                Console.Write(ChoosePhrases[random.Next(ChoosePhrases.Length)] + " \"");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(skins.Dequeue());
                Console.Title = $"osu! Random Skin FC Challenge - {skins.Count} skins left";

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\"");

                Console.Write("Press ENTER when you FC the map");
                Console.ReadLine();
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations! You don't have any skins left that you haven't played with!");
            Console.ReadLine();
        }

        static Queue<string> GetSkins(string path)
        {
            Random random = new Random();

            string[] skinDirs = Directory.GetDirectories(path);
            
            List<string> skins = new List<string>();
            skins.Add("Default");

            foreach(string dir in skinDirs)
            {
                string[] hit = Directory.GetFiles(dir, "hitcircle.png", SearchOption.TopDirectoryOnly);
                if(hit.Length > 0) skins.Add(dir.Replace(path + "\\", ""));
            }

            return new Queue<string>(skins.OrderBy(x => random.Next()));
        }
    }
}
