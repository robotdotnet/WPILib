.. _getting_started:

Getting Started
===============

Please note that we currently only have documentation for C#. VB support has been minimally tested, but it should work.

RobotDotNet and programs built from it can be compiled using Mono on Linux and Mac OSX, but is currently best supported using Windows and Visual Studio. Here's how to get started.


Online Installation - Windows
------------------------------

If you do not already have Visual Studio 2013 or newer installed, then you will need to install Visual Studio. We recommend Visual Studio 2015 Community, which can be found 'here. <https://www.visualstudio.com/products/vs-2015-product-editions>' The license for the Community edition allows for education use, so it can be used for our purposes.

After Visual Studio has been installed, open it, and then go to Tools | Extensions and Updates. Then select Online and search for "FRC Extension". Click download on the FRC Extension. 

If your RoboRIO has not been setup for RobotDotNet yet, please see SetupRIO for provisioning instructions. Also please make sure to set your Team Number, by going to FRC | Team Number in Visual Studio after installing the Extension.

Now that your RoboRIO has been setup, and the FRC extensions have been installed into Visual Studio, you are now ready to create some code and upload it to the robot. Please see our Programmer's Guide for instructions on this.


Manual Installation - Windows
-----------------------------

If you do not already have Visual Studio 2013 or newer installed, then you will need to install Visual Studio. We recommend Visual Studio 2015 Community, which can be found 'here. <https://www.visualstudio.com/products/vs-2015-product-editions>' The license for the Community edition allows for education use, so it can be used for our purposes.

An offline version of the FRC Extension can be found 'here. <https://visualstudiogallery.msdn.microsoft.com/7c7f4cd1-e4bc-43bb-a9f1-072c6f1197d9>'

Run the MSI installer, which will install the FRC Extension to Visual Studio

If your RoboRIO has not been setup for RobotDotNet yet, please see SetupRIO for provisioning instructions. Also please make sure to set your Team Number, by going to FRC | Team Number in Visual Studio after installing the Extension.

Now that your RoboRIO has been setup, and the FRC extensions have been installed into Visual Studio, you are now ready to create some code and upload it to the robot. Please see our Programmer's Guide for instructions on this.

Linux and Mac Instructions
---------------------------

TODO


Upgrading the Extension
-----------------------

If your developer system is connected to the internet, you can install updates to the extention using the Tools | Extensions and Updates utility. If updates are available, they will show up in the Updates section.

If your developer system is not connected to the internet, you can download the offline version of the installer and run it. That will update the extentions and tools to the latest version. 