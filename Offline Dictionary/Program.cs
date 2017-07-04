using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offline_Dictionary {
    class Program {
        static void Main(string[] args) {

            /*
            OfflineDictionary dict = OfflineDictionary.FromFile("Latin", "C:/Users/torcm/AppData/Roaming/Plexico/Latin Vocab/Syllabus Vocab Cleaned.txt");
            dict.Translate();*/

            OfflineDictionary dict = OfflineDictionary.FromWiktionary("English");


            Console.WriteLine();
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
