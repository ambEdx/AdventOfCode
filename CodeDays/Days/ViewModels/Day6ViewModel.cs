using Microsoft.Maui.Controls.Shapes;

namespace Days.ViewModels
{
    public partial class Day6ViewModel : ViewModelBase
    {
        #region Constructor
        public Day6ViewModel()
        {
            Day = 6;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var markerCount = 14;

            var result = RawInput
                .Select((chr, index) => (chr, index))
                .First(c => RawInput.Substring(c.index, markerCount)
                    .Distinct()
                    .Count() == markerCount)
                .index + markerCount;

            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day6Pg2");
            base.HandleProcessCommand();
        }
        #endregion
    }
}
