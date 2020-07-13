using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace MeminiDictGenerator
{
    class Program
    {

        static void WriteGloss(StreamWriter sr, XElement entry)
        {
            foreach (XElement gloss in entry.Descendants("gloss"))
            {
                string tmp = string.Empty;

                tmp += ';' + gloss.Value;
                sr.Write(tmp);
            }
            sr.Write(Environment.NewLine);
        }

        static void WriteKanji(StreamWriter sr, XElement entry)
        {
            foreach (XElement kanji in entry.Descendants("keb"))
            {
                string tmp = string.Empty;

                tmp += ':' + kanji.Value;
                sr.Write(tmp);
            }
            sr.Write(Environment.NewLine);
        }
        static void WriteKana(StreamWriter sr, XElement entry)
        {
            foreach (XElement kana in entry.Descendants("reb"))
            {
                string tmp = String.Empty;

                tmp += '!' + kana.Value;
                sr.Write(tmp);
            }
            sr.Write(Environment.NewLine);
        }

        static void WriteEntry(StreamWriter sr, XElement entry)
        {
            if (entry.Descendants("gloss").Count() != 0)
                WriteGloss(sr, entry);
            if (entry.Descendants("keb").Count() != 0)
                WriteKanji(sr, entry);
            if (entry.Descendants("reb").Count() != 0)
                WriteKana(sr, entry);
        }

        static void GenerateDict(string dictPath, string exportName)
        {
            FileStream file = File.Create("./../../../Generated/" + exportName);
            StreamWriter sr = new StreamWriter(file);
            XDocument xdoc = XDocument.Load(dictPath);
            foreach (XElement xelement in xdoc.Descendants("entry"))
            {
                WriteEntry(sr, xelement);
            }
            Console.WriteLine("DEBUG");
        }

        static void Main(string[] args)
        {
            GenerateDict("./../../../Resources/JMdict_e.txt", "Jdict.txt");
        }

    }
}
