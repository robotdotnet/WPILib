# RobotDotNet WPILib
[![Build status](https://ci.appveyor.com/api/projects/status/owip0u906jj6j574/branch/master?svg=true)](https://ci.appveyor.com/project/robotdotnet/robotdotnet-wpilib/branch/master)   [![codecov.io](https://codecov.io/github/robotdotnet/WPILib/coverage.svg?branch=master)](https://codecov.io/github/robotdotnet/WPILib?branch=master)

This repository contains the source code for a DotNet implementation of the WPILib. 

Documentation
=============

Install and Support documentation can be found [Here.](http://robotdotnet.github.io/Documentation/API/html/97ea22ce-3980-446f-96c5-2d89871a71e8.htm)

API documentation can be found [Here.](http://robotdotnet.github.io/Documentation/API/html/R_Project_RobotDotNet.htm)

Installation
============

The easiest way to use WPILib is to install our extension from the Visual Studio gallery. The extension is called FRC Extension. Just open the VS extension manager, and search for FRC Extension, then install it.

More instructions can be found [Here.](http://robotdotnet.github.io/Documentation/API/html/c85aadff-f6d3-48a2-8453-ac3eb71b06c5.htm)

Compiling
=========

Compiling the WPILib currently requires Visual Studio 2015+ or Mono 4.0+. This is because the project uses some C# 6.0 features. Since the program compiles down to a .NET 4.5 program, the library however can be used with VS 2013, or Mono 3.

When you build the project in VS, the DLLs can be found in the Output\ folder. To reference this in your project, you first must uninstall the WPILib using the NuGet packet manager. Then reference the files found in the output folder. Note that if you do this you will lose intellisense for any overriden functions, because the intellisense for those is generated during the release process.

If you do find a bug that you need fixed, or a new feature to add, submit a pull request and we would be happy to look at it.

Projects
========
* `WPILib` - DotNet implementation of the WPILib
* `WPILib.Extras` - Useful functionality to extend the WPILib.
* `HAL` - The Hardware Abstraction Layer, for communicating with either the RoboRIO or the Simulator. Also includes the Simulator code base.
* `NIVision` - Wrapper for NIVision (Not Fully Functional).
* `Sandcastle` - Documentation Generator

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
