using Cirrious.MvvmCross.Plugins.Sqlite;

namespace CrossBar.Platform.DataAccess.Entities
{
    public class FavoriteBrewery
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public int BreweryId { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }
    }
}