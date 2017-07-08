using System;
using System.Text.RegularExpressions;
using LibUtil;
using System.IO;

namespace Tests {


    class Program {

        static void Main(string[] args) {

            Console.WriteLine("THE SUPER AWESOME WIKTIONARY SEARCHER");
            Console.WriteLine("© ALEX TAN 2017");
            Console.WriteLine();
            Console.WriteLine("INSTRUCTIONS:");
            Console.WriteLine();
            Console.WriteLine("\tSearches up a word in Wiktionary in a given language and displays its meaning/translation");
            Console.WriteLine("\tIf you leave the language option blank, it will default to the previous language");
            Console.WriteLine("\t(or English if there is no previous language)");
            Console.WriteLine();

            //  DisplayTranslation("かん", "Japanese");
            //   DisplayTranslation("bonus", "Latin");

            //for (int i = 0; i < 30; i++) {
            //    double frequency = 440 * Math.Pow(Math.Pow(2, 1.0 / 12), i);
            //    Console.Beep((int)frequency, 200);
            //}

            //   DisplayTranslation("bonus", "Latin");

            //  DisplayTranslation("a", "English");
            // DisplayTranslation("argentum", "Latin");
            //  DisplayTranslation("an", "Latin");
            DisplayTranslation("里", "Japanese");
            ReadAndTranslate();

            //DisplayTranslation("bona", "Latin");
            //DisplayTranslation("parent", "Latin");
            //DisplayTranslation("sum", "Latin");
            //DisplayTranslation("erunt", "Latin");
            //DisplayTranslation("pessimis", "Latin");
            //DisplayTranslation("precis", "Latin");
            //Console.ReadLine();
            //DisplayTranslation("bonus", "Latin");
            //DisplayTranslation("paro", "Latin");
            //DisplayTranslation("confiteor", "Latin");
            //DisplayTranslation("haereo", "Latin");
            //DisplayTranslation("pessimus", "Latin");
            //DisplayTranslation("et", "Latin");
            // DisplayTranslation("et", "Danish");

            Console.ReadLine();

        }

        static void ReadAndTranslate() {
            string language = "English";
            while (true) {
                Console.Write("Enter language: ");
                string newLanguage = Console.ReadLine();
                if (!newLanguage.Equals("")) {
                    language = newLanguage;
                }
                char[] charArr = language.ToCharArray();
                charArr[0] = char.ToUpper(charArr[0]);
                language = new string(charArr);
                Console.Write("Enter word: ");
                string word = Console.ReadLine();
                string separator = "**********************************************************";
                Console.WriteLine(separator);
                try {
                    DisplayTranslation(word, language);
                } catch (Exception e) {
                    Console.WriteLine(e);
                }
                Console.WriteLine(separator);
            }

        }

        static void DisplayTranslation(string input, string language) {
            Translator translator = new Translator(language);
            FullTranslation transList = translator.Translate(input);
            Console.WriteLine("Translation from " + language + ": \"" + input + "\"");
            Console.WriteLine();
            Console.WriteLine(transList.ToString());
        }

        static void PrintMatch(Match m) {
            for (int i = 0; i < m.Groups.Count; i++) {
                Group g = m.Groups[i];
                Console.WriteLine("Group " + i + ": ");
                for (int j = 0; j < g.Captures.Count; j++) {
                    Capture c = g.Captures[j];
                    Console.WriteLine("\tCapture " + j + ": " + c);
                }
                Console.WriteLine();
            }
            Console.WriteLine("----------");
        }

    }


}
