using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

public class DiceRollerViewModel : INotifyPropertyChanged
{
    private int _numberOfDice;
    private ObservableCollection<string> _rollResults;
    private RollDiceService _diceRoller;
    private string _errorMessage;
    private string _selectedRoll;

    public DiceRollerViewModel()
    {
        _diceRoller = new RollDiceService();
        RollResults = new ObservableCollection<string>();
        RollDiceCommand = new RelayCommand(async () => await RollDiceAsync());
        DeleteSelectedRollCommand = new RelayCommand(DeleteSelectedRoll, CanDeleteSelectedRoll);

        NumberOfDice = 1;
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

    private async Task RollDiceAsync()
    {
        try
        {
            var result = await _diceRoller.RollDiceAsync(NumberOfDice);
            var resultString = string.Join(", ", result);
            RollResults.Insert(0, resultString);
            ErrorMessage = string.Empty;
        }
        catch (ArgumentException ex)
        {
            ErrorMessage = $"Invalid input: {ex.Message}";
        }
        catch (Exception ex)
        {
            ErrorMessage = $"An unexpected error occurred: {ex.Message}";
        }
    }

    private void DeleteSelectedRoll()
    {
        if (SelectedRoll != null)
        {
            RollResults.Remove(SelectedRoll);
        }
    }

    // какие то изменения. 

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
