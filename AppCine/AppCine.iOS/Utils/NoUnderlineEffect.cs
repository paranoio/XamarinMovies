using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("SuperAwesome")]
[assembly: ExportEffect(typeof(AppCine.iOS.Utils.NoUnderlineEffect), "NoUnderlineEffect")]

namespace AppCine.iOS.Utils
{

    public class NoUnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (this.Control != null)
            {
                var entry = (UITextField)this.Control;
                entry.BorderStyle = UITextBorderStyle.None;
            }
        }
        protected override void OnDetached()
        {
        }
    }
    
}