[![Build Status](https://www.myget.org/BuildSource/Badge/robotdotnet-dev?identifier=75049e1c-38b1-4f09-882d-b061cad6461f)](https://www.myget.org)

# robotdotnet-wpilib
DotNet implementation of WPILib for FIRST Robotics Competition (FRC)

Installation Instructions:

1. Create directories C:\Builds\WPILib and C:\Builds\WPILib\HAL on your development machine.
2. Open robotdotnet-wpilib solution.
3. Build solution.
4. Navigate to C:\Builds\WPILib\HAL
5. Follow the instructions to install mono that are in the Installation folder. You can leave the SSH window open.
6. Run "mkdir /home/lvuser/HAL" - This will be used later, unless we find a better place to put the HAL. but for testing it is easier to have the HAL in the folder we put the executable in.
7. Run "mkdir /home/lvuser/mono"
8. FTP HAL-RoboRIO.dll and libHALAthena_shared.so to /home/lvuser/mono on the RoboRIO.
9. Create a new Visual Studio project. Make it be a Console Application using the .NET Framework 4.
10. Rename project.cs to be whatever you want your main robot class to be called. Allow in to rename the class that is in the file.
11. Add a reference to WPILib that is located in C:\Builds\WPILib. You do not have to add a reference to HAL-Base.
12. Add "using WPILib;" to your main class
13. Make the class inherit from either SampleRobot or IterativeRobot.
14. Add the following line inside of the static main function
  RobotBase.main(System.Reflection.Assembly.GetExecutingAssembly());
15. Press F6 to build the program.
16. Right click on the Project, and Open Folder in file explorer.
17. Navigate to bin\debug.
18. FTP HAL-Base.dll, WPILib.dll and YourRobot.exe to /home/lvuser/mono on the RoboRIO. Replace YourRobot with the name of your executable.
19. Using the SSH window, run "mono /home/lvuser/mono/YourRobot.exe" replacing YourRobot with the name of your executable.
20. Your code should now be running. 
21. From here, you can create code similar to the way Java does. I will write better documentation on this later. But if you are helping this early you should be able to figure it out.


Note that you should also be able to use the libary from VB as well. 
