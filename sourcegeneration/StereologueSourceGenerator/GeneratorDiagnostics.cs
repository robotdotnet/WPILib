namespace Stereologue.SourceGenerator;

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#pragma warning disable RS2008 // Enable analyzer release tracking

using System;
using Microsoft.CodeAnalysis;

public static class GeneratorDiagnostics
{
    public class Ids
    {
        public const string Prefix = "WPILIB";
        public const string GeneratedTypeNotPartial = Prefix + "1000";
        public const string GeneratedTypeImplementsILogged = Prefix + "1001";
        public const string LoggableTypeNotSupported = Prefix + "1002";
        public const string GeneratedTypeIsInterface = Prefix + "1003";
        public const string GeneratedTypeIsRefStruct = Prefix + "1004";
        public const string LoggedMethodDoesntReturnVoid = Prefix + "1005";
        public const string LoggedMethodTakesArguments = Prefix + "1006";
        public const string LoggedMemberTypeNotSupported = Prefix + "1007";
    }

    private const string Category = "StereologueSourceGenerator";

    public static readonly DiagnosticDescriptor GeneratedTypeNotPartial = new(
        Ids.GeneratedTypeNotPartial, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");

    public static readonly DiagnosticDescriptor GeneratedTypeImplementsILogged = new(
        Ids.GeneratedTypeImplementsILogged, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");

    public static readonly DiagnosticDescriptor LoggableTypeNotSupported = new(
        Ids.LoggableTypeNotSupported, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");

    public static readonly DiagnosticDescriptor GeneratedTypeIsInterface = new(
        Ids.LoggableTypeNotSupported, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");

    public static readonly DiagnosticDescriptor GeneratedTypeIsRefStruct = new(
        Ids.GeneratedTypeIsRefStruct, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");

    public static readonly DiagnosticDescriptor LoggedMethodDoesntReturnVoid = new(
        Ids.LoggedMethodDoesntReturnVoid, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");

    public static readonly DiagnosticDescriptor LoggedMethodTakesArguments = new(
        Ids.LoggedMethodTakesArguments, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
    public static readonly DiagnosticDescriptor LoggedMemberTypeNotSupported = new(
        Ids.LoggedMemberTypeNotSupported, "", "", Category, DiagnosticSeverity.Error, isEnabledByDefault: true, "");
}