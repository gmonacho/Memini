using Memini.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Xamarin.Forms.Shapes;

namespace Memini.Data
{
    public class XmlDict
    {
        private string _dictString;
        private int _i;

        public XmlDict(string dictString)
        {
            if (dictString == null)
                throw new ArgumentNullException("xml (string) argument is null");
            _dictString = dictString;
        }

        public XmlDict(StreamReader dictStream)
        {
            _dictString = dictStream.ReadToEnd();
        }

        void MoveToNextMarker()
        {
            while (_dictString[_i] != ':' &&
                   _dictString[_i] != ';' &&
                   _dictString[_i] != '!' &&
                   _dictString[_i] != '\0')
            {
                _i++;
            }
        }

        void SkipLine()
        {
            while (_dictString[_i] != '\n' &&
                   _dictString[_i] != '\0')
            {
                _i++;
            }
            MoveToNextMarker();
        }

        bool IsSearchedWord(string lookingStr)
        {
            int i = _i;

            while (_dictString[i] != '\n')
                i++;
            return (_dictString.IndexOf(lookingStr, _i, i - _i) != -1);
        }

        string ParseContent(char delimiter)
        {
            string kanji = string.Empty;
            int len = _dictString.Length;
            int i = _i;

            while (i < len &&
                   _dictString[i] != delimiter &&
                   _dictString[i] != '\r' &&
                   _dictString[i] != '\n')
            { 
                i++;
            }
            kanji = _dictString.Substring(_i, i - _i);
            return (kanji);
        }

        Word ParseCurrentWord(string lookingStr)
        {
            Word word = new Word();

            SkipLine();
            if (_dictString[_i] == ':')
            {
                _i++;
                word.Kanji = ParseContent(':');
                SkipLine();
            }
            if (_dictString[_i] == '!')
            {
                _i++;
                word.Kana = ParseContent('!');
            }
            word.Translation = lookingStr;
            return (word);
        }

        public List<Word>   GetWordsByGloss(string lookingStr)
        {
            List<Word> words = new List<Word>();
            int len = _dictString.Length;

            if (string.IsNullOrEmpty(lookingStr) == false)
            { 
                _i = 0;
                while (_i < len)
                {
                    if (_dictString[_i] == ';')
                    {
                        if (IsSearchedWord(lookingStr))
                        {
                            words.Add(ParseCurrentWord(lookingStr));
                        }
                    }
                    _i++;
                }
                return (words.Count == 0 ? null : words);
            }
            return (null);
        }
    }
}
