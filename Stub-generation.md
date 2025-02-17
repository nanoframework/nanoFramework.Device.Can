# Stub generation

The libraries that require a native components generate a stub for the native part on each build. The stub is placed in, e.g., the `nanoFramework.Device.Can.Stm32\bin\Release\Stubs` directory.

The native component will probably require access to the common classes in `nanoFramework.Device.Can.Core`. As that library is pure .NET, no stub is required. To generate the stub, select the **GenerateStub** configuration (instead of *Debug* or *Release*) in the *nanoFramework.Device.Can.sln* solution and build the solution. The stub will be placed in the `nanoFramework.Device.Can.Core\bin'\GenerateStubs\Stubs` directory.
