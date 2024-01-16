namespace WPIMath.Interpolation;

public interface IInterpolatable<T>
{
    T Interpolate(T endValue, double t);
}
