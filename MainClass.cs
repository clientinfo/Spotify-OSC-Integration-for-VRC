using BuildSoft.VRChat.Osc;
using BuildSoft.VRChat.Osc.Avatar;
using SharpOSC;
using Spotify_OSC_Integration;
using System.Diagnostics;
using System.Text.RegularExpressions;

public class MainClass
{
    //Textboxflag
    private static bool textBoxToggle = false;

    //Regex for slurs thx to https://github.com/Blank-Cheque
    private static Regex nWordRegex = new Regex(@"\b(s[a4]nd)?n[ila4o10][gq]{1,2}(l[e3]t|[e3]r|[a4]|n[o0]g)?s?\b", RegexOptions.IgnoreCase);
    private static Regex fWordRegex = new Regex(@"f[a@4](g{1,2}|qq)([e3il1o0]t{1,2}(ry|r[i1l]e)?)??s?\b", RegexOptions.IgnoreCase);

    public static async Task Main()
    {
        //sets Console Title
        Console.Title = "Spotify OSC Integration by Purple420";

        Console.WriteLine("Welcome to the Spotify OSC Integration App made by Purple420");

        OscAvatarConfig? avatarConfig = null;

        //Step is for finding the right avatar config in "LocalLow\VRChat\VRChat\OSC\usr_xxxxxxx\Avatars" xxx is your userid on vrchat
        Console.WriteLine($"[NOTIFICATION] Reading now... Try to \"Reset Avatar.\"");

        //waits for the avatar reset option ingame (not needed if you start the programm before vrchat)
        avatarConfig = await OscAvatarConfig.WaitAndCreateAtCurrentAsync();
        Console.WriteLine($"[NOTIFICATION] Read avatar config. Name: {avatarConfig.Name}");

        //Avatar Parameter Change Event
        OscAvatarParameterChangedEventHandler? handler = async (parameter, e) =>
        {
            //Checks the incoming paramter and executes the rights stuff
            switch (parameter.Name)
            {
                //this is in for the memes, got that idea from a friend all it does is closing the game when the parameter is getting recieved
                case "KillVRC":
                    //Checks if the incomming command got the value true
                    if (e.NewValue.ToString() == "True")
                    {
                        //Closes VRChat
                        ProcessStuff.closeProcessByName("VRChat");
                    }
                    break;

                //If that command arrives
                case "Next-Spotify":
                    //check if the command has the value true
                    if (e.NewValue.ToString() == "True")
                    {
                        //skip to next song
                        SpotifyCommands.skipNextSong();
                    }
                    break;

                //if that command arrives
                case "Back-Spotify":
                    //check if the command has the value true
                    if (e.NewValue.ToString() == "True")
                    {
                        //goes to the previous song
                        SpotifyCommands.previousSong();
                    }
                    break;

                //if that command arrives
                case "Pause&Resume-Spotify":
                    //check if the command has the value true
                    if (e.NewValue.ToString() == "True")
                    {
                        //Pause/Play the song dependend on the current song state
                        SpotifyCommands.pauseOrResumeSong();
                    }
                    break;

                //if that command arrives
                case "Volum-Spotify":
                    //Sets the system volume of spotify on the value from the radial
                    VolumeControl.SetVolume((uint)ProcessStuff.getProcessIdByName("Spotify"), (float)e.NewValue * 100);
                    break;

                //if that command arrives
                case "ShowSong-Spotify":

                    //Creates a osc server
                    var sender = new UDPSender("127.0.0.1", OscUtility.SendPort);

                    //check if the command has the value true
                    while (e.NewValue.ToString() == "True")
                    {
                        //Check to leave the loop
                        if (textBoxToggle == true)
                        {
                            //setting flag to false
                            textBoxToggle = false;
                            break;
                        }

                        //Finds the Spotify Process
                        Process[] p = Process.GetProcessesByName("Spotify");

                        //changes Spotify Vanilla Title to Music Paused
                        if (p[0].MainWindowTitle == "Spotify Premium")
                        {
                            //sends the Song title to textbox
                            sender.Send(new OscMessage("/chatbox/input", "[Spotify]: Music Paused", true));
                        }

                        //Filters and Replace the nword
                        else if (fWordRegex.IsMatch(p[0].MainWindowTitle))
                        {
                            string cleanedString = fWordRegex.Replace(p[0].MainWindowTitle, "F*****");
                            //sends cleaned Song title to textbox
                            sender.Send(new OscMessage("/chatbox/input", "[Spotify]: " + cleanedString, true));
                        }

                        //Filters and Replace the fWord
                        else if (nWordRegex.IsMatch(p[0].MainWindowTitle))
                        {
                            string cleanedString = nWordRegex.Replace(p[0].MainWindowTitle, "N*****");
                            //sends cleaned Song title to textbox
                            sender.Send(new OscMessage("/chatbox/input", "[Spotify]: " + cleanedString, true));
                        }

                        else
                        {
                            //sends the Song title to vrchat textbox
                            sender.Send(new OscMessage("/chatbox/input", "[Spotify] playing: " + p[0].MainWindowTitle, true));
                        }

                        //Delay so that the Programm can check for other parameters
                        await Task.Delay(2000);
                    }

                    //Disables the textbox
                    if (e.NewValue.ToString() == "False")
                    {
                        //Command for emptying the text box
                        sender.Send(new OscMessage("/chatbox/input", "", true));
                        //setting flag to true
                        textBoxToggle = true;
                        //Delay so that the Programm can check for other parameters
                        await Task.Delay(2000);
                        break;
                    }
                    break;
            }
        };

        //Avatar Change Event
        OscAvatarUtility.AvatarChanged += (sender, e) =>
        {
            avatarConfig.Parameters.ParameterChanged -= handler;

            avatarConfig = OscAvatarConfig.CreateAtCurrent()!;
            Console.WriteLine($"[NOTIFICATION] Changed avatar. Name: {avatarConfig.Name}");

            avatarConfig.Parameters.ParameterChanged += handler;
        };

        avatarConfig.Parameters.ParameterChanged += handler;

        await Task.Delay(-1);
    }
}