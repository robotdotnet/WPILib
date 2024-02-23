using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace WPILib.CodeHelpers;

public record DirectTypeConstraint(string Name, bool IsNullableAnnotated);

[Flags]
public enum TypeParameterConstraintFlags
{
    None = 0x0,
    Class = 0x1,
    Struct = 0x2,
    New = 0x4,
    Unmanaged = 0x8,
    NullableAnnotated = 0x10,
    NotNull = 0x20,
}

public record TypeParameterModel(string Name, VarianceKind Kind, TypeParameterConstraintFlags ConstraintFlags, EquatableArray<DirectTypeConstraint> TypeConstraints)
{
    public void WriteTypeParameter(IndentedStringBuilder builder)
    {
        if (Kind == VarianceKind.In)
        {
            builder.Append("in ");
        }
        else if (Kind == VarianceKind.Out)
        {
            builder.Append("out ");
        }
        builder.Append(Name);
    }

    public void WriteConstraint(IndentedStringBuilder builder)
    {
        if (ConstraintFlags == TypeParameterConstraintFlags.None && TypeConstraints.IsDefaultOrEmpty)
        {
            return;
        }
        builder.Append($"where {Name} : ");
        string? firstConstraint = null;
        if ((ConstraintFlags & TypeParameterConstraintFlags.Struct) != 0)
        {
            firstConstraint = "struct";
        }
        else if ((ConstraintFlags & TypeParameterConstraintFlags.Class) != 0)
        {
            firstConstraint = "class";
        }
        else if ((ConstraintFlags & (TypeParameterConstraintFlags.Class | TypeParameterConstraintFlags.NullableAnnotated)) != 0)
        {
            firstConstraint = "class?";
        }
        else if ((ConstraintFlags & TypeParameterConstraintFlags.Unmanaged) != 0)
        {
            firstConstraint = "unmanaged";
        }
        else if ((ConstraintFlags & TypeParameterConstraintFlags.NotNull) != 0)
        {
            firstConstraint = "notnull";
        }

        bool first = true;

        if (firstConstraint is not null)
        {
            first = false;
            builder.Append(firstConstraint);
        }

        if ((ConstraintFlags & TypeParameterConstraintFlags.New) != 0)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append(", ");
            }
            builder.Append("new()");
        }

        foreach (var classConstraints in TypeConstraints.AsSpan())
        {
            if (first)
            {
                first = false;
            }
            else
            {
                builder.Append(", ");
            }
            builder.Append(classConstraints.Name);
            if (classConstraints.IsNullableAnnotated)
            {
                builder.Append("?");
            }
        }
    }
}

public static class TypeParameterExtensions
{
    private static string GetFullyQualifiedTypeName(this ITypeSymbol symbol)
    {
        // TODO stop using fully qualified
        return symbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    public static TypeParameterModel GetTypeParameterModel(this ITypeParameterSymbol symbol, bool grabConstraints)
    {
        TypeParameterConstraintFlags flags = TypeParameterConstraintFlags.None;
        EquatableArray<DirectTypeConstraint> constraints;
        if (grabConstraints)
        {
            if (symbol.HasReferenceTypeConstraint)
            {
                flags |= TypeParameterConstraintFlags.Class;
            }
            if (symbol.HasValueTypeConstraint)
            {
                flags |= TypeParameterConstraintFlags.Struct;
            }
            if (symbol.ReferenceTypeConstraintNullableAnnotation == NullableAnnotation.Annotated)
            {
                flags |= TypeParameterConstraintFlags.NullableAnnotated;
            }
            if (symbol.HasUnmanagedTypeConstraint)
            {
                flags |= TypeParameterConstraintFlags.Unmanaged;
            }
            if (symbol.HasNotNullConstraint)
            {
                flags |= TypeParameterConstraintFlags.NotNull;
            }
            if (symbol.HasConstructorConstraint)
            {
                flags |= TypeParameterConstraintFlags.New;
            }
            var typeConstraints = symbol.ConstraintTypes;

            if (typeConstraints.IsDefaultOrEmpty)
            {
                constraints = [];
            }
            else
            {
                var builder = ImmutableArray.CreateBuilder<DirectTypeConstraint>(typeConstraints.Length);
                var nullableConstraints = symbol.ConstraintNullableAnnotations;

                for (int i = 0; i < typeConstraints.Length; i++)
                {
                    var constraintSymbol = typeConstraints[i];
                    var name = constraintSymbol.GetFullyQualifiedTypeName();
                    builder.Add(new DirectTypeConstraint(name, nullableConstraints[i] == NullableAnnotation.Annotated));
                }

                constraints = builder.ToImmutable();
            }

        }
        else
        {
            constraints = [];
        }
        return new(symbol.Name, symbol.Variance, flags, constraints);
    }
}
