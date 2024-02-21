namespace CsCore;

[Flags]
public enum SourceKind
{
    Unknown = 0,
    Usb = 1,
    Http = 2,
    Cv = 4,
    Raw = 8
}
