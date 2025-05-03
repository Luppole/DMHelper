using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DMHelper.App.Data;
using DMHelper.App.Services;
using DMHelper.App.ViewModels;

namespace DMHelper.App;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private ServiceProvider _serviceProvider;
    private IConfiguration _configuration;

    public App()
    {
        try
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            // Handle unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                MessageBox.Show($"An unhandled exception occurred: {e.ExceptionObject}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            DispatcherUnhandledException += (sender, e) =>
            {
                MessageBox.Show($"An unhandled exception occurred: {e.Exception}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            };
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error during application startup: {ex}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        try
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

            // Services
            services.AddSingleton<CampaignService>();

            // ViewModels
            services.AddTransient<CampaignViewModel>();
            services.AddTransient<MainWindowViewModel>();

            // Views
            services.AddTransient<MainWindow>();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error configuring services: {ex}", "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Error);
            throw;
        }
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"An error occurred while starting the application: {ex}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Shutdown();
        }
    }
}

