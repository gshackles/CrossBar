using System;
using Cirrious.MvvmCross.Converters;
using System.Globalization;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace CrossBar.Platform.Converters
{
	public class CollectionEmptyConverter : MvxBaseValueConverter
	{
		public override Object Convert (Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			var collection = (ICollection)value;

			if (collection == null)
				return true;

			return collection.Count == 0;
		}
	}
}

