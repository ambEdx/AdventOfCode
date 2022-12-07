using Microsoft.Maui.Controls.Shapes;

namespace Days.ViewModels
{
    public partial class Day7ViewModel : ViewModelBase
    {
        #region Constructor
        public Day7ViewModel()
        {
            Day = 7;
        }
        #endregion

        #region Overrides
        protected async override void HandleProcessCommand()
        {
            const string ROOT = "/";

            var directories = new Dictionary<string, SysDir>();
            var sysDir = new SysDir { Name = ROOT, ParentName = string.Empty, SysFiles = new List<SysFiles>() };
            directories.Add(sysDir.Name, sysDir);

            var lines = RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (line == "$ cd " + ROOT || line.StartsWith("dir") || line == "$ ls")
                {
                    continue;
                }
                else if (line == "$ cd ..")
                {
                    if (sysDir.Name != ROOT)
                    {
                        sysDir = directories[sysDir.ParentName];
                    }
                }
                else if (line.StartsWith("$ cd"))
                {
                    var dirName = line.Replace("$ cd ", "");
                    if (!directories.ContainsKey(dirName))
                    {
                        directories.Add(dirName, new SysDir { Name = dirName, ParentName = sysDir.Name, SysFiles = new List<SysFiles>() });
                    }
                    sysDir = directories[dirName];
                }
                else if (char.IsDigit(line[0]))
                {
                    var fileData = line.Split(" ");
                    var sysFile = new SysFiles { Name = fileData[1], Size = int.Parse(fileData[0]) };
                    sysDir.SysFiles.Add(sysFile);

                    var memberDir = sysDir;
                    while (true)
                    {
                        memberDir.Size += sysFile.Size;
                        if (memberDir.Name == ROOT)
                        {
                            break;
                        }
                        memberDir = directories[memberDir.ParentName];
                    }
                }
            }

            var discSpace = 70000000;
            var requiredSpace = 30000000;
            var unusedSpace = discSpace - directories[ROOT].Size;
            var result = directories.Values.Select(v => v.Size).Where(s => s + unusedSpace > requiredSpace).Min();

            Answer = result.ToString();

            await Shell.Current.GoToAsync("//Day7Pg2");
            base.HandleProcessCommand();
        }
        #endregion

        #region Private Helpers

        private class SysFiles
        {
            public string Name;
            public int Size;
        }

        private class SysDir
        {
            public string Name;
            public string ParentName;
            public List<SysFiles> SysFiles;
            public int Size;
        }
        #endregion
    }
}
