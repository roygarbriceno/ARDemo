namespace ARDemo.Droid
{
    using Core;
    using MvvmCross.Forms.Platforms.Android.Core;
    using MvvmCross.Logging;
    using MvvmCross;
    using MvvmCross.Forms.Presenters;


    /// <summary>
    /// Android setup class
    /// </summary>
    public class Setup : MvxFormsAndroidSetup<MvxApp, App>
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