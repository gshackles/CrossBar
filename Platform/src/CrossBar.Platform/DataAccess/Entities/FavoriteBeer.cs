using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace CrossBar.Platform.DataAccess.Entities
{
    public class FavoriteBeer
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }

        public int BeerId { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }
    }
}