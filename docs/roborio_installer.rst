.. _roborio_installer:

Installing Software on the RoboRIO
==================================

Before you can run RobotDotNet, you must install Mono and the HAL onto the RoboRIO. Once you have the Visual Studio extention installed, the install tool can easily be started by opening Visual Studio, and going to FRC | Install Tool.

Make sure that the RoboRIO is powered and connected to your computer using either USB or Ethernet. Please do not perform this over WiFi. Also make sure your RoboRIO is imaged before doing this. If it is not, the RoboRIO imaging tool can be opened from within the Install Tool.

Once you are ready to go please follow these instructions:

1. Click on the "Connect to RoboRIO" button. This will start the connection to the RoboRIO.
2. Click on the "Check for Newest Versions" button. 
3. Follow the next set of steps for both Mono and the HAL.
4. If the newest version of the software is not downloaded onto your system, the "Download ***" button will be red. Click the button to download the newest version of the software onto your system.
5. The "Install ***" button will be in 1 of 4 states.
	* Green - Newest version of the software installed on RoboRIO already. Click to reinstall the software.
	* Yellow - Older version of the software installed on RoboRIO. Click to install the newest software.
	* Red - No software installed on the RoboRIO. Click to install the newest software.
	* Disabled - No software downloaded on your machine, or no connection to the RoboRIO has been made.

Updating Software on the RoboRIO
--------------------------------

If you would like to update the software on the RoboRIO, please follow the instructions above. If you are not connected to the internet, skip instructions 2 and 4. Note if you have not done the download steps beforehand, you will not be able to install the software onto the RoboRIO until you have an internet connects. We will be adding an import and export button at some point to make this requirement less restrcting. For now, if you need to download the files on another system, contact us and we will give instructions on where to place the files.