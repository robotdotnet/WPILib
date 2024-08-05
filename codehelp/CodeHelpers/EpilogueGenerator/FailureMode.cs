namespace WPILib.CodeHelpers.EpilogueGenerator;

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
