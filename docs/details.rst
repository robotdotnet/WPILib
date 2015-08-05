RobotDotNet Details
===================

This page contains some internal design and implementation details for the project. This will be of interested to teams that want to modify and extend the WPILib.

Project Goals
-------------

The .NET implementation of the WPILib was derived from the C++ implementation of the official FIRST WPILib. One of the goals is to keep the implementation similar to the FIRST WPILib. However, there are certain areas where we have swayed from this guideline. For instance, many of the getters and setters found in the WPILib have been replaced with properties. In addition, many of the single function interfaces that are used in the original WPILib to pass functions to other classes have been replaced with delegates, in order to create cleaner code.