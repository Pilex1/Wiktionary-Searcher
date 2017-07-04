using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;

namespace Unit_Test___Wiktionary_Translator {
    [TestClass]
    public class Test_English {

        [TestMethod]
        public void a() {
            Translator t = new Translator();
            t.Language = "English";
            FullTranslation f = t.Translate("a");

            Assert.AreEqual("Letter", f.Translations[0].g);
            Assert.AreEqual("The first letter of the English alphabet, called a and written in the Latin script.", f.Translations[0].s);

            Assert.AreEqual("Numeral", f.Translations[1].g);
            Assert.AreEqual("The ordinal number first, derived from this letter of the English alphabet, called a and written in the Latin script.", f.Translations[1].s);

            Assert.AreEqual("Noun", f.Translations[2].g);
            Assert.AreEqual("The name of the Latin script letter A/a.", f.Translations[2].s);

            Assert.AreEqual("Article", f.Translations[3].g);
            Assert.AreEqual("One; any indefinite example of; used to denote a singular item of a group. [First attested prior to 1150]", f.Translations[3].s);

            Assert.AreEqual("Article", f.Translations[4].g);
            Assert.AreEqual("Used in conjunction with the adjectives score, dozen, hundred, thousand, and million, as a function word.", f.Translations[4].s);

            Assert.AreEqual("Article", f.Translations[5].g);
            Assert.AreEqual("One certain or particular; any single. [First attested between around 1150 to 1350]", f.Translations[5].s);

            Assert.AreEqual("Article", f.Translations[6].g);
            Assert.AreEqual("The same; one. [16th Century]", f.Translations[6].s);

            Assert.AreEqual("Article", f.Translations[7].g);
            Assert.AreEqual("Any, every; used before a noun which has become modified to limit its scope; also used with a negative to indicate not a single one.", f.Translations[7].s);

            Assert.AreEqual("Article", f.Translations[8].g);
            Assert.AreEqual("Used before plural nouns modified by few, good many, couple, great many, etc.", f.Translations[8].s);

            Assert.AreEqual("Article", f.Translations[9].g);
            Assert.AreEqual("Someone or something like; similar to; Used before a proper noun to create an example out of it.", f.Translations[9].s);

            Assert.AreEqual("Preposition", f.Translations[10].g);
            Assert.AreEqual("(archaic) To do with position or direction; In, on, at, by, towards, onto. [First attested before 1150]", f.Translations[10].s);

            Assert.AreEqual("Preposition", f.Translations[11].g);
            Assert.AreEqual("To do with separation; In, into. [First attested before 1150]", f.Translations[11].s);

            Assert.AreEqual("Preposition", f.Translations[12].g);
            Assert.AreEqual("To do with time; Each, per, in, on, by. [First attested before 1150]", f.Translations[12].s);

            Assert.AreEqual("Preposition", f.Translations[13].g);
            Assert.AreEqual("(obsolete) To do with method; In, with. [First attested before 1150]", f.Translations[13].s);

            Assert.AreEqual("Preposition", f.Translations[14].g);
            Assert.AreEqual("(obsolete) To do with role or capacity; In. [First attested before 1150]", f.Translations[14].s);

            Assert.AreEqual("Preposition", f.Translations[15].g);
            Assert.AreEqual("To do with status; In. [First attested before 1150]", f.Translations[15].s);

            Assert.AreEqual("Preposition", f.Translations[16].g);
            Assert.AreEqual("(archaic) To do with process, with a passive verb; In the course of, experiencing. [First attested before 1150]", f.Translations[16].s);

            Assert.AreEqual("Preposition", f.Translations[17].g);
            Assert.AreEqual("(archaic) To do with an action, an active verb; Engaged in. [16th century]", f.Translations[17].s);

            Assert.AreEqual("Preposition", f.Translations[18].g);
            Assert.AreEqual("(archaic) To do with an action/movement; To, into. [16th century]", f.Translations[18].s);

            Assert.AreEqual("Pronoun", f.Translations[19].g);
            Assert.AreEqual("(obsolete outside England and Scotland dialects) He. [1150-1900]", f.Translations[19].s);

            Assert.AreEqual("Interjection", f.Translations[20].g);
            Assert.AreEqual("A meaningless syllable; ah.", f.Translations[20].s);

            Assert.AreEqual("Adverb", f.Translations[21].g);
            Assert.AreEqual("(chiefly Scotland) All. [First attested from 1350 to 1470.]", f.Translations[21].s);

            Assert.AreEqual("Adjective", f.Translations[22].g);
            Assert.AreEqual("(chiefly Scotland) All. [First attested from 1350 to 1470.]", f.Translations[22].s);

            Assert.AreEqual("Symbol", f.Translations[23].g);
            Assert.AreEqual("Distance from leading edge to aerodynamic center.", f.Translations[23].s);

            Assert.AreEqual("Symbol", f.Translations[24].g);
            Assert.AreEqual("specific absorption coefficient", f.Translations[24].s);

            Assert.AreEqual("Symbol", f.Translations[25].g);
            Assert.AreEqual("specific rotation", f.Translations[25].s);

            Assert.AreEqual("Symbol", f.Translations[26].g);
            Assert.AreEqual("allele (recessive)", f.Translations[26].s);
        }
    }
}
