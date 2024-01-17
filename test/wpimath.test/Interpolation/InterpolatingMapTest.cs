using WPIMath.Interpolation;
using Xunit;

namespace WPIMath.Test.Interpolation;

public class InterpolatingMapTest
{
    public const double Epsilon = 1e-9;

    [Fact]
    public void TestMapDouble()
    {
        InterpolatingMap<double> table = new();

        table.Add(125.0, 450.0);
        table.Add(200.0, 510.0);
        table.Add(268.0, 525.0);
        table.Add(312.0, 550.0);
        table.Add(326.0, 650.0);


        Assert.Equal(450.0, table[100.0], Epsilon);

        // Minimum key gives exact value
        Assert.Equal(450.0, table[125.0], Epsilon);

        // Key gives interpolated value
        Assert.Equal(480.0, table[162.5], Epsilon);

        // Key at right of interpolation range gives exact value
        Assert.Equal(510.0, table[200.0], Epsilon);

        // Maximum key gives exact value
        Assert.Equal(650.0, table[326.0], Epsilon);

        // Key above maximum gives largest value
        Assert.Equal(650.0, table[400.0], Epsilon);
    }

    [Fact]
    public void TestMapClear()
    {
        InterpolatingMap<double> table = new();

        table.Add(125.0, 450.0);
        table.Add(200.0, 510.0);
        table.Add(268.0, 525.0);
        table.Add(312.0, 550.0);
        table.Add(326.0, 650.0);


        Assert.Equal(450.0, table[100.0], Epsilon);

        // Minimum key gives exact value
        Assert.Equal(450.0, table[125.0], Epsilon);

        // Key gives interpolated value
        Assert.Equal(480.0, table[162.5], Epsilon);

        // Key at right of interpolation range gives exact value
        Assert.Equal(510.0, table[200.0], Epsilon);

        // Maximum key gives exact value
        Assert.Equal(650.0, table[326.0], Epsilon);

        // Key above maximum gives largest value
        Assert.Equal(650.0, table[400.0], Epsilon);

        table.Clear();

        table.Add(100.0, 250.0);
        table.Add(200.0, 500.0);

        Assert.Equal(375.0, table[150.0], Epsilon);
    }
}
