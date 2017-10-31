using LibUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiktionaryTranslator;

namespace Latin_Vocab_Tester {
    class WordAdder {
        private string innerDirectory;
        private QuestionGenerator generator;

        public WordAdder(string innerDirectory, QuestionGenerator generator) {
            this.innerDirectory = innerDirectory;
            this.generator = generator;
        }

        public void AddWord() {
            Console.Clear();
            Console.WriteLine("Search word:");
            Console.WriteLine();
            string word = Console.ReadLine();
            Console.WriteLine();
            WordInfo info = generator.GetInfo(word);
            if (info == null) {
                // Console.WriteLine("Auto translate? Y|N");
                //  string input2 = Console.ReadLine();
                string translationString;
                //  if (input2.ToLower() == "y") {
                //      Translator translator = new Translator("Latin");
                //      FullTranslation translation = translator.Translate(word);
                //      translationString = translation.ToString();
                //  } else {
                Console.WriteLine("Input part of speech:");
                translationString = Console.ReadLine().Capitalise() + " { ";
                Console.WriteLine("Input translation:");
                translationString += Console.ReadLine() + " }";
                // }
                generator.UpdateTranslation(word, new List<string>(new string[] { translationString }));
            } else {
                Console.WriteLine(info.DefinitionsString());
                Console.WriteLine("Modify translation? Y|N");
                string input2 = Console.ReadLine();
                if (input2.ToLower() == "y") {
                    string newTranslation = Console.ReadLine();
                    generator.UpdateTranslation(word, new List<string>(new string[] { newTranslation }));
                }
            }
        }

    }
}
