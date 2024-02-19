namespace WPILib.CodeHelpers.LogGenerator;

public enum FailureMode
{
    None,
    AttributeUnknownMemberType,
    ProtobufArray,
    UnknownTypeNonArray,
    UnknownTypeArray,
    MethodReturnsVoid,
    MethodHasParameters,
    UnknownTypeToLog,
    NullableStructArray,
    MissingGenerateLog,
}
