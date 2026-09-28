
using System.Diagnostics.CodeAnalysis;

using ErikForwerk.TestAbstractions.v3.Models;

using Microsoft.Extensions.Logging;

using Xunit;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ErikForwerk.TestAbstractions.v3.Tests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class TestLoggerFactoryTests
{
	[Fact]
	public void CreateLogger()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestLogger expectedLogger	= new();
		TestLoggerFactory sut		= new (expectedLogger);

		//--- ACT -------------------------------------------------------------
		ILogger logger = sut.CreateLogger("Test logger");

		//--- ASSERT ----------------------------------------------------------
		Assert.NotNull(logger);
		Assert.Same(expectedLogger, logger);
	}

	[Fact]
	public void AddProvider_ThrowsException()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestLoggerFactory sut = new (new TestLogger());

		//--- ACT -------------------------------------------------------------
		NotImplementedException ex = Assert.Throws<NotImplementedException>(
			[ExcludeFromCodeCoverage]
			() => sut.AddProvider(null!));

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal("The method or operation is not implemented.", ex.Message);
	}

	[Fact]
	public void Dispose_ThrowsException()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestLoggerFactory sut = new (new TestLogger());

		//--- ACT -------------------------------------------------------------
		NotImplementedException ex = Assert.Throws<NotImplementedException>(
			[ExcludeFromCodeCoverage]
			() => sut.Dispose());

		//--- ASSERT ----------------------------------------------------------
		Assert.Equal("The method or operation is not implemented.", ex.Message);
	}
}
