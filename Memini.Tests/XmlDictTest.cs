using Memini.Data;
using Memini.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Memini.Tests
{
    [TestFixture]
    public class XmlDictTest
    {
        private XmlDict _xmlDict = new XmlDict(@";hello;good day;good afternoon
:今日わ
!こんにちわ
;because of this;thanks to this;due to this
!このおかげで
;because of this
!このため
;besides;moreover;in addition
:この外
!このほか
;in this kind of situation
:このような場合に
!このようなばあいに
;the other day;lately;recently;during this period;meanwhile;in the meantime
:この間:此の間
!このあいだ!このかん
;consequently;as a result
:この結果
!このけっか
;these days;nowadays;now;at present;recently;lately
:この頃:此の頃
!このごろ!このころ
;in this case
:この場合
!このばあい
;this occasion;at this time;now
:この度:此の度:此度
!このたび!こたび
;this area;around here;this point;(for) now
:この辺:此の辺
!このへん
;hello;good day;good afternoon
:TEST今日わ
!TESTこんにちわ
;complaining
:こぼし話:零し話
!こぼしばなし
;hey!;hey!
!こら!コラ!ゴルァ
;after this
:此れから:此から
!これから
;here;with this
!これで
;the same as this
:これと同じ
!これとおなじ
;so far;up to now;hitherto;that's enough (for today);it ends here
:これ迄:此れまで:是迄:此れ迄
!これまで
;these
:これ等:此等:是等:此れ等
!これら
;hello;good day;good afternoon
:TEST1今日わ
!TEST1こんにちわ");

        [Test]
        public void TestGethelloWords()
        {
            List<Word> words = _xmlDict.GetWordsByGloss("hello");
            Assert.That(words, Is.Not.Null);
            Assert.That(words[0].Kanji, Is.EqualTo("今日わ"));
            Assert.That(words[0].Kana, Is.EqualTo("こんにちわ"));
            Assert.That(words[0].Translation, Is.EqualTo("hello"));
            Assert.That(words[1].Kanji, Is.EqualTo("TEST今日わ"));
            Assert.That(words[1].Kana, Is.EqualTo("TESTこんにちわ"));
            Assert.That(words[1].Translation, Is.EqualTo("hello"));
            Assert.That(words[2].Kanji, Is.EqualTo("TEST1今日わ"));
            Assert.That(words[2].Kana, Is.EqualTo("TEST1こんにちわ"));
            Assert.That(words[2].Translation, Is.EqualTo("hello"));
        }

        [Test]
        public void TestGetNullWords()
        {
            List<Word> words = _xmlDict.GetWordsByGloss(null);
            Assert.That(words, Is.Null);
        }

        [Test]
        public void TestGetUnknowWords()
        {
            List<Word> words = _xmlDict.GetWordsByGloss("unknow");
            Assert.That(words, Is.Null);
        }
    }
}
