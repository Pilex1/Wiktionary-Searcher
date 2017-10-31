using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Latin_Vocab_Tester {
    public class QASet {

        QuestionGenerator parent;
        public string wordToBeTranslated;
        public string[] possibleAnswers;
        public int indexOfCorrectAnswer;

        internal QASet(QuestionGenerator parent, string wordToBeTranslated, string[] possibleAnswers, int indexOfCorrectAnswer) {
            this.parent = parent;
            this.wordToBeTranslated = wordToBeTranslated;
            this.possibleAnswers = possibleAnswers;
            this.indexOfCorrectAnswer = indexOfCorrectAnswer;
        }

        public void AskQuestion() {
            bool result = GetAnswer();
            if (result == true) {
                parent.MarkCorrect(wordToBeTranslated);
            } else {
                parent.MarkIncorrect(wordToBeTranslated);
            }
        }

        // returns whether the question was answered correctly or not
        private bool GetAnswer() {
            string separator = "******************************";
            bool result = false;
            Console.WriteLine(wordToBeTranslated);
            Console.WriteLine(separator);
            Console.ReadKey();
            Console.WriteLine();
            for (int i = 0; i < possibleAnswers.Length; i++) {
                Console.WriteLine("\t" + possibleAnswers[i]);
                Console.WriteLine();
            }
            Console.WriteLine(separator);
            string line = Console.ReadLine();
            int resultChoice;
            if (int.TryParse(line, out resultChoice)) {
                if (resultChoice == indexOfCorrectAnswer + 1) {
                    Console.WriteLine("Correct");
                    result = true;
                } else {
                    Console.WriteLine("Incorrect - correct answer is: " + (indexOfCorrectAnswer + 1));
                    result = false;
                }
            } else {
                Console.WriteLine("Incorrect - correct answer is: " + (indexOfCorrectAnswer + 1));
                result = false;
            }
            Console.WriteLine(separator);
            Console.ReadKey();
            Console.Clear();
            return result;
        }
    }
}
