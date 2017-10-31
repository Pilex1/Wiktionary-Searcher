using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WiktionaryTranslator;

namespace Unit_Test___Wiktionary_Translator {
    [TestClass]
    public class Test_English {

        [TestMethod]
        public void Marshallese() {
            Translator t = new Translator("English");
            FullTranslation f = t.Translate("Marshallese");

            Assert.AreEqual("The Austronesian language of the Marshall Islands; also called Marshall and Ebon.", f.Translations[0].translation);
            Assert.AreEqual("An inhabitant of the Marshall Islands.", f.Translations[1].translation);
            Assert.AreEqual("Of or pertaining to the Marshall Islands, its people, or its language.", f.Translations[2].translation);
        }

        [TestMethod]
        public void diamond_ring() {
            Translator t = new Translator("English");
            FullTranslation f = t.Translate("diamond ring");

            Assert.AreEqual("Used other than as an idiom: see diamond,‎ ring.", f.Translations[0].translation);
        }

        [TestMethod]
        public void a() {
            Translator t = new Translator("English");
            FullTranslation f = t.Translate("a");

            Assert.AreEqual("Letter", f.Translations[0].grammaticalForm);
            Assert.AreEqual("The first letter of the English alphabet, called a and written in the Latin script.", f.Translations[0].translation);

            Assert.AreEqual("Numeral", f.Translations[1].grammaticalForm);
            Assert.AreEqual("The ordinal number first, derived from this letter of the English alphabet, called a and written in the Latin script.", f.Translations[1].translation);

            Assert.AreEqual("Noun", f.Translations[2].grammaticalForm);
            Assert.AreEqual("The name of the Latin script letter A/a.", f.Translations[2].translation);

            Assert.AreEqual("Article", f.Translations[3].grammaticalForm);
            Assert.AreEqual("One; any indefinite example of; used to denote a singular item of a group. [First attested prior to 1150]", f.Translations[3].translation);

            Assert.AreEqual("Article", f.Translations[4].grammaticalForm);
            Assert.AreEqual("Used in conjunction with the adjectives score, dozen, hundred, thousand, and million, as a function word.", f.Translations[4].translation);

            Assert.AreEqual("Article", f.Translations[5].grammaticalForm);
            Assert.AreEqual("One certain or particular; any single. [First attested between around 1150 to 1350]", f.Translations[5].translation);

            Assert.AreEqual("Article", f.Translations[6].grammaticalForm);
            Assert.AreEqual("The same; one. [16th Century]", f.Translations[6].translation);

            Assert.AreEqual("Article", f.Translations[7].grammaticalForm);
            Assert.AreEqual("Any, every; used before a noun which has become modified to limit its scope; also used with a negative to indicate not a single one.", f.Translations[7].translation);

            Assert.AreEqual("Article", f.Translations[8].grammaticalForm);
            Assert.AreEqual("Used before plural nouns modified by few, good many, couple, great many, etc.", f.Translations[8].translation);

            Assert.AreEqual("Article", f.Translations[9].grammaticalForm);
            Assert.AreEqual("Someone or something like; similar to; Used before a proper noun to create an example out of it.", f.Translations[9].translation);

            Assert.AreEqual("Preposition", f.Translations[10].grammaticalForm);
            Assert.AreEqual("(archaic) To do with position or direction; In, on, at, by, towards, onto. [First attested before 1150]", f.Translations[10].translation);

            Assert.AreEqual("Preposition", f.Translations[11].grammaticalForm);
            Assert.AreEqual("To do with separation; In, into. [First attested before 1150]", f.Translations[11].translation);

            Assert.AreEqual("Preposition", f.Translations[12].grammaticalForm);
            Assert.AreEqual("To do with time; Each, per, in, on, by. [First attested before 1150]", f.Translations[12].translation);

            Assert.AreEqual("Preposition", f.Translations[13].grammaticalForm);
            Assert.AreEqual("(obsolete) To do with method; In, with. [First attested before 1150]", f.Translations[13].translation);

            Assert.AreEqual("Preposition", f.Translations[14].grammaticalForm);
            Assert.AreEqual("(obsolete) To do with role or capacity; In. [First attested before 1150]", f.Translations[14].translation);

            Assert.AreEqual("Preposition", f.Translations[15].grammaticalForm);
            Assert.AreEqual("To do with status; In. [First attested before 1150]", f.Translations[15].translation);

            Assert.AreEqual("Preposition", f.Translations[16].grammaticalForm);
            Assert.AreEqual("(archaic) To do with process, with a passive verb; In the course of, experiencing. [First attested before 1150]", f.Translations[16].translation);

            Assert.AreEqual("Preposition", f.Translations[17].grammaticalForm);
            Assert.AreEqual("(archaic) To do with an action, an active verb; Engaged in. [16th century]", f.Translations[17].translation);

            Assert.AreEqual("Preposition", f.Translations[18].grammaticalForm);
            Assert.AreEqual("(archaic) To do with an action/movement; To, into. [16th century]", f.Translations[18].translation);

            Assert.AreEqual("Verb", f.Translations[19].grammaticalForm);
            Assert.AreEqual("(archaic or slang) Have. [between 1150 and 1350, continued in some use until 1650; used again after 1950]", f.Translations[19].translation);

            Assert.AreEqual("Pronoun", f.Translations[20].grammaticalForm);
            Assert.AreEqual("(obsolete outside England and Scotland dialects) He. [1150-1900]", f.Translations[20].translation);

            Assert.AreEqual("Interjection", f.Translations[21].grammaticalForm);
            Assert.AreEqual("A meaningless syllable; ah.", f.Translations[21].translation);

            Assert.AreEqual("Preposition", f.Translations[22].grammaticalForm);
            Assert.AreEqual("(archaic, slang) Of.", f.Translations[22].translation);

            Assert.AreEqual("Adverb", f.Translations[23].grammaticalForm);
            Assert.AreEqual("(chiefly Scotland) All. [First attested from 1350 to 1470.]", f.Translations[23].translation);

            Assert.AreEqual("Adjective", f.Translations[24].grammaticalForm);
            Assert.AreEqual("(chiefly Scotland) All. [First attested from 1350 to 1470.]", f.Translations[24].translation);

            Assert.AreEqual("Symbol", f.Translations[25].grammaticalForm);
            Assert.AreEqual("Distance from leading edge to aerodynamic center.", f.Translations[25].translation);

            Assert.AreEqual("Symbol", f.Translations[26].grammaticalForm);
            Assert.AreEqual("specific absorption coefficient", f.Translations[26].translation);

            Assert.AreEqual("Symbol", f.Translations[27].grammaticalForm);
            Assert.AreEqual("specific rotation", f.Translations[27].translation);

            Assert.AreEqual("Symbol", f.Translations[28].grammaticalForm);
            Assert.AreEqual("allele (recessive)", f.Translations[28].translation);
        }

        [TestMethod]
        public void aa() {
            Translator t = new Translator("English");
            FullTranslation f = t.Translate("aa");

            Assert.AreEqual("(volcanology) A form of lava flow associated with Hawaiian-type volcanoes, consisting of basaltic rock, usually dark-colored with a jagged and loose, clinkery surface. Compare pahoehoe. [First attested in the mid 19th century.]", f.Translations[0].translation);

        }

        [TestMethod]
        public void Aaronic() {
            Translator t = new Translator("English");
            FullTranslation f = t.Translate("Aaronic");

            Assert.AreEqual("Pertaining to Aaron. [First attested in the mid 17th century.]", f.Translations[0].translation);
            Assert.AreEqual("Pertaining to or characteristic of a high priest; priestly. [First attested in the mid 17th century.]", f.Translations[1].translation);
            Assert.AreEqual("(Mormonism) Of or pertaining to the lower order of priests. [First attested in the early 19th century.]", f.Translations[2].translation);

        }

        [TestMethod]
        public void Aaronical() {
            Translator t = new Translator("English");
            FullTranslation f = t.Translate("Aaronical");

            Assert.AreEqual("Alternative form of Aaronic [First attested in the early 17th century.]", f.Translations[0].translation);

        }
    }
}
