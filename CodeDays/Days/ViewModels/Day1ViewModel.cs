using CommunityToolkit.Mvvm.ComponentModel;
using Days.Models;
using System.ComponentModel;

namespace Days.ViewModels
{
    [ObservableObject]
    public partial class Day1ViewModel
    {
        public Day1ViewModel()
        {
            SaveCommand = new Command(HandleSaveCommand);
            ClearCommand = new Command(HandleClearCommand);
            PropertyChanged += Day1ViewModel_PropertyChanged;
            Task.Run(async () => await Initialise());
        }

        private void Day1ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Elves))
            {
                CalculateMaxFood();
            }
        }

        private async Task Initialise()
        {
            var result = string.Empty;
            Elves = await App.LocalDataService.GetAllElvesAndFoodAsync();
            if (Elves == null)
            {
                MostFoodElves = null;
                return;
            }
            foreach(var elf in Elves)
            {
                if (elf.Foods == null) continue;
                foreach (var food in elf.Foods)
                {
                    result += food.Calories + "\r\n";
                }
                result += "\r\n";
            }
            result = result.TrimEnd();
            CaloriesRaw = result;
        }

        [ObservableProperty]
        private string caloriesRaw;

        [ObservableProperty]
        private Command saveCommand;

        [ObservableProperty]
        private Command clearCommand;

        [ObservableProperty]
        private List<Elf> elves;

        [ObservableProperty]
        private List<Elf> mostFoodElves;

        [ObservableProperty]
        private int aggregateCalories;

        private void HandleSaveCommand()
        {
            Task.Run(async() => await PopulateElvesFoodAsync());
        }

        private void HandleClearCommand()
        {
            Task.Run(async() => await ClearInputAsync());
        }

        private void HandleMostFoodCommand()
        {
            CalculateMaxFood();
        }

        private async Task ClearInputAsync()
        {
            await App.LocalDataService.ClearAllElvesAndFoodAsync();
            CaloriesRaw = string.Empty;
            Elves = null;
        }

        private async Task PopulateElvesFoodAsync()
        {
            await App.LocalDataService.ClearAllElvesAndFoodAsync();
            var elvesRaw = CaloriesRaw.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.None);
            var elfNumber = 1;
            foreach (var elfRaw in elvesRaw)
            {

                var funName = await App.LocalDataService.GetRandomFunNameAsync();
                var elf = new Elf
                {
                    Name = funName.Name,
                    Number = elfNumber++
                };
                await App.LocalDataService.InsertAsync(elf);

                var caloriesRaw = elfRaw.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                foreach (var caloryRaw in caloriesRaw)
                {
                    var food = new Food
                    {
                        Calories = int.Parse(caloryRaw),
                        ElfId = elf.Id
                    };
                    await App.LocalDataService.InsertAsync(food);
                }
            }

            Elves = await App.LocalDataService.GetAllElvesAndFoodAsync();
        }

        private void CalculateMaxFood()
        {
            if (Elves == null)
            {
                MostFoodElves = null;
            }

            MostFoodElves = Elves.OrderByDescending(e => e.TotalCalories).Take(3).ToList();
            AggregateCalories = MostFoodElves.Sum(e => e.TotalCalories);
        }

    }
}
