using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DMHelper.App.ViewModels;
using DMHelper.App.Views;

namespace DMHelper.App;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly CampaignView _campaignView;

    public MainWindow(CampaignView campaignView)
    {
        _campaignView = campaignView;
        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        if (CampaignView != null)
        {
            CampaignView.DataContext = _campaignView.DataContext;
        }
    }
}