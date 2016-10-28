using System;
using System.Runtime.InteropServices;
using Scaffolder.Core.Base;
using Scaffolder.Core.Data;
using Scaffolder.Core.Engine.MySql;
using Scaffolder.Core.Engine.Sql;
using Scaffolder.Core.Meta;
using NLog;

namespace Scaffolder.Core.Engine
{
    public class Executor
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        public bool Execute(string command)
        {
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            bool isLinux = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            bool isMacOSX = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            try
            {
                System.Diagnostics.Process p = null;

                if (isLinux || isMacOSX)
                {
                    //System.Diagnostics.Process.Start("/bin/bash", "-c \"echo '12345d' >> /Users/andrew/Projects/test/2.txt\"");
                    p = System.Diagnostics.Process.Start($"/bin/bash", $"-c \"{command}\"");
                }

                if (isWindows)
                {
                    p = System.Diagnostics.Process.Start($"cmd.exe", $"/c \"{command}\"");
                }

                p?.WaitForExit();

                return true;

            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error on restart command executing");
                return false;
            }

        }
    }
}