using Memini.Models;
using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace Memini.Data
{
    public class XmlDict
    {
        private string _xmlString;

        public XmlDict(string xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml (string) argument is null");
            _xmlString = xml;
        }

        public Word GetWord(string lookingStr)
        {
            int entryLine = 0;
            bool found = false;

            try
            { 
                using (StringReader sr = new StringReader(_xmlString))
                {
                    using (XmlTextReader xmlReader = new XmlTextReader(sr))
                    {
                        if (lookingStr != null)
                        {
                            while (xmlReader.ReadToFollowing("entry") == true && found == false)
                            {
                                entryLine = xmlReader.LineNumber;
                                XmlReader entryReader = xmlReader.ReadSubtree();
                                while (entryReader.ReadToFollowing("gloss") == true && entryReader.EOF == false && found == false)
                                {
                                    if (entryReader.ReadInnerXml() == lookingStr)
                                    {
                                        found = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            if (found == true)
            {
                using (StringReader sr = new StringReader(_xmlString))
                {
                    for (int i = 0; i < entryLine - 1; i++)
                    {
                        sr.ReadLine();
                    }
                    string element = String.Empty;

                    string line;
                    while ((line = sr.ReadLine()).Contains("</entry>") == false && line != null)
                    {
                        if (line.Contains(";") == false)
                        {
                            element += line + '\n';
                        }
                    }
                    element += line + '\n';
                    XDocument doc = XDocument.Parse(element);

                    Word word = new Word();

                    if (doc.Descendants("keb").FirstOrDefault() != null)
                        word.Kanji = doc.Descendants("keb").FirstOrDefault().Value;
                    if (doc.Descendants("reb").FirstOrDefault() != null)
                        word.Kana = doc.Descendants("reb").FirstOrDefault().Value;
                    word.Translation = lookingStr;

                    return (word);
                }
            }
            else
            {
                return (null);
            }
        }
    }
}
