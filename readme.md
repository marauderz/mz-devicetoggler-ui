# Marauderz Device Toggler

## Background

So I decided to try using a USB4 eGPU board and realized pretty quickly that due to *quirks*
I'd get BSODs if I tried to disconnect the eGPU without disabling the
video card first.

I tried to make something simple to toggle the device via...

- BAT batch file scripting using pnputil, needed me to grab the Instance ID off device manager manually... not very elegant
- PS1 Powershell scripting, could find the device by name, but decided that getting a machine setup to run PS1 files wasn't fun
- C# console program, worked great... but I realized if I forgotten wheter the state the device was in, the terminal would just flash open and I couldn't see the status...

So in the end I ended up with THIS, a UI to search for a device and then try to toggle the status.


## Usage

- Program must be run as administrator
- Type the name of your device in the box and press **Find** or Enter
- Navigate the list to find the device you want to toggle
- Once you selected the device you want to toggle press the **Toggle** button
- The name of the device you last toggled will be saved so the next time you start the program you won't need to find it again.

