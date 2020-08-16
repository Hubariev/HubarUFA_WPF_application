using MagisterkaApp.Domain;
using MagisterkaApp.Repo.Abstractions;
using MagisterkaApp.Repo.Database;
using MagisterkaApp.Repo.Repositories;
using Prism.Unity;
using System;
using System.Windows;
using Unity;

namespace MagisterkaApp.UI.Startup
{
    public class Bootstrapper: UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.MeasureWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Container.RegisterType(typeof(IMeasureRepository), typeof(MeasureRepository));
            Container.RegisterType(typeof(IMeasureLiteDbContext), typeof(MeasureLiteDbContext));
            Container.RegisterType(typeof(IFrequenceStepLiteDbContext), typeof(FrequenceStepLiteDbContext));
        }
    }
}
