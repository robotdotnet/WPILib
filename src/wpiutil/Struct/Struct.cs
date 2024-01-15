using System;

namespace WPIUtil;

public interface Struct<T> {
    public string TypeString {get;}
    public int Size {get;}
    public string Schema {get;}

    public Struct<object>[] Nested => [];

    T Unpack(); // TODO figure out the buffer here
}