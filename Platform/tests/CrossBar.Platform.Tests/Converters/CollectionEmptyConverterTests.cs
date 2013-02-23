using System;
using NUnit.Framework;
using CrossBar.Platform.Converters;
using System.Collections.Generic;

namespace CrossBar.Platform.Tests.Converters
{
	[TestFixture]
	public class CollectionEmptyConverterTests
	{
		[Test]
		public void Convert_NotACollection_ThrowsInvalidCastException()
		{
			var converter = new CollectionEmptyConverter ();
			var toConvert = new object ();

			Assert.Throws<InvalidCastException>(() =>
				converter.Convert (toConvert, null, null, null));
		}

		[Test]
		public void Convert_NullCollection_ReturnsTrue()
		{
			var converter = new CollectionEmptyConverter ();
			List<object> toConvert = null;
			
			var converted = converter.Convert (toConvert, null, null, null);
			
			Assert.AreEqual (true, converted);
		}

		[Test]
		public void Convert_EmptyCollection_ReturnsTrue()
		{
			var converter = new CollectionEmptyConverter ();
			var toConvert = new List<string> ();
			
			var converted = converter.Convert (toConvert, null, null, null);
			
			Assert.AreEqual (true, converted);
		}

		[Test]
		public void Convert_NonEmptyCollection_ReturnsFalse()
		{
			var converter = new CollectionEmptyConverter ();
			var toConvert = new List<object> { "foo" };
			
			var converted = converter.Convert (toConvert, null, null, null);
			
			Assert.AreEqual (false, converted);
		}
	}
}

