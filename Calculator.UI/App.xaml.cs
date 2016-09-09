using Calculator.Contracts;
using Calculator.Services;
using Ninject;

using System.Windows;

namespace Calculator.UI
{
    using Calculator.Contracts.ServiceContracts;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            Current.MainWindow = container.Get<MainWindow>();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            container = new StandardKernel();
            container.Bind<ICalculatorService>().To<CalculatorService>().InSingletonScope();
            //container.Bind<IServiceFactory>().To<ServiceFactory>();

        }

    }
}
