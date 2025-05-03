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
using DMHelper.App.Views;

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
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Database
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

        // Services
        services.AddSingleton<CampaignService>();

        // ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<CampaignViewModel>();

        // Views
        services.AddTransient<MainWindow>();
        services.AddTransient<CampaignView>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.DataContext = _serviceProvider.GetRequiredService<MainWindowViewModel>();

        // Configure CampaignView
        var campaignView = _serviceProvider.GetRequiredService<CampaignView>();
        campaignView.DataContext = _serviceProvider.GetRequiredService<CampaignViewModel>();

        mainWindow.Show();
    }
}

