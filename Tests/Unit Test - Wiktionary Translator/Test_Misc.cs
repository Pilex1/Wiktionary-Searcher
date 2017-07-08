using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests;

namespace Unit_Test___Wiktionary_Translator {
    [TestClass]
    public class Test_Misc {

        [TestMethod]
        public void ацҳақәа() {
            Translator t = new Translator("Abkhaz");
            FullTranslation f = t.Translate("ацҳақәа");

            Assert.AreEqual("Noun", f.Translations[0].g);
            Assert.AreEqual("plural of а́цҳа (ā́cḥā)", f.Translations[0].s);
        }

        [TestMethod]
        public void skæg() {
            Translator t = new Translator("Danish");
            FullTranslation f = t.Translate("skæg");

            Assert.AreEqual("(uncountable) fun (n or c)", f.Translations[2].s);
        }

        [TestMethod]
        public void chef() {
            Translator t = new Translator("Danish");
            FullTranslation f = t.Translate("chef");

            Assert.AreEqual("A chef, head cook (Can we verify this sense?)", f.Translations[1].s);
        }

        [TestMethod]
        public void hydrogennitrat() {
            Translator t = new Translator("Danish");
            FullTranslation f = t.Translate("hydrogennitrat");

            Assert.AreEqual("(inorganic chemistry) nitric acid, HNO3", f.Translations[0].s);
        }

        [TestMethod]
        public void tandsmør() {
            Translator t = new Translator("Danish");
            FullTranslation f = t.Translate("tandsmør");

            Assert.AreEqual("Butter on bread thick enough to reveal tooth marks.", f.Translations[0].s);
        }
    }
}
