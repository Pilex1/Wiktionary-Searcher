using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests;

namespace Unit_Test___Wiktionary_Translator {
    [TestClass]
    public class Test_Latin {

        [TestMethod]
        public void an() {
            Translator t = new Translator();
            t.Language = "Latin";

            FullTranslation f = t.Translate("an");

            Assert.AreEqual("Conjunction", f.Translations[0].g);
            Assert.AreEqual("or, or whether (A conjunction that introduces the second part of a disjunctive interrogation, or a phrase implying doubt.)", f.Translations[0].s);
        }

        [TestMethod]
        public void argentum() {
            Translator t = new Translator();
            t.Language = "Latin";

            FullTranslation f = t.Translate("argentum");

            Assert.AreEqual("Noun", f.Translations[0].g);
            Assert.AreEqual("silver (metal, element)", f.Translations[0].s);

            Assert.AreEqual("Noun", f.Translations[1].g);
            Assert.AreEqual("(by extension) a silver thing", f.Translations[1].s);
        }

        [TestMethod]
        public void bonus() {
            Translator t = new Translator();
            t.Language = "Latin";

            FullTranslation f = t.Translate("bonus");

            Assert.AreEqual("Adjective", f.Translations[0].g);
            Assert.AreEqual("good, honest, brave, noble, kind, pleasant", f.Translations[0].s);

            Assert.AreEqual("Adjective", f.Translations[1].g);
            Assert.AreEqual("right", f.Translations[1].s);

            Assert.AreEqual("Adjective", f.Translations[2].g);
            Assert.AreEqual("useful", f.Translations[2].s);

            Assert.AreEqual("Adjective", f.Translations[3].g);
            Assert.AreEqual("valid", f.Translations[3].s);

            Assert.AreEqual("Adjective", f.Translations[4].g);
            Assert.AreEqual("healthy", f.Translations[4].s);

            Assert.AreEqual("Noun", f.Translations[5].g);
            Assert.AreEqual("A good, moral, honest or brave man", f.Translations[5].s);

            Assert.AreEqual("Noun", f.Translations[6].g);
            Assert.AreEqual("A gentleman", f.Translations[6].s);
        }

        [TestMethod]
        public void animus() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("animus");

            Assert.AreEqual("Noun", f.Translations[0].g);
            Assert.AreEqual("mind, soul, life force", f.Translations[0].s);

            Assert.AreEqual("Noun", f.Translations[1].g);
            Assert.AreEqual("courage, will", f.Translations[1].s);
        }

        [TestMethod]
        public void ad() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("ad");

            Assert.AreEqual("Preposition", f.Translations[0].g);
            Assert.AreEqual("(direction) toward, to, on, up to, for", f.Translations[0].s);
        }

        [TestMethod]
        public void gaudeo() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("gaudeo");


            Assert.AreEqual("Verb", f.Translations[0].g);
            Assert.AreEqual("I rejoice.", f.Translations[0].s);

            Assert.AreEqual("Verb", f.Translations[1].g);
            Assert.AreEqual("I take pleasure in.", f.Translations[1].s);
        }

        [TestMethod]
        public void gratia() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("gratia");

            Assert.AreEqual("Noun", f.Translations[0].g);
            Assert.AreEqual("grace", f.Translations[0].s);

            Assert.AreEqual("Noun", f.Translations[1].g);
            Assert.AreEqual("thankfulness", f.Translations[1].s);

            Assert.AreEqual("Noun", f.Translations[2].g);
            Assert.AreEqual("(plural) thanks", f.Translations[2].s);

            Assert.AreEqual("Noun", f.Translations[3].g);
            Assert.AreEqual("sake; pleasure", f.Translations[3].s);

            Assert.AreEqual("Noun", f.Translations[4].g);
            Assert.AreEqual("(figurative) friendship", f.Translations[4].s);
        }


        [TestMethod]
        public void en() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("en");

            Assert.AreEqual("Interjection", f.Translations[0].g);
            Assert.AreEqual("lookǃ beholdǃ (presenting something in a lively or indignant manner)", f.Translations[0].s);

            Assert.AreEqual("Interjection", f.Translations[1].g);
            Assert.AreEqual("reallyǃ? (surprise or anger in questions)", f.Translations[1].s);

            Assert.AreEqual("Interjection", f.Translations[2].g);
            Assert.AreEqual("c'monǃ (exhortation to action in imperatives)", f.Translations[2].s);

            Assert.AreEqual("Noun", f.Translations[3].g);
            Assert.AreEqual("The name of the letter N.", f.Translations[3].s);
        }

        [TestMethod]
        public void clam() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("clam");

            Assert.AreEqual("Adverb", f.Translations[0].g);
            Assert.AreEqual("clandestinely, secretly, privately", f.Translations[0].s);

            Assert.AreEqual("Adverb", f.Translations[1].g);
            Assert.AreEqual("stealthily", f.Translations[1].s);

            Assert.AreEqual("Preposition", f.Translations[2].g);
            Assert.AreEqual("(with accusative or, rarely, ablative) without the knowledge of, unknown to", f.Translations[2].s);
        }

        [TestMethod]
        public void aliter() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("aliter");

            Assert.AreEqual("Adverb", f.Translations[0].g);
            Assert.AreEqual("otherwise", f.Translations[0].s);

            Assert.AreEqual("Adverb", f.Translations[1].g);
            Assert.AreEqual("differently, wrongly, poorly", f.Translations[1].s);

            Assert.AreEqual("Adverb", f.Translations[2].g);
            Assert.AreEqual("badly, negatively", f.Translations[2].s);

            Assert.AreEqual("Adverb", f.Translations[3].g);
            Assert.AreEqual("mis- (aliter exceptum; mis-understood)", f.Translations[3].s);
        }

        [TestMethod]
        public void palma() {
            Translator t = new Translator();
            t.Language = "Latin";
            FullTranslation f = t.Translate("palma");

            Assert.AreEqual("Noun", f.Translations[0].g);
            Assert.AreEqual("(anatomy) palm of the hand, hand", f.Translations[0].s);

            Assert.AreEqual("Noun", f.Translations[1].g);
            Assert.AreEqual("(nautical) blade of an oar", f.Translations[1].s);

            Assert.AreEqual("Noun", f.Translations[2].g);
            Assert.AreEqual("(botany) palm tree; date tree", f.Translations[2].s);

            Assert.AreEqual("Noun", f.Translations[3].g);
            Assert.AreEqual("(figuratively) victory", f.Translations[3].s);

            Assert.AreEqual("Noun", f.Translations[4].g);
            Assert.AreEqual("vocative singular of palma", f.Translations[4].s);

            Assert.AreEqual("Noun", f.Translations[5].g);
            Assert.AreEqual("(unit of measure, medieval) palm, of various exact values throughout Europe but usually ¼ of the local foot.", f.Translations[5].s);

            Assert.AreEqual("Noun", f.Translations[6].g);
            Assert.AreEqual("ablative singular of palma", f.Translations[6].s);

            Assert.AreEqual("Noun", f.Translations[7].g);
            Assert.AreEqual("a parma; a small shield carried by the infantry and cavalry", f.Translations[7].s);

            Assert.AreEqual("Noun", f.Translations[8].g);
            Assert.AreEqual("(poetic) any shield", f.Translations[8].s);

            Assert.AreEqual("Noun", f.Translations[9].g);
            Assert.AreEqual("(poetic) a Threx", f.Translations[9].s);

            Assert.AreEqual("Noun", f.Translations[10].g);
            Assert.AreEqual("vocative singular of palma", f.Translations[10].s);

            Assert.AreEqual("Noun", f.Translations[11].g);
            Assert.AreEqual("ablative singular of palma", f.Translations[11].s);
        }
    }
}
