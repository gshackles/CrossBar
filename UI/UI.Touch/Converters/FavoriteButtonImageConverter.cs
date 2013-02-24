using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Cirrious.MvvmCross.Converters;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CrossBar.UI.Converters
{
    public class FavoriteButtonImageConverter : MvxBaseValueConverter
    {
        public override Object Convert (Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            var isFavorite = (bool)value;

            return isFavorite
                    ? UIImage.FromFile("Resources/favorite_on.png")
                    : UIImage.FromFile("Resources/favorite_off.png");
        }
    }
}
