using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DMHelper.App.Models;

namespace DMHelper.App.ViewModels;

public partial class CombatTrackerViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Combatant> _combatants = new();

    [ObservableProperty]
    private Combatant? _selectedCombatant;

    [RelayCommand]
    private void AddCombatant()
    {
        var combatant = new Combatant { Name = "New", HitPoints = 1 };
        Combatants.Add(combatant);
        SelectedCombatant = combatant;
    }

    [RelayCommand]
    private void RemoveCombatant()
    {
        if (SelectedCombatant != null)
        {
            Combatants.Remove(SelectedCombatant);
            SelectedCombatant = null;
        }
    }

    [RelayCommand]
    private void StartEncounter()
    {
        foreach (var c in Combatants)
            c.IsCurrentTurn = false;
        var sorted = Combatants.OrderByDescending(c => c.Initiative).ToList();
        Combatants.Clear();
        foreach (var c in sorted)
            Combatants.Add(c);
        if (Combatants.Count > 0)
            Combatants[0].IsCurrentTurn = true;
    }

    [RelayCommand]
    private void NextTurn()
    {
        if (Combatants.Count == 0)
            return;
        var index = Combatants.ToList().FindIndex(c => c.IsCurrentTurn);
        if (index >= 0)
            Combatants[index].IsCurrentTurn = false;
        index = (index + 1) % Combatants.Count;
        Combatants[index].IsCurrentTurn = true;
    }

    [RelayCommand]
    private void RollInitiative(Combatant combatant)
    {
        var rnd = new Random();
        combatant.Initiative = rnd.Next(1, 21);
    }
}
