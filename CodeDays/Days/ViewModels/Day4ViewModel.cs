namespace Days.ViewModels
{
    public partial class Day4ViewModel : ViewModelBase
    {
        #region Constructor
        public Day4ViewModel()
        {
            Day = 4;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var result = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                .Select(x => x.Split(',')
                .Select(y => Array.ConvertAll(y.Split('-'), int.Parse).ToArray()).ToArray())
                .Count(x => (x[0][0] <= x[1][0] && x[0][1] >= x[1][0]) || (x[1][0] <= x[0][0] && x[1][1] >= x[0][0]));

            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day4Pg2");
            base.HandleProcessCommand();
        }
        #endregion

    }
}
