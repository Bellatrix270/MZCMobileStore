using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MZCMobileStore.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;

[assembly: ExportRenderer(typeof(Entry), typeof(ColorFullEntry))]
namespace MZCMobileStore.Droid
{
    public class ColorFullEntry : EntryRenderer
    {
        public ColorFullEntry(Context context) : base(context)
        {
            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            Control.FocusChange += ControlOnFocusChange;

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Color.Purple);
            else
                Control.Background.SetColorFilter(Color.White, PorterDuff.Mode.SrcAtop);
        }

        private void ControlOnFocusChange(object sender, FocusChangeEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}