This is our patch to get Notifier working in the HAL.

To apply, place cppSettings.gradle in the root of the build, and place ThreadShim.cpp in hal\lib\Athena

Then run gradlew hALAthenaSharedLibrary, and the new libHALAthena.so should be built.