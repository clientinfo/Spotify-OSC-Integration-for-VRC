using System.Diagnostics;

namespace Spotify_OSC_Integration
{
    public class ProcessStuff
    {
        //Name says everything t
        public static void closeProcessByName(string processName)
        {
            DateTime now = DateTime.Now;
            foreach (var process in Process.GetProcessesByName(processName))
            {
                Console.WriteLine($"[{now.ToShortDateString()} {now.ToShortTimeString()}] " + processName + " " + "closed");
                process.Kill();
            }
        }

        //Name says everything
        public static IntPtr getWindowHandle(string proccessName)
        {
            var proc = Process.GetProcessesByName(proccessName).FirstOrDefault(p => !string.IsNullOrWhiteSpace(p.MainWindowTitle));
            IntPtr hwnd = proc.MainWindowHandle;
            return hwnd;
        }

        //Name says everything
        public static int getProcessIdByName(string processName)
        {
            Process[] runningProcesses = Process.GetProcesses();
            foreach (Process process in runningProcesses)
            {
                if (process.ProcessName == processName)
                {
                    return process.Id;
                }
            }
            return 0;
        }
    }
}