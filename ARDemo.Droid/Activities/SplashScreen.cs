namespace ARDemo.Droid.Activities
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Core;
    using System.Threading.Tasks;
    using MvvmCross.Forms.Platforms.Android.Views;

    /// <summary>
    /// Splash Screen
    /// </summary>
    [Activity(
        Label = "@string/appName",
        MainLauncher = true,
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/Theme.Splash",
        NoHistory = true,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxFormsSplashScreenActivity<Setup, MvxApp, App>
    {
        /// <summary>
        /// Sets the activity to run when started
        /// </summary>        
        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return base.RunAppStartAsync(bundle);
        }
    }
}