
using Xunit;

//-----------------------------------------------------------------------------------------------------------------------------------------
namespace ErikForwerk.TestAbstractions.v3.Tests;

//-----------------------------------------------------------------------------------------------------------------------------------------
public sealed class TestOutputCollectorTests
{
	//-----------------------------------------------------------------------------------------------------------------
	#region WriteLine

	[Fact]
	public void WriteLine_WithMessage_AddsMessageToOutput()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestOutputCollector sut = new ();

		//--- ACT -------------------------------------------------------------
		sut.WriteLine("Hello, World!");

		//--- ASSERT ----------------------------------------------------------
		string singleContent = Assert.Single(sut.Output);
		Assert.Equal("Hello, World!", singleContent);
	}

	[Fact]
	public void WriteLine_WithMultipleMessages_AddsAllMessagesToOutput()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestOutputCollector sut = new ();

		//--- ACT -------------------------------------------------------------
		sut.WriteLine("First");
		sut.WriteLine("Second");
		sut.WriteLine("Third");

		//--- ASSERT ----------------------------------------------------------
		Assert.Collection(sut.Output,
			item => Assert.Equal("First", item),
			item => Assert.Equal("Second", item),
			item => Assert.Equal("Third", item));
	}

	[Fact]
	public void WriteLine_WithFormatString_AddsFormattedMessageToOutput()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestOutputCollector sut = new ();

		//--- ACT -------------------------------------------------------------
		sut.WriteLine("Hello, {0}!", "World");

		//--- ASSERT ----------------------------------------------------------
		string singleContent = Assert.Single(sut.Output);
		Assert.Equal("Hello, World!", singleContent);
	}

	#endregion WriteLine

	//-----------------------------------------------------------------------------------------------------------------
	#region Write

	[Fact]
	public void Write_StaticText_AddsMessageToOutput()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestOutputCollector sut = new ();

		//--- ACT -------------------------------------------------------------
		sut.Write("Hello, World!");

		//--- ASSERT ----------------------------------------------------------
		string singleContent = Assert.Single(sut.Output);
		Assert.Equal("Hello, World!", singleContent);
	}

	[Fact]
	public void Write_WithFormatString_AddsFormattedMessageToOutput()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestOutputCollector sut = new ();

		//--- ACT -------------------------------------------------------------
		sut.Write("Hello, {0}!", "World");

		//--- ASSERT ----------------------------------------------------------
		string singleContent = Assert.Single(sut.Output);
		Assert.Equal("Hello, World!", singleContent);
	}

	#endregion Write

	//-----------------------------------------------------------------------------------------------------------------
	#region ITestOutputHelper.Output

	[Fact]
	public void Output_WhenNoMessagesWritten_ReturnsEmptyCollection()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestOutputCollector sut	= new ();

		//--- ACT -------------------------------------------------------------
		List<string> output		= sut.Output;

		//--- ASSERT ----------------------------------------------------------
		Assert.Empty(output);
	}

	[Fact]
	public void Output_InterfaceProperty_ReturnsAllMessagesJoinedByNewline()
	{
		//--- ARRANGE ---------------------------------------------------------
		TestOutputCollector sut = new ();

		//--- ACT -------------------------------------------------------------
		sut.WriteLine("First");
		sut.WriteLine("Second");
		sut.WriteLine("Third");
		string output = ((ITestOutputHelper)sut).Output;

		//--- ASSERT ----------------------------------------------------------
		string expectedOutput = $"First{Environment.NewLine}Second{Environment.NewLine}Third";
		Assert.Equal(expectedOutput, output);
	}

	#endregion ITestOutputHelper.Output
}
