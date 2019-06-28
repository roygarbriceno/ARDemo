namespace ARDemo.iOS
{
    using Core;
    using MvvmCross.Forms.Platforms.Ios.Core;
    using MvvmCross.Logging;
    using MvvmCross;
    using MvvmCross.Forms.Presenters;
        
    /// <summary>
    /// Android setup class
    /// </summary>
    public class Setup : MvxFormsIosSetup<MvxApp, App>
    {
        /// <summary>
        /// Sets the log provider
        /// </summary>
        /// <returns></returns>
        public override MvxLogProviderType GetDefaultLogProviderType()
            => MvxLogProviderType.Console;


        /// <summary>
        /// Register the form presenter (MvvmCross)
        /// </summary>        
        protected override IMvxFormsPagePresenter CreateFormsPagePresenter(IMvxFormsViewPresenter viewPresenter)
        {
            var formsPresenter = base.CreateFormsPagePresenter(viewPresenter);
            Mvx.IoCProvider.RegisterSingleton(formsPresenter);
            return formsPresenter;
        }

        
    }
}