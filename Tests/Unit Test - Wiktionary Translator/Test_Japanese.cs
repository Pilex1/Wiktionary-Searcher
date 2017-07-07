using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace Unit_Test___Wiktionary_Translator {
    [TestClass]
    public class Test_Japanese {
        [TestMethod]
        public void Toshiki() {
            Translator t = new Translator();
            t.Language = "Japanese";
            FullTranslation f = t.Translate("Toshiki");

            Assert.AreEqual("Romanization", f.Translations[0].g);
            Assert.AreEqual("Rōmaji transcription of としき", f.Translations[0].s);
        }

        [TestMethod]
        public void 頭が良い() {
            Translator t = new Translator();
            t.Language = "Japanese";
            FullTranslation f = t.Translate("頭が良い");

            Assert.AreEqual("Adjective", f.Translations[0].g);
            Assert.AreEqual("bright, intelligent, sharp", f.Translations[0].s);
        }

        [TestMethod]
        public void オーバー() {
            Translator t = new Translator();
            t.Language = "Japanese";
            FullTranslation f = t.Translate("オーバー");

            Assert.AreEqual("Adjectival noun", f.Translations[0].g);
            Assert.AreEqual("exaggerated", f.Translations[0].s);

            Assert.AreEqual("Noun", f.Translations[1].g);
            Assert.AreEqual("a overcoat", f.Translations[1].s);

            Assert.AreEqual("Noun", f.Translations[2].g);
            Assert.AreEqual("over, exceeding the limit", f.Translations[2].s);

            Assert.AreEqual("Noun", f.Translations[3].g);
            Assert.AreEqual("(baseball) ball hit over the head of an outfielder", f.Translations[3].s);
        }

        [TestMethod]
        public void そこで() {
            Translator t = new Translator();
            t.Language = "Japanese";
            FullTranslation f = t.Translate("そこで");

            Assert.AreEqual("Adverb", f.Translations[0].g);
            Assert.AreEqual("(conjunctive) so, then, in that case", f.Translations[0].s);
        }

    }
}
