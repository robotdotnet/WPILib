# Repository Guidance

## Project Context

- This repository is a C# port of WPILib's upstream `allwpilib` project: <https://github.com/wpilibsuite/allwpilib>.
- When behavior, API shape, native entry points, or generated bindings are unclear, compare against upstream before making assumptions.
- Keep changes aligned with the structure and semantics of upstream WPILib unless the C# port has an intentional local difference.

## Native Interop

- Use `LibraryImport` for native imports. Do not introduce new `DllImport` declarations.
- Always check C native imports against the upstream `wpilibsuite/allwpilib` declarations before adding or changing signatures.
- Verify entry point names, parameter types, string marshalling, ownership/freeing rules, and callback signatures against upstream C/C++ headers and existing C# native wrappers.

## Environment Notes

- .NET commands cannot be run successfully inside the sandbox for this repository. Commands such as `dotnet restore`, `dotnet build`, and `dotnet test` will fail unless they are run with escalated permissions.
- When validation requires a .NET command, request approval to run it outside the sandbox instead of treating the sandbox failure as a product or test failure.
