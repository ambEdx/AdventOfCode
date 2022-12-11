using System.Numerics;
using Point = System.Drawing.Point;

namespace Days.ViewModels
{
    public partial class Day11ViewModel : ViewModelBase
    {
        #region Constructor
        public Day11ViewModel()
        {
            Day = 11;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var monkeys = new Dictionary<int, Monkey>();

            var monkeyLines = RawInput.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.None);
            foreach (var monkeyLine in monkeyLines)
            {
                var monkeyItems = monkeyLine.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                var operation = monkeyItems[2].Replace("  Operation: new = old ", "").Replace(" ", "");
                var monkey = new Monkey
                {
                    Num = int.Parse(monkeyItems[0].Replace("Monkey ", "").Replace(":", "")),
                    WorryLevels = new Queue<ulong>(monkeyItems[1].Replace("  Starting items: ", "").Split(",").Select(i => ulong.Parse(i))),
                    OperationOperator = operation[0],
                    OperationValue = operation.Substring(1),
                    DivisibleBy = ulong.Parse(monkeyItems[3].Replace("  Test: divisible by ", "")),
                    ThrowToIfTrue = int.Parse(monkeyItems[4].Replace("    If true: throw to monkey ", "")),
                    ThrowToIfFalse = int.Parse(monkeyItems[5].Replace("    If false: throw to monkey ", "")),
                    InspectionCount = 0
                };
                monkeys.Add(monkey.Num, monkey);
            }

            var leastCommonMultiple = LeastCommonMultipleOfMany(monkeys.Values.Select(m => m.DivisibleBy).ToArray());

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys.Values)
                {
                    monkey.InspectionCount += monkey.WorryLevels.Count;
                    while (monkey.WorryLevels.Count > 0)
                    {
                        var worryLevel = monkey.WorryLevels.Dequeue();
                        ulong operationValue = monkey.OperationValue == "old" ? worryLevel : ulong.Parse(monkey.OperationValue);
                        switch (monkey.OperationOperator)
                        {
                            case '+': worryLevel += operationValue; break;
                            case '*': worryLevel *= operationValue; break;
                        }
                        var catchingMonkey = worryLevel % monkey.DivisibleBy == 0 ? monkeys[monkey.ThrowToIfTrue] : monkeys[monkey.ThrowToIfFalse];
                        worryLevel %= leastCommonMultiple;
                        catchingMonkey.WorryLevels.Enqueue(worryLevel);
                    }
                }
            }

            var activeMonkeys = monkeys.Values.Select(m => m.InspectionCount).OrderByDescending(c => c).Take(2).ToList();
            BigInteger result = BigInteger.Multiply(activeMonkeys[0], activeMonkeys[1]);

            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day11Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers
        private ulong LeastCommonMultipleOfMany(ulong[] numbers)
        {
            return numbers.Aggregate(LeastCommonMultipleOfPair);
        }

        private ulong LeastCommonMultipleOfPair(ulong a, ulong b)
        {
            return (a / GreatestCommonDenominator(a, b)) * b;
        }
        private ulong GreatestCommonDenominator(ulong a, ulong b)
        {
            while (b != 0)
            {
                var tmp = b;
                b = a % b;
                a = tmp;
            }
            return a;
        }

        private class Monkey
        {
            public int Num;
            public Queue<ulong> WorryLevels;
            public char OperationOperator;
            public string OperationValue;
            public ulong DivisibleBy;
            public int ThrowToIfTrue;
            public int ThrowToIfFalse;
            public int InspectionCount;
        }
        #endregion
    }
}
