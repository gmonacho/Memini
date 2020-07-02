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
        public string V1 { get; set; }
        public string V2 { get; set; }

        [ManyToOne]
        public Theme theme { get; set; }

        public Word()
        {
            V1 = " ";
            V2 = " ";
        }
    }
}
