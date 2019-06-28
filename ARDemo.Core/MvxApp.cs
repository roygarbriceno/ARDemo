namespace ARDemo.Core
{
    using MvvmCross.IoC;
    using MvvmCross.ViewModels;
    using ViewModels;
    
    /// <summary>
    /// MvvmCross Application
    /// </summary>
    public class MvxApp : MvxApplication
    {
        /// <summary>
        /// Initialize the services and defines the first viewmodel
        /// </summary>
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            RegisterAppStart<MainViewModel>();            
        }
    }
}
