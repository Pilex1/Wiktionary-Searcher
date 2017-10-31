using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiktionaryTranslator;

namespace Unit_Test___Wiktionary_Translator {
    [TestClass]
    public class Test_Japanese {

        [TestMethod]
        public void 里() {
            Translator t = new Translator("Japanese");
            FullTranslation f = t.Translate("里");

            Assert.AreEqual("Readings", f.Translations[0].grammaticalForm);
            Assert.AreEqual("Goon: り (ri, Jōyō)", f.Translations[0].translation);

            Assert.AreEqual("Readings", f.Translations[1].grammaticalForm);
            Assert.AreEqual("Kan’on: り (ri, Jōyō)", f.Translations[1].translation);

            Assert.AreEqual("Readings", f.Translations[2].grammaticalForm);
            Assert.AreEqual("Kun: さと (sato, 里, Jōyō)", f.Translations[2].translation);

            Assert.AreEqual("Adjective", f.Translations[3].grammaticalForm);
            Assert.AreEqual("(slang, red-light district) boorish, hickish", f.Translations[3].translation);

            Assert.AreEqual("Noun", f.Translations[4].grammaticalForm);
            Assert.AreEqual("village", f.Translations[4].translation);
        }

        [TestMethod]
        public void Toshiki() {
            Translator t = new Translator("Japanese");
            FullTranslation f = t.Translate("Toshiki");

            Assert.AreEqual("Romanization", f.Translations[0].grammaticalForm);
            Assert.AreEqual("Rōmaji transcription of としき", f.Translations[0].translation);
        }

        [TestMethod]
        public void 頭が良い() {
            Translator t = new Translator("Japanese");
            FullTranslation f = t.Translate("頭が良い");

            Assert.AreEqual("Adjective", f.Translations[0].grammaticalForm);
            Assert.AreEqual("bright, intelligent, sharp", f.Translations[0].translation);
        }

        [TestMethod]
        public void オーバー() {
            Translator t = new Translator("Japanese");
            FullTranslation f = t.Translate("オーバー");

            Assert.AreEqual("Adjectival noun", f.Translations[0].grammaticalForm);
            Assert.AreEqual("exaggerated", f.Translations[0].translation);

            Assert.AreEqual("Noun", f.Translations[1].grammaticalForm);
            Assert.AreEqual("a overcoat", f.Translations[1].translation);

            Assert.AreEqual("Noun", f.Translations[2].grammaticalForm);
            Assert.AreEqual("over, exceeding the limit", f.Translations[2].translation);

            Assert.AreEqual("Noun", f.Translations[3].grammaticalForm);
            Assert.AreEqual("(baseball) ball hit over the head of an outfielder", f.Translations[3].translation);
        }

        [TestMethod]
        public void そこで() {
            Translator t = new Translator("Japanese");
            FullTranslation f = t.Translate("そこで");

            Assert.AreEqual("Adverb", f.Translations[0].grammaticalForm);
            Assert.AreEqual("(conjunctive) so, then, in that case", f.Translations[0].translation);
        }

    }
}
