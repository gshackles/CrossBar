using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Cirrious.MvvmCross.Binding.Bindings.Target;
using Cirrious.MvvmCross.Binding.Interfaces;

namespace CrossBar.UI.Bindings
{
    public class FavoriteButtonBinding : MvxBaseTargetBinding
    {
        private static readonly UIImage OnImage = UIImage.FromFile("Content/favorite_on.png");
        private static readonly UIImage OffImage = UIImage.FromFile("Content/favorite_off.png");
        private readonly UIButton _button;

        public FavoriteButtonBinding(UIButton button)
        {
            _button = button;
        }

        public override void SetValue(object value)
        {
            var isFavorite = (bool)value;
            _button.SetImage(isFavorite ? OnImage : OffImage, UIControlState.Normal);
        }

        public override Type TargetType
        {
            get { return typeof(bool); }
        }

        public override Cirrious.MvvmCross.Binding.Interfaces.MvxBindingMode DefaultMode
        {
            get { return MvxBindingMode.OneWay; }
        }
    }
}
