# Nuspec / NuGet packages

The `nanoFramework.Device.Can` namespace implements the classical CAN (2.0, ISO 11898) protocol. As the CAN protocol only covers the physical and data link layers of the OSI model, additional protocols (e.g., CAN-TP) are required to cover the other layers. The code for those protocols can be based on the device independent class library `nanoFramework.Device.Can.Core` and debugged/tested on any device, including the Virtual nanoDevice. For an application one of the device dependent class libraries like `nanoFramework.Device.Can.Stm32` should be added to connect to a device that is connected to the CAN bus.

Hence the NuGet package for a device dependent class library should include only the assemblies for that library and not the `nanoFramework.Device.Can.Core` files. It should have the `nanoFramework.Device.Can.Core` NuGet package as dependency, with the same version as the device dependent NuGet package (`$version$`). The project for a device dependent class library does not reference the `nanoFramework.Device.Can.Core` NuGet package but references the `nanoFramework.Device.Can.Core.nfproj` instead. 

# Stub generation

If a device dependent class library requires part of its implementation to be a native component, it should be configured to generate stubs for the native code. It is very likely that the native code also requires information about classes and enumerations that are part of the `nanoFramework.Device.Can.Core` class library. As that is a pure .NET library, there is (at the time of writing) no way to generate header files from `nanoFramework.Device.Can.Core`.

As a workaround, the `nanoFramework.Device.Can.sln` solution has an extra configuration **GenerateStubs** in addition to *Debug* and *Release*. Select that configuration to generate the stubs. The stubs are placed in, e.g., the `nanoFramework.Device.Can.Esp32\bin\GenerateStubs\Stubs` directory. The stubs in other directories, e.g., `nanoFramework.Device.Can.Esp32\bin\Debug\Stubs`, are not complete.

The *nfproj* project for the device dependent class library has to be prepared for that (see *nanoFramework.Device.Can.Esp32.nfproj* as example):

- Remove the direct reference to `nanoFramework.Device.Can.Core` from the class library's project.
- Create a copy of `nanoFramework.Device.Can.Esp32.native.props` as, e.g., `nanoFramework.Device.Can.MyDevice.native.props` in the class library's project directory.
- Modify the *nfproj* file and add `<Import Project="nanoFramework.Device.Can.MyDevice.native.props" />` just before the first `<PropertyGroup>`
- Change the `NF_GenerateSkeletonProjectName` and `Name` in `nanoFramework.Device.Can.MyDevice.native.props`.
- Ensure the relevant code files from `nanoFramework.Device.Can.Core` are listed in `nanoFramework.Device.Can.MyDevice.native.props`.
- Open the `nanoFramework.Device.Can.sln` solution file in Visual Studio, Open the `Configuration Manager` for the solution, select `GenerateStubs` and select for the class library configuration `GenerateStubs` and check that the project is built for this configuration.
