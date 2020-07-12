using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Xml.Serialization;

namespace Memini.Models
{
    [XmlRoot("entry")]
    public class Word
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Theme))]
        public int OwnerID { get; set; }
        [XmlElement("gloss")]
        public string Translation { get; set; }
        [XmlElement("keb")]
        public string Kanji { get; set; }
        [XmlElement("reb")]
        public string Kana { get; set; }

        [ManyToOne]
        public Theme Theme { get; set; }

        public Word()
        {
            Translation = string.Empty;
            Kanji = string.Empty;
            Kana = string.Empty;
        }
    }
}
