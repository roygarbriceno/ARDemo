namespace ARDemo.Core.ViewModels
{
    using MvvmCross.Commands;
    using MvvmCross.Logging;
    using MvvmCross.Navigation;
    using System.Windows.Input;

    /// <summary>
    /// Main View (tabs))
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private IMvxCommand showArViewCommand;

        /// <summary>
        /// Shwos the AR View
        /// </summary>
        public ICommand ShowArViewCommand
        {
            get
            {
                return showArViewCommand ?? (showArViewCommand = new MvxCommand(async () =>
                {
                    await this.NavigationService.Navigate<ARViewModel>();
                }));
            }
        }


        /// <summary>
        /// Gets by DI the required services
        /// </summary>
        public MainViewModel(IMvxLogProvider logProvider,
            IMvxNavigationService navigationService) : base(logProvider, navigationService)
        {

        }

    }
}
