namespace Days.ViewModels
{
    public partial class Day1ViewModel : ViewModelBase
    {
        #region Constructor
        public Day1ViewModel()
        {
            Day = 1;
        }
        #endregion

        protected async override void HandleProcessCommand()
        {
            var result = RawInput.Split(new[] { "\r\n\r\n", "\r\r", "\n\n" }, StringSplitOptions.None)
                .Select(x => x.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).Sum(int.Parse))
                .OrderByDescending(x => x)
                .Take(3)
                .Sum(x => x);

            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day1Pg2");
            base.HandleProcessCommand();
        }


    }
}
