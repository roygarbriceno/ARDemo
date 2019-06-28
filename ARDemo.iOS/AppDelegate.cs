using ARDemo.Core;
using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;

namespace ARDemo.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<MvxApp, App>, MvxApp, App>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(65, 105, 225);
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(255, 255, 255);

            return base.FinishedLaunching(app, options);
        }
    }

}


