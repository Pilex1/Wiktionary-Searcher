using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latin_Vocab_Tester {
    class Program {

        public static string DownloadDirectory = "QuestionsManual/";
        public static string InputFile = "inputWords.txt";

        static void Main(string[] args) {
            //  LatinLibraryDownloader downloader = new LatinLibraryDownloader();
            //  downloader.Download();

            //  OfflineWordbank wordbank = new OfflineWordbank(DownloadDirectory);
            //  wordbank.Download("Ovid", "Cicero", "Tacitus");

            QuestionGenerator generator = new QuestionGenerator(LatinLibraryDownloader.Dir+DownloadDirectory);

            Form1 form = new Form1(generator);
            form.ShowDialog();
            form.Dispose();


            //WordAdder adder = new WordAdder(LatinLibraryDownloader.Dir + DownloadDirectory, generator);

            //while (true) {
            //    adder.AddWord();
            //}

            // while (true) {
            //     QuestionAnswerPair question = generator.Generate(4);
            //     question.AskQuestion();
            //  }
        }

    }
}
