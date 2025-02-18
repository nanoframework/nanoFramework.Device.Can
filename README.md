[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_lib-nanoFramework.Devices.Can&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoframework_lib-nanoFramework.Devices.Can) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_lib-nanoFramework.Devices.Can&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework_lib-nanoFramework.Devices.Can) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/nanoFramework.Device.Can.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Device.Can/) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md) [![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://raw.githubusercontent.com/nanoframework/Home/main/resources/logo/nanoFramework-repo-logo.png)

-----

### Welcome to the .NET **nanoFramework** CAN Class Library repository

These libraries are the successor of the `nanoFramework.Device.Can` class library that required a native component and was available for two STM32-based boards. The direct replacement of that library is `nanoFramework.Device.Can.Stm32`.

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
| nanoFramework.Device.Can.Core | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_apis/build/status/nanoFramework.Device.Can?repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main)](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_build/latest?definitionId=25&repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Device.Can.Core.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Device.Can.Core/)  |
| nanoFramework.Device.Can.Esp32 | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_apis/build/status/nanoFramework.Device.Can?repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main)](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_build/latest?definitionId=25&repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Device.Can.Esp32.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Device.Can.Esp32/)  |
| nanoFramework.Device.Can.Mcp2515 | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_apis/build/status/nanoFramework.Device.Can?repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main)](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_build/latest?definitionId=25&repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Device.Can.Mc2515.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Device.Can.Mcp2515/)  |
| nanoFramework.Device.Can.Stm32 | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_apis/build/status/nanoFramework.Device.Can?repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main)](https://dev.azure.com/nanoframework/nanoFramework.Device.Can/_build/latest?definitionId=25&repoName=nanoframework%2FnanoFramework.Device.Can&branchName=main) | [![NuGet](https://img.shields.io/nuget/v/nanoFramework.Device.Can.Stm32.svg?label=NuGet&style=flat&logo=nuget)](https://www.nuget.org/packages/nanoFramework.Device.Can.Stm32/)  |

## The .NET **nanoFramework** CAN Class Libraries

To exchange CAN messages by a .NET **nanoFramework** application, at least one device dependent class library is required:

- `nanoFramework.Device.Can.Esp32` uses the CAN/TWAI implementation of an ESP32 microcontroller. This requires a matching firmware/target version for the microcontroller that provides the native part of the implementation. In general an external CAN transceiver is also required.
- `nanoFramework.Device.Can.Stm` uses the CAN implementation of a STM32 microcontroller. This requires a matching firmware/target version for the microcontroller that provides the native part of the implementation. In general an external CAN transceiver is also required.
- `nanoFramework.Device.Can.Mcp2515` uses an external CAN device based on the MCP2515 chip. It is a 100% .NET implementation that works with every microcontroller, and can be used in combination with the ESP32 or STM32 library. MCP2515 boards typically include a CAN transceiver.

All device dependent libraries are based on:

- `nanoFramework.Device.Can.Core` contains the common classes and interfaces. As the `nanoFramework.Device.Can` namespace implements the basic ISO 11898 CAN message transfer, this library can be used to implement higher-level protocols (e.g., CAN-TP or ISO-TP as used in ODB II) in device independent libraries.

## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** Class Libraries are licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behaviour in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

### .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).
