using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Memini.Models
{
    public class Word
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Theme))]
        public int OwnerID { get; set; }
        public string Translation { get; set; }
        public string Kanji { get; set; }
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
