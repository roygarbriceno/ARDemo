namespace ARDemo.Core.Pages
{
    using Xamarin.Forms.Xaml;
    using MvvmCross.Forms.Presenters.Attributes;

    /// <summary>
    /// Main page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = true)]
    public partial class MainPage 
	{
		public MainPage ()
		{
			InitializeComponent ();
		}
	}
}