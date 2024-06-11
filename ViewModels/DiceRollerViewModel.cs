using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

public class DiceRollerViewModel : INotifyPropertyChanged
{
    private int _numberOfDice;
    private ObservableCollection<string> _rollResults;
    private RollResult _diceRoller;
    private string _errorMessage;
    private string _selectedRoll;

    public DiceRollerViewModel()
    {
        _diceRoller = new RollResult();
        RollResults = new ObservableCollection<string>();
        RollDiceCommand = new RelayCommand(RollDice);
        DeleteSelectedRollCommand = new RelayCommand(DeleteSelectedRoll, CanDeleteSelectedRoll);
    }

    public int NumberOfDice
    {
        get => _numberOfDice;
        set
        {
            _numberOfDice = value;
            OnPropertyChanged();
        }
    }

    public string SelectedRoll
    {
        get => _selectedRoll;
        set
        {
            _selectedRoll = value;
            OnPropertyChanged();
            ((RelayCommand)DeleteSelectedRollCommand).RaiseCanExecuteChanged();
        }
    }

    public ObservableCollection<string> RollResults
    {
        get => _rollResults;
        set
        {
            _rollResults = value;
            OnPropertyChanged();
        }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set
        {
            _errorMessage = value;
            OnPropertyChanged();
        }
    }

    public ICommand RollDiceCommand { get; }
    public ICommand DeleteSelectedRollCommand { get; }

    private void RollDice()
    {
        try
        {
            var result = _diceRoller.RollDice(NumberOfDice);
            foreach (var item in result)
            {
                RollResults.Insert(0, item);
            }
            ErrorMessage = string.Empty;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private void DeleteSelectedRoll()
    {
        if (SelectedRoll != null)
        {
            RollResults.Remove(SelectedRoll);
        }
    }

    private bool CanDeleteSelectedRoll()
    {
        return SelectedRoll != null;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
