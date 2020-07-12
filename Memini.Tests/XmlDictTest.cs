using Memini.Data;
using Memini.Models;
using NUnit.Framework;
using System;

namespace Memini.Tests
{
    [TestFixture]
    public class XmlDictTest
    {

        private XmlDict _xmlDict = new XmlDict(@"
            <JMdict>
                <entry>
                    <ent_seq>1000020</ent_seq>
                    <r_ele>
                        <reb>ゝ</reb>
                    </r_ele>
                    <sense>
                        <gloss>repetition mark in hiragana</gloss>
                    </sense>
                </entry>
                <entry>
                    <k_ele>
                        <keb>test1</keb>
                        <ke_pri>spec1</ke_pri>
                    </k_ele>
                    <r_ele>
                        <reb>test1</reb>
                        <re_pri>spec1</re_pri>
                    </r_ele>
                    <sense>
                        <gloss>truchellotruc</gloss>
                    </sense>
                </entry>
                <entry>
                    <k_ele>
                        <keb>今日わ</keb>
                        <ke_pri>spec1</ke_pri>
                    </k_ele>
                    <r_ele>
                        <reb>こんにちわ</reb>
                        <re_pri>spec1</re_pri>
                    </r_ele>
                    <sense>
                        <gloss>hello</gloss>
                    </sense>
                </entry>
                <entry>
                    <k_ele>
                        <keb>test2</keb>
                        <ke_pri>spec1</ke_pri>
                    </k_ele>
                    <r_ele>
                        <reb>test2</reb>
                        <re_pri>spec1</re_pri>
                    </r_ele>
                    <sense>
                        <gloss>hello</gloss>
                    </sense>
                </entry>
                <entry>
                    <ent_seq>1000060</ent_seq>
                    <k_ele>
                        <keb>々</keb>
                    </k_ele>
                    <r_ele>
                        <reb>のま</reb>
                    </r_ele>
                    <r_ele>
                        <reb>ノマ</reb>
                    </r_ele>
                    <sense>
                        <gloss>kanji repetition mark</gloss>
                    </sense>
                </entry>
            </JMdict>
            ");

        [Test]
        public void TestGethelloWord()
        {
            Word word = _xmlDict.GetWord("hello");
            Assert.That(word, Is.Not.Null);
            Assert.That(word.Kanji, Is.EqualTo("今日わ"));
            Assert.That(word.Kana, Is.EqualTo("こんにちわ"));
            Assert.That(word.Translation, Is.EqualTo("hello"));
        }

        [Test]
        public void TestGetNoneExistingWord()
        {
            Word word = _xmlDict.GetWord("ioeioiofeijofif (inconnu)");
            Assert.That(word, Is.Null);
        }

        [Test]
        public void TestGetNullWord()
        {
            Word word = _xmlDict.GetWord(null);
            Assert.That(word, Is.Null);
        }
    }
}
