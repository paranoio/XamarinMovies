using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ResolutionGroupName("SuperAwesome")]
[assembly: ExportEffect(typeof(AppCine.Droid.Utils.NoUnderlineEffect), "NoUnderlineEffect")]
namespace AppCine.Droid.Utils
{
    public class NoUnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (this.Control != null)
            {                
                if (this.Control is EditText editText)
                {
                                        
                    (this.Control as EditText).SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                    
                }                
            }
        }
        protected override void OnDetached()
        {
        }
    }
}