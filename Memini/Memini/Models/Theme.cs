using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.ObjectModel;

namespace Memini.Models
{
    public class Theme
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public ObservableCollection<Word> Words { get; set; } = new ObservableCollection<Word>();
    }
}
