namespace WPILib.CodeHelpers.EpilogueGenerator;

public static class Strings
{
    public const string LogNamespace = "Epilogue";
    public const string LoggedAttributeTypeName = "LoggedAttribute";
    public const string NotLoggedAttributeTypeName = "NotLoggedAttribute";
    public const string CustomLoggerForAttributeName = "CustomLoggerForAttribute";
    public const string LoggedAttributeNameWithoutGlobal = $"{LogNamespace}.{LoggedAttributeTypeName}";
    public const string CustomLoggerForAttributeNameWithoutGlobal = $"{LogNamespace}.{CustomLoggerForAttributeName}";
    public const string ClassSpecificLoggerFullyQualifiedTypeName = "global::Epilogue.Logging.ClassSpecificLogger";
    public const string UpdateFunctionStart = "protected override void Update(global::Epilogue.Logging.IDataLogger dataLogger, ";
    public const string IStructSerializableName = "IStructSerializable";
}
