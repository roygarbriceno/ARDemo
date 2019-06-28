namespace ARDemo.Core.ViewModels
{
    using MvvmCross.Logging;
    using MvvmCross.Navigation;

    /// <summary>
    /// The AR Logic
    /// </summary>
    public class ARViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public ARViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }

    }
}
