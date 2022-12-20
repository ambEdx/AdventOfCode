using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Days.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Text.Json;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace Days.ViewModels
{
    public partial class Day13ViewModel : ViewModelBase
    {
        #region Constructor
        public Day13ViewModel()
        {
            Day = 13;
        }
        #endregion


        #region Overrides
        protected async override void HandleProcessCommand()
        {
            var result = 0;

            var lines = RawInput.ReplaceLineEndings().Replace(CRCR, CR).Split(CR);

            // Part 2
            //var firsts = RawInput
            //    .Replace("[]", "0").Replace("[", "").Replace("]", "")
            //    .ReplaceLineEndings().Replace(CRCR, CR)
            //    .Split(CR)
            //    .Select(x => int.Parse(x.Split(",").First()))
            //    .ToList();
            //var two = firsts.Count(n => n < 2) + 1;
            //var six = firsts.Count(n => n < 6) + 2;
            //result = two * six;


            for (int i = 0; i < lines.Length; i += 2)
            {
                var lhs = JsonSerializer.Deserialize<dynamic>(lines[i]);
                var rhs = JsonSerializer.Deserialize<dynamic>(lines[i + 1]);

                var v = IsValid(lhs, rhs);
                if (v < 0)
                {
                    result += i / 2 + 1;

                }
            }
            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day13Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers
        private int IsValid(JsonElement lhs, JsonElement rhs)
        {
            if (lhs.ValueKind == JsonValueKind.Number && rhs.ValueKind == JsonValueKind.Number)
            {
                return lhs.GetInt32() - rhs.GetInt32();
            }
            else if (lhs.ValueKind == JsonValueKind.Number)
            {
                return IsValid(JsonSerializer.Deserialize<dynamic>($"[{lhs}]"), rhs);
            }
            else if (rhs.ValueKind == JsonValueKind.Number)
            {
                return IsValid(lhs, JsonSerializer.Deserialize<dynamic>($"[{rhs}]"));
            }

            var max = Math.Max(lhs.GetArrayLength(), rhs.GetArrayLength());
            {
                for (int i = 0; i < max; i++)
                {
                    if (lhs.GetArrayLength() - 1 < i) return -1;
                    if (rhs.GetArrayLength() - 1 < i) return 1;
                    var isValid = IsValid(lhs[i], rhs[i]);
                    if (isValid != 0) return isValid;
                }
            }
            return 0;
        }
        #endregion
    }
}
