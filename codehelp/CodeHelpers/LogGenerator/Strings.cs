namespace WPILib.CodeHelpers.LogGenerator;

public static class Strings
{
    public const string LogNamespace = "Stereologue";
    public const string LogAttributeTypeName = "LogAttribute";
    public const string GenerateLogAttributeTypeName = "GenerateLogAttribute";
    public const string LoggedInterfaceTypeName = "ILogged";
    public const string LoggerTypeName = "Stereologuer";
    public const string GenerateLogAttributeNameWithoutGlobal = $"{LogNamespace}.{GenerateLogAttributeTypeName}";
    public const string LoggerInterfaceFullyQualified = "global::Stereologue.ILogged";
    public const string UpdateStereologueName = "UpdateStereologue";
    public const string FullMethodDeclaration = $"public void {UpdateStereologueName}(string path, global::{LogNamespace}.{LoggerTypeName} logger)";
    public const string FullMethodDeclarationVb = $"Sub {UpdateStereologueName}(path as String, logger as Global.{LogNamespace}.{LoggerTypeName})";
    public const string IStructSerializableName = "IStructSerializable";

    // WPIUtil.Serialization.Struct
    public const string IProtobufSerializableString = "IProtobufSerializable";
}
