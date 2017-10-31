using LibUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WiktionaryTranslator;

namespace Latin_Vocab_Tester {
    public class OfflineWordbank {

        internal string InnerDirectory;
        internal const string InvalidFile = "invalid.txt";
        internal const string TempFile = "definitions_temp.txt";
        internal const string FullFile = "definitions_full.txt";

        private HashSet<string> alreadyLoaded;
        private HashSet<string> invalid;

        public OfflineWordbank(string innerDirectory) {
            InnerDirectory = innerDirectory;
            alreadyLoaded = new HashSet<string>();
            invalid = new HashSet<string>();
        }

        private void LoadAlreadyDownloaded() {
            if (!File.Exists(LatinLibraryDownloader.Dir + InnerDirectory + TempFile)) return;
            SortedDictionary<string, List<string>> sortedDict = ReadFromFile(TempFile);
            foreach (string word in sortedDict.Keys) {
                alreadyLoaded.Add(word);
            }
        }

        // file is either TempFile or FullFile
        internal SortedDictionary<string, List<string>> ReadFromFile(string file) {
            var dict = new SortedDictionary<string, List<string>>();
            StreamReader reader = new StreamReader(LatinLibraryDownloader.Dir + InnerDirectory + file);

            string line = null;

            string word = null;
            List<string> allTranslations = new List<string>();
            string curTranslation = "";
            while ((line = reader.ReadLine()) != null) {

                // start of a new word 
                // e.g. bonus {
                Match m1 = Regex.Match(line, "^([^{]+?) {$");
                if (m1.Success) {
                    word = m1.Groups[1].Captures[0].ToString();
                    continue;
                }

                // end of a word + number correct
                // e.g. } 9
                Match m5 = Regex.Match(line, "^{ (\\d+)");
                if (m5.Success) {
                    int correct = int.Parse(m5.Groups[1].Captures[0].ToString());
                    if (allTranslations.Count > 0 && word != null) {
                        string joined = string.Join(" ", allTranslations);
                        dict.Add(word, allTranslations);
                        word = null;
                        allTranslations = new List<string>();
                        curTranslation = "";
                    }
                    continue;
                }


                // start of a new translation e.g.
                // Adjective {
                Match m2 = Regex.Match(line, "[\\s]+\\w+ \\{ .+?");
                if (m2.Success) {
                    curTranslation += line + "\n";
                }

                // middle of a multiline translation
                Match m6 = Regex.Match(line, "[\\s]+[^{}]+?$");
                if (!m2.Success && m6.Success) {
                    curTranslation += line + "\n";
                }

                // end of a multiline translation
                Match m3 = Regex.Match(line, "[\\s]+.+? \\}");
                if (!m2.Success && m3.Success) {
                    curTranslation += line + "\n";
                }

                if (line.Contains("}") && line != "}") {
                    if (curTranslation.EndsWith("\n")) {
                        curTranslation = curTranslation.Substring(0, curTranslation.Length - 1);
                    }
                    allTranslations.Add(curTranslation);
                    curTranslation = "";
                    continue;
                }
            }
            reader.Dispose();

            return dict;
        }

        private void WriteToFile() {
            StreamWriter writer = new StreamWriter(LatinLibraryDownloader.Dir + InnerDirectory + FullFile);
            SortedDictionary<string, List<string>> sortedDict = ReadFromFile(TempFile);
            foreach (string word in sortedDict.Keys) {
                List<string> translations = sortedDict[word];
                writer.WriteLine(word + " {");
                foreach (string translation in translations) {
                    writer.WriteLine(translation);
                }
                writer.WriteLine("}");
                writer.WriteLine();
            }
            writer.Dispose();
        }

        private void LoadInvalids() {
            if (!File.Exists(LatinLibraryDownloader.Dir + InnerDirectory + InvalidFile)) return;
            string[] array = FileUtil.ReadFileAsArray(LatinLibraryDownloader.Dir + InnerDirectory + InvalidFile);
            foreach (string s in array) {
                invalid.Add(s);
            }
        }

        private void DownloadWords(string[] words) {
            Directory.CreateDirectory(LatinLibraryDownloader.Dir + InnerDirectory);
            LoadAlreadyDownloaded();
            LoadInvalids();

            StreamWriter tempWriter = new StreamWriter(LatinLibraryDownloader.Dir + InnerDirectory + TempFile, true);
            tempWriter.AutoFlush = true;
            StreamWriter invalidWriter = new StreamWriter(LatinLibraryDownloader.Dir + InnerDirectory + InvalidFile, true);
            invalidWriter.AutoFlush = true;

            Translator translator = new Translator("Latin");
            foreach (string s in words) {
                //    Console.WriteLine(FileUtil.GetReverseParentDirectoryName(file, 2) + " " + word);
                if (!alreadyLoaded.Contains(s)) {
                    if (invalid.Contains(s)) {
                        // the word has been translated before and no translation was found
                        // so we can ignore it and move on
                        //Console.WriteLine("Already translated with no translation found");
                    } else {
                        FullTranslation translation = translator.Translate(s);
                        if (translation == null) {
                            invalid.Add(s);
                            invalidWriter.WriteLine(s);

                            Console.WriteLine("No translation found: " + s);

                        } else {
                            alreadyLoaded.Add(s);
                            tempWriter.WriteLine(translation.ToString());

                            Console.WriteLine("Translated successfully: " + s);
                        }
                    }
                } else {
                    // word has already been translated
                    // Console.WriteLine("Already translated");
                }
            }
            invalidWriter.Dispose();
            tempWriter.Dispose();

            WriteToFile();
        }

        public void Download(string file) {
            string[] words = FileUtil.ReadFileAsArray(LatinLibraryDownloader.Dir + InnerDirectory + file);
            DownloadWords(words);
        }

        public void DownloadAuthors(params string[] authors) => Download(author => authors.Contains(author));
        public void DownloadAll() => Download(author => true);

        private void Download(Predicate<string> AuthorPredicate) {
            string[] files = FileUtil.GetAllFilesInDirectoryAndSubdirectories(LatinLibraryDownloader.Dir);
            HashSet<string> words = new HashSet<string>();
            foreach (string file in files) {
                string curAuthor = Path.GetFileName(FileUtil.GetParentDirectoryName(file, 2));
                if (!AuthorPredicate(curAuthor)) continue;
                string contents = FileUtil.ReadFile(file);
                contents = contents.RemovePunctuation().RemoveDigits().RemoveUnnecessarySpacing();
                string[] array = contents.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in array) {
                    words.Add(word);
                }
            }

            DownloadWords(words.ToArray());
        }
    }
}
