namespace ARDemo.Core.ViewModels
{
    using MvvmCross.ViewModels;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;


    /// <summary>
    /// Base class for all ViewModels
    /// </summary>
    public abstract class BaseViewModel : MvxNavigationViewModel
    {      
        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseViewModel(IMvxLogProvider logProvider, IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {       
        }
    }
}
