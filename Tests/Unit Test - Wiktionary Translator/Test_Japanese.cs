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
    }
}
