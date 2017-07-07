using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Dictionary {
    class Program {
        static void Main(string[] args) {


            //   OfflineDictionary.TranslateFromFile("Latin", "C:/Users/torcm/AppData/Roaming/Plexico/Latin Vocab/test2.txt");


            // some obscure language with very few entries, making it really easy to test stuff
            //    OfflineDictionary.TranslateFromWiktionary("Afar");
            // OfflineDictionary.TranslateFromWiktionary("Abkhaz");


            /*
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;



            Console.WriteLine("COPYRIGHT ALEX TAN 2017");
            Console.WriteLine();
            Console.WriteLine("Downloads all the Wiktionary entries for a language (may take up to several hours)");
            Console.WriteLine("Downloads to: C:/Users/.../Appdata/Roaming/Plexico/Wiktionary Searcher/[language]/");
            Console.WriteLine();
            Console.WriteLine("Do not close your computer while downloading");
            Console.WriteLine("Do not disconnect from the internet while downloading");
            Console.WriteLine();
            Console.Write("Enter a language: ");
            string language = Console.ReadLine();

            OfflineDictionary.TranslateFromWiktionary(language);
            */

            //  OfflineDictionary.TranslateFromWiktionary("Japanese");
            //  OfflineDictionary.TranslateFromWiktionary("Latin");
            //  OfflineDictionary.TranslateFromWiktionary("Danish");
            //    OfflineDictionary.TranslateFromWiktionary("English");



            Console.WriteLine();
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
