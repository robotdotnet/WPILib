namespace WPILib.CodeHelpers.EpilogueGenerator;

public static class Strings
{
    public const string LogOuterNamespace = "WPILib";
    public const string LogInnerNamespace = "Logging";
    public const string LogNamespace = "WPILib.Logging";
    public const string LoggedAttributeTypeName = "LoggedAttribute";
    public const string NotLoggedAttributeTypeName = "NotLoggedAttribute";
    public const string CustomLoggerForAttributeName = "CustomLoggerForAttribute";
    public const string LoggedAttributeNameWithoutGlobal = $"{LogNamespace}.{LoggedAttributeTypeName}";
    public const string CustomLoggerForAttributeNameWithoutGlobal = $"{LogNamespace}.{CustomLoggerForAttributeName}";
    public const string ClassSpecificLoggerFullyQualifiedTypeName = "global::WPILib.Logging.Loggers.ClassSpecificLogger";
    public const string UpdateFunctionStart = "protected override void Update(global::WPILib.Logging.Loggers.IDataLogger dataLogger, ";
    public const string IStructSerializableName = "IStructSerializable";
}
