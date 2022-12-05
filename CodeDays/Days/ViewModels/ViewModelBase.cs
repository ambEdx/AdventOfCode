using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Days.Messages;
using Days.Models;

namespace Days.ViewModels
{
    [ObservableObject]
    public partial class ViewModelBase
    {
        public ViewModelBase()
        {
            WeakReferenceMessenger.Default.Register<DataServiceMessage>(this, (r, m) => StatusMessage = m.Value);
            FetchCommand = new RelayCommand(HandleFetchCommand);
            SaveCommand = new RelayCommand(HandleSaveCommand, () => !string.IsNullOrWhiteSpace(RawInput));
            ClearCommand = new RelayCommand(HandleClearCommand, () => !string.IsNullOrWhiteSpace(RawInput));
            ProcessCommand = new RelayCommand(HandleProcessCommand, () => !string.IsNullOrWhiteSpace(RawInput));
            PropertyChanged += ViewModelBase_PropertyChanged;
        }

        private void ViewModelBase_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RawInput))
            {
                SaveCommand.NotifyCanExecuteChanged();
                ClearCommand.NotifyCanExecuteChanged();
                ProcessCommand.NotifyCanExecuteChanged();
            }
        }

        public int Day { get; set; }

        #region Bindable properties
        [ObservableProperty]
        private string rawInput;

        [ObservableProperty]
        private string statusMessage;

        [ObservableProperty]
        private RelayCommand fetchCommand;

        [ObservableProperty]
        private RelayCommand saveCommand;

        [ObservableProperty]
        private RelayCommand clearCommand;

        [ObservableProperty]
        private RelayCommand processCommand;

        [ObservableProperty]
        private string answer;

        #endregion

        #region Command Handlers
        private async void HandleFetchCommand()
        {
            RawInput = await App.LocalDataService.GetInputOfDayAsync(Day);
            StatusMessage = String.IsNullOrWhiteSpace(RawInput) ? "No raw input to fetch." : "Raw input retrieved.";
        }

        private async void HandleSaveCommand()
        {
            var input = new RawInput { Day = Day, Input = RawInput };
            await App.LocalDataService.ClearInputOfDayAsync(Day);
            await App.LocalDataService.InsertAsync(input);
        }

        private void HandleClearCommand()
        {
            RawInput = null;
        }

        protected virtual void HandleProcessCommand()
        {
            StatusMessage = "We have a result :-)";
        }
        #endregion

    }
}
