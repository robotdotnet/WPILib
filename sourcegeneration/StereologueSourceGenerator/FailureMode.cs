namespace Stereologue.SourceGenerator;

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
}
