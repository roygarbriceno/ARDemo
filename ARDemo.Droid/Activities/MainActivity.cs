namespace ARDemo.Droid.Activities
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Core.ViewModels;
    using MvvmCross.Forms.Platforms.Android.Views;

    /// <summary>
    /// Main activity 
    /// </summary>
    [Activity(
        Theme = "@style/AppTheme",
        Label = "@string/appName",
        Icon = "@mipmap/ic_launcher",
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : MvxFormsAppCompatActivity<StartUpViewModel>
    {
        public static MainActivity Instance;


        /// <summary>
        /// Setups resources
        /// </summary>
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Droid.Resource.Layout.Toolbar;
            TabLayoutResource = Droid.Resource.Layout.Tabbar;
            base.OnCreate(bundle);
            Instance = this;
        }


        /// <summary>
        /// /Initializes forms
        /// </summary>
        public override void InitializeForms(Bundle bundle)
        {
            base.InitializeForms(bundle);
        }

    }
}

