# Test Abstractions

Basic test-class implementations and abstractions that simplify the most common test cases for xUnit testing.

## Current Status

[![codecov](https://codecov.io/gh/3rikF/test-abstractions-v3/graph/badge.svg?token=NMGTTG3KR4)](https://codecov.io/gh/3rikF/test-abstractions-v3)
[![wakatime](https://wakatime.com/badge/user/ccce5eac-49f0-481f-998c-1183a3cd0b18/project/b5bbf7e9-f1d1-49e4-9451-ba72bac4627b.svg)](https://wakatime.com/badge/user/ccce5eac-49f0-481f-998c-1183a3cd0b18/project/b5bbf7e9-f1d1-49e4-9451-ba72bac4627b)

[![Current Repository Status](https://github.com/3rikF/test-abstractions-v3/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/3rikF/test-abstractions-v3/actions)
[![Current Repository Status](https://github.com/3rikF/test-abstractions-v3/actions/workflows/build-and-publish.yml/badge.svg)](https://github.com/3rikF/test-abstractions-v3/actions)

[![NuGet](https://img.shields.io/nuget/v/ErikForwerk.TestAbstractions.v3?label=NuGet%20TestAbstractions.v3)](https://www.nuget.org/packages/ErikForwerk.TestAbstractions.v3/)
[![NuGet STA](https://img.shields.io/nuget/v/ErikForwerk.TestAbstractions.STA.v3?label=NuGet%20TestAbstractions.STA.v3)](https://www.nuget.org/packages/ErikForwerk.TestAbstractions.STA.v3/)


- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
  - [TestBase Class](#testbase-class)
  - [Test Loggers](#test-loggers)
  - [Property Comparison](#property-comparison)
  - [Auto Properties](#auto-properties)
  - [File Cleanup](#file-cleanup)
  - [STA Test Support](#sta-test-support)
- [Requirements](#requirements)
- [License](#license)
- [Contributing](#contributing)

## Features

This library provides a collection of utilities and abstractions to simplify unit testing with xUnit:

- **TestBase Class**: Base class with common test utilities and helper methods
- **Test Loggers**: Implementation of `ILogger` and `ILogger<T>` for testing logging scenarios
- **Property Comparison**: Deep property comparison utilities using reflection
- **Auto Properties**: Automatic property generation with random values for testing
- **File Cleanup**: Automatic test file cleanup on test completion
- **String Extensions**: Test-specific string manipulation utilities
- **STA Test Support**: Support for Single-Threaded Apartment mode tests (Windows only)
- **Fail Test Helpers**: Convenient methods to fail tests with custom messages

## Installation

### NuGet Package

For standard testing:
```bash
dotnet add package ErikForwerk.TestAbstractions.v3
```

For Windows STA (Single-Threaded Apartment) testing:
```bash
dotnet add package ErikForwerk.TestAbstractions.STA.v3
```

Or via NuGet Package Manager:
```
Install-Package ErikForwerk.TestAbstractions.v3
Install-Package ErikForwerk.TestAbstractions.STA.v3
```

## Usage

### TestBase Class

Inherit from `TestBase` to access common test utilities:

```csharp
using ErikForwerk.TestAbstractions.v3.Models;
using Xunit;

public class MyTests : TestBase
{
    public MyTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void TestExample()
    {
        // Use TestConsole for output
        TestConsole.WriteLine("Test output");

        // Use B() method for bracketed string representation
        var result = B("value"); // Returns "[value]"
        var nullValue = B(null); // Returns "<null>"
    }
}
```

### Test Loggers

Use `TestLogger` to capture and verify log messages:

```csharp
using ErikForwerk.TestAbstractions.v3.Models;
using Microsoft.Extensions.Logging;
using Xunit;

public class MyServiceTests : TestBase
{
    public MyServiceTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void TestLogging()
    {
        var logger = GetTestLogger();
        var service = new MyService(logger);

        service.DoSomething();

        // Verify logged messages
        Assert.Contains("Expected message", logger.LogMessages);
    }
}
```

### Property Comparison

Use `CompareHelper` to deeply compare object properties:

```csharp
using ErikForwerk.TestAbstractions.v3.Tools;
using Xunit;

[Fact]
public void TestObjectComparison()
{
    var expected = new MyClass { Property1 = "value1", Property2 = 42 };
    var actual = new MyClass { Property1 = "value1", Property2 = 42 };

    // Assert all properties are equal
    CompareHelper.AssertEqual(expected, actual, TestConsole);

    // Or assert all properties are different
    var different = new MyClass { Property1 = "other", Property2 = 99 };
    CompareHelper.AssertCompletelyUnequal(expected, different, TestConsole);
}
```

### Auto Properties

Generate random property values for testing:

```csharp
using ErikForwerk.TestAbstractions.v3.Tools;
using Xunit;

[Fact]
public void TestWithRandomData()
{
    var autoProps = new AutoProperties();

    // Generate random GUID
    var guid = autoProps.GenerateGuid();

    // Generate random string
    var randomString = autoProps.GenerateString();

    // Get random enum value
    var randomStatus = autoProps.GetRandomEnum<MyStatus>();
}
```

### File Cleanup

Automatically clean up test files after test execution:

```csharp
using ErikForwerk.TestAbstractions.v3.Models;
using Xunit;

[Fact]
public void TestWithFileCleanup()
{
    var testFile = Path.GetTempFileName();

    using (CreateTestFileCleanUp(testFile))
    {
        // Work with the file
        File.WriteAllText(testFile, "test data");

        // File will be automatically deleted when disposed
    }
}
```

### STA Test Support

For Windows applications requiring Single-Threaded Apartment mode:

```csharp
using ErikForwerk.TestAbstractions.STA.v3.Models;
using Xunit;

public class MyStaTests : StaTestBase
{
    public MyStaTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void TestInStaMode()
    {
        // Test code that requires STA thread
    }
}
```

## Requirements

- .NET 10.0 or higher
- xUnit.v3 3.2.2 or higher
- Microsoft.Extensions.Logging.Abstractions 10.0.10 or higher

For STA package:
- Windows operating system
- .NET 10.0-windows or higher

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.
