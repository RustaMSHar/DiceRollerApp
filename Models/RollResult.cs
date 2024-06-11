using System;
using System.Collections.ObjectModel;
using System.Linq;

public class RollResult
{
    private static Random _random = new Random();

    public ObservableCollection<string> RollDice(int numberOfDice)
    {
        if (numberOfDice <= 0) throw new ArgumentException("Number of dice must be greater than zero.");

        var rolls = new ObservableCollection<int>(
            Enumerable.Range(0, numberOfDice)
                      .Select(_ => _random.Next(1, 11))
                      .OrderByDescending(x => x));

        var result = string.Join(", ", rolls);
        return new ObservableCollection<string> { result };
    }
}
