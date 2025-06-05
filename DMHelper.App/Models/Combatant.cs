using CommunityToolkit.Mvvm.ComponentModel;

namespace DMHelper.App.Models;

public partial class Combatant : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private int _initiative;

    [ObservableProperty]
    private int _hitPoints;

    [ObservableProperty]
    private bool _isPlayer;

    [ObservableProperty]
    private bool _isCurrentTurn;
}
