# RobotDotNet WPILib
<a href="https://www.myget.org/"><img src="https://www.myget.org/BuildSource/Badge/robotdotnet-build?identifier=48b0fb66-4701-429a-b76e-bae9c28bbef7" alt="robotdotnet-build MyGet Build Status" /></a> Note that this is failing because the build server does not support C# 6.0 yet. 

This repository contains the source code for a DotNet implentation of the WPILib. 

Documentation
=============

Install and Support documentation can be found at http://robotdotnet-wpilib.readthedocs.org/

API documentation can be found at http://robotdotnet.github.io/Documentation/API/html/R_Project_RobotDotNet.htm

Installation
============

The easist way to use WPILib is to install our extension from the Visual Studio gallery. The extension is called FRC Extension. Just open the VS extension manager, and search for FRC Extension, then install it.

Compiling
=========

Compiling the WPILib currently requires Visual Studio 2015 RC or Mono 4.0. This is because the project uses some C# 6.0 features. Since the program compiles down to a .Net 4.5 program, the library however can be used with VS 2013, or Mono 3.

When you build the project in VS, the DLLs can be found in the Output\ folder. To reference this in your project, you first must uninstall the WPILib using the NuGet packet manager. Then reference the files found in the output folder. Note that if you do this you will loose intellisense for any overriden functions, because the intellisense for those is generated during the release process.

If you do find a bug that you need fixed, or a new feature to add, submit a pull request and we would be happy to look at it.

Projects
========
* `WPILib` - DotNet implementation of the WPILib
* `WPILib.Extras` 
* `HAL-Base` - The base functionality for interfacing with the HAL
* `HAL-RoboRIO` - HAL-Base interface to the HAL C library
* `HAL-Simulation` - TOTO - Will write HAL data to a data structure for simulations.

License
=======
See [LICENSE.txt](LICENSE.txt)

Contributors
============

Thad House (@thadhouse)
Jeremy Koritzinsky (@jkoritzinsky)

Code for simulator derived from the following sources:
* RobotPy (@robotpy)
* Team254 (@team254)
