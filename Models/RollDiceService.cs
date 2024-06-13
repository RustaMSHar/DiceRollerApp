using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RollDiceService
{
    private static Random _random = new Random();

    public async Task<List<int>> RollDiceAsync(int numberOfDice)
    {
        return await Task.Run(() =>
        {
            if (numberOfDice <= 0) throw new ArgumentException("Number of dice must be greater than zero.");

            var rolls = Enumerable.Range(0, numberOfDice)
                                  .Select(_ => _random.Next(1, 11))
                                  .OrderByDescending(x => x)
                                  .ToList();

            return rolls;
        });
    }
}
