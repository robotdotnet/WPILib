namespace WPIUtil;

/// <summary>
/// Marker interface to indicate a class is serializable using WPI serialization methods.
/// </summary>
public interface IWPISerializable<T> where T : IWPISerializable<T> { }
