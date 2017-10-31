using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WiktionaryTranslator;

namespace Unit_Test___Wiktionary_Translator {
    [TestClass]
    public class Test_Latin {

        [TestMethod]
        public void bulla() {
            Translator t = new Translator("Latin");

            FullTranslation f = t.Translate("bulla");

            Assert.AreEqual("a bubble", f.Translations[0].translation);
            Assert.AreEqual("a swollen or bubble-shaped object, particularly:\n\t - a knob, boss, or stud, as on doors, shields, etc.\n\t - (historical) a bulla: a protective (usually golden) amulet worn by upper-class Roman children\n\t - (medieval, historical) a round metallic seal certifying official medieval documents, particularly the golden imperial seal and the leaden papal one.", f.Translations[1].translation);
            Assert.AreEqual("(medieval) a papal bull or other official document sealed with a bulla", f.Translations[2].translation);

        }

        [TestMethod]
        public void bonus() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("bonus");

            Assert.AreEqual("Adjective", f.Translations[0].grammaticalForm);
            Assert.AreEqual("good, honest, brave, noble, kind, pleasant", f.Translations[0].translation);

            Assert.AreEqual("Adjective", f.Translations[1].grammaticalForm);
            Assert.AreEqual("right", f.Translations[1].translation);

            Assert.AreEqual("Adjective", f.Translations[2].grammaticalForm);
            Assert.AreEqual("useful", f.Translations[2].translation);

            Assert.AreEqual("Adjective", f.Translations[3].grammaticalForm);
            Assert.AreEqual("valid", f.Translations[3].translation);

            Assert.AreEqual("Adjective", f.Translations[4].grammaticalForm);
            Assert.AreEqual("healthy", f.Translations[4].translation);

            Assert.AreEqual("Noun", f.Translations[5].grammaticalForm);
            Assert.AreEqual("A good, moral, honest or brave man", f.Translations[5].translation);

            Assert.AreEqual("Noun", f.Translations[6].grammaticalForm);
            Assert.AreEqual("A gentleman", f.Translations[6].translation);
        }

        [TestMethod]
        public void animus() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("animus");

            Assert.AreEqual("Noun", f.Translations[0].grammaticalForm);
            Assert.AreEqual("mind, soul, life force", f.Translations[0].translation);

            Assert.AreEqual("Noun", f.Translations[1].grammaticalForm);
            Assert.AreEqual("courage, will", f.Translations[1].translation);
        }

        [TestMethod]
        public void ad() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("ad");

            Assert.AreEqual("Preposition", f.Translations[0].grammaticalForm);
            Assert.AreEqual("(direction) toward, to, on, up to, for", f.Translations[0].translation);
        }

        [TestMethod]
        public void gaudeo() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("gaudeo");


            Assert.AreEqual("Verb", f.Translations[0].grammaticalForm);
            Assert.AreEqual("I rejoice.", f.Translations[0].translation);

            Assert.AreEqual("Verb", f.Translations[1].grammaticalForm);
            Assert.AreEqual("I take pleasure in.", f.Translations[1].translation);
        }

        [TestMethod]
        public void gratia() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("gratia");

            Assert.AreEqual("Noun", f.Translations[0].grammaticalForm);
            Assert.AreEqual("grace", f.Translations[0].translation);

            Assert.AreEqual("Noun", f.Translations[1].grammaticalForm);
            Assert.AreEqual("thankfulness", f.Translations[1].translation);

            Assert.AreEqual("Noun", f.Translations[2].grammaticalForm);
            Assert.AreEqual("(plural) thanks", f.Translations[2].translation);

            Assert.AreEqual("Noun", f.Translations[3].grammaticalForm);
            Assert.AreEqual("sake; pleasure", f.Translations[3].translation);

            Assert.AreEqual("Noun", f.Translations[4].grammaticalForm);
            Assert.AreEqual("(figurative) friendship", f.Translations[4].translation);
        }


        [TestMethod]
        public void en() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("en");

            Assert.AreEqual("Interjection", f.Translations[0].grammaticalForm);
            Assert.AreEqual("lookǃ beholdǃ (presenting something in a lively or indignant manner)", f.Translations[0].translation);

            Assert.AreEqual("Interjection", f.Translations[1].grammaticalForm);
            Assert.AreEqual("reallyǃ? (surprise or anger in questions)", f.Translations[1].translation);

            Assert.AreEqual("Interjection", f.Translations[2].grammaticalForm);
            Assert.AreEqual("c'monǃ (exhortation to action in imperatives)", f.Translations[2].translation);

            Assert.AreEqual("Noun", f.Translations[3].grammaticalForm);
            Assert.AreEqual("The name of the letter N.", f.Translations[3].translation);
        }

        [TestMethod]
        public void clam() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("clam");

            Assert.AreEqual("Adverb", f.Translations[0].grammaticalForm);
            Assert.AreEqual("clandestinely, secretly, privately", f.Translations[0].translation);

            Assert.AreEqual("Adverb", f.Translations[1].grammaticalForm);
            Assert.AreEqual("stealthily", f.Translations[1].translation);

            Assert.AreEqual("Preposition", f.Translations[2].grammaticalForm);
            Assert.AreEqual("(with accusative or, rarely, ablative) without the knowledge of, unknown to", f.Translations[2].translation);
        }

        [TestMethod]
        public void aliter() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("aliter");

            Assert.AreEqual("Adverb", f.Translations[0].grammaticalForm);
            Assert.AreEqual("otherwise", f.Translations[0].translation);

            Assert.AreEqual("Adverb", f.Translations[1].grammaticalForm);
            Assert.AreEqual("differently, wrongly, poorly", f.Translations[1].translation);

            Assert.AreEqual("Adverb", f.Translations[2].grammaticalForm);
            Assert.AreEqual("badly, negatively", f.Translations[2].translation);

            Assert.AreEqual("Adverb", f.Translations[3].grammaticalForm);
            Assert.AreEqual("mis- (aliter exceptum; mis-understood)", f.Translations[3].translation);
        }

        [TestMethod]
        public void palma() {
            Translator t = new Translator("Latin");
            FullTranslation f = t.Translate("palma");

            Assert.AreEqual("Noun", f.Translations[0].grammaticalForm);
            Assert.AreEqual("(anatomy) palm of the hand, hand", f.Translations[0].translation);

            Assert.AreEqual("Noun", f.Translations[1].grammaticalForm);
            Assert.AreEqual("(nautical) blade of an oar", f.Translations[1].translation);

            Assert.AreEqual("Noun", f.Translations[2].grammaticalForm);
            Assert.AreEqual("(botany) palm tree; date tree", f.Translations[2].translation);

            Assert.AreEqual("Noun", f.Translations[3].grammaticalForm);
            Assert.AreEqual("(figuratively) victory", f.Translations[3].translation);

            Assert.AreEqual("Noun", f.Translations[4].grammaticalForm);
            Assert.AreEqual("vocative singular of palma", f.Translations[4].translation);

            Assert.AreEqual("Noun", f.Translations[5].grammaticalForm);
            Assert.AreEqual("(unit of measure, medieval) palm, of various exact values throughout Europe but usually ¼ of the local foot.", f.Translations[5].translation);

            Assert.AreEqual("Noun", f.Translations[6].grammaticalForm);
            Assert.AreEqual("ablative singular of palma", f.Translations[6].translation);

            Assert.AreEqual("Noun", f.Translations[7].grammaticalForm);
            Assert.AreEqual("a parma; a small shield carried by the infantry and cavalry", f.Translations[7].translation);

            Assert.AreEqual("Noun", f.Translations[8].grammaticalForm);
            Assert.AreEqual("(poetic) any shield", f.Translations[8].translation);

            Assert.AreEqual("Noun", f.Translations[9].grammaticalForm);
            Assert.AreEqual("(poetic) a Threx", f.Translations[9].translation);

            Assert.AreEqual("Noun", f.Translations[10].grammaticalForm);
            Assert.AreEqual("vocative singular of palma", f.Translations[10].translation);

            Assert.AreEqual("Noun", f.Translations[11].grammaticalForm);
            Assert.AreEqual("ablative singular of palma", f.Translations[11].translation);
        }
    }
}
