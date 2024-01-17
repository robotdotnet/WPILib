using System.Numerics;

namespace WPIMath;

public static class MathExtras
{

    public static T Lerp<T>(T startValue, T endValue, double t) where T : IAdditionOperators<T, T, T>,
                                                                          ISubtractionOperators<T, T, T>,
                                                                          IMultiplyOperators<T, double, T>
    {
        return startValue + (endValue - startValue) * t;
    }
}
