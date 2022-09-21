using System.Runtime.InteropServices;

namespace Spotify_OSC_Integration
{
    public class SpotifyCommands
    {
        //contains the spotifyAction IDs
        private enum spotifyAction
        {
            NextTrack = 720896,
            PreviousTrack = 786432,
            PlayPause = 917504

        }

        //Including the sendmessage function from the user32.dll
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        //Name says everything
        public static void skipNextSong()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"[{now.ToShortDateString()} {now.ToShortTimeString()}] " + "Skipped to the next Song");

            SendMessage(ProcessStuff.getWindowHandle("Spotify"), 0x0319, (IntPtr)0, (IntPtr)spotifyAction.NextTrack);
        }

        //Name says everything
        public static void previousSong()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"[{now.ToShortDateString()} {now.ToShortTimeString()}] " + "Skipped to previous Song");

            SendMessage(ProcessStuff.getWindowHandle("Spotify"), 0x0319, (IntPtr)0, (IntPtr)spotifyAction.PreviousTrack);
        }

        //Name says everything
        public static void pauseOrResumeSong()
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"[{now.ToShortDateString()} {now.ToShortTimeString()}] " + "Paused/Resumed Song");

            SendMessage(ProcessStuff.getWindowHandle("Spotify"), 0x0319, (IntPtr)0, (IntPtr)spotifyAction.PlayPause);
        }
    }
}