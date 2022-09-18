This Repo Provides everything you need to Integrate Spotify Controlls to your Avatar

Pic ingame:

![image](https://user-images.githubusercontent.com/54999075/190461308-42b1dd1d-bfa2-49bf-b0a0-801a20a7b487.png)
![image](https://user-images.githubusercontent.com/54999075/190461366-ad759ee3-6367-42f9-afe8-f8d148c5e4fb.png)
![image](https://user-images.githubusercontent.com/54999075/190461491-3e78ab36-1fda-4fe0-885d-811403ae4e0e.png)

Pic from tool:

![image](https://user-images.githubusercontent.com/54999075/190462284-5469ec16-a04d-426f-b581-2695bb65ad75.png)




Unity Usage:
1. Add the needed parameters to your avi (you can find it in "Spotify OSC Integration/Avatar Menu Stuff")
2. Add the "Spotify Menu" to your avi (you can find it "Spotify OSC Integration/Avatar Menu Stuff)
3. (Not Required) but if you want you can use the random icons i found in the internet and apply those to the menu
4. Upload your avi.

Game Steps:
1. Start my programm
2. Start VRChat
3. Turn on OSC before switching into your avi
4. Switch into your avi
5. Reset your Avi if needed (Check Console)
6. Have Fun

Issues:

What if nothing happens after the unity steps?
0. Close Vrchat
1. Press Win + R
2. Enter "%appdata%
3. Press Enter
4. Click on Appdata on the left of the windows searchbar
5. Navigate to "LocalLow\VRChat\VRChat\OSC\usr_xxxxxxx\Avatars" xxx is your userid on vrchat
6. Delete all those files (goes faster than trying to find you avatar id and delete that json file
7. Restart my programm
7.1 Relaunch VRChat
8. Turn on "OSC" if not already done
9. Switch into your avi
10. Everything should work now

Bug:
 - Textbox which shows the current song sometimes doesnt turn off when you want it to, than just relaunche my programm & reset avi

Disclaimer: I do not Provide Updates if that tool breaks and wont provide any support if you dont know what to do.

Credits:
 - https://github.com/ValdemarOrn/SharpOSC (@ValdemarOrn for their great OSC lib which made it easier to send the OSC command for the textbox)
 - https://github.com/ChanyaVRC/VRCOscLib (@ChanyaVRC for their good OSC Lib which made it easier to recieve the avatar params over OSC)
 - Also thanks to @Simon Mourier from stackoverflow for providing an easy to use Class for audio controlling
 - Icons are from the internet and if you are the owner of them and dont want me to use them than pls create an issue on this repo :D
 
