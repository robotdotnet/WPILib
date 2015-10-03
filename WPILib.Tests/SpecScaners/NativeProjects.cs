namespace WPILib.Tests.SpecScaners
{
    public static class NativeProjects
    {
        /*
        public static void clanger()
        {


            //string[] args = new []{}
            //Regex re = new Regex(@"(?<switch>-{1,2}\S*)(?:[=:]?|\s+)(?<value>[^-\s].*?)?(?=\s+[-]|$)");
            //List<KeyValuePair<string, string>> matches = (from match in re.Matches(string.Join(" ", args)).Cast<Match>()
             //                                             select new KeyValuePair<string, string>(match.Groups["switch"].Value, match.Groups["value"].Value))
            //    .ToList();

            //var files = new List<string>();
            var includeDirs = new List<string>()
            {
                "include"
            };
            //string libraryPath = string.Empty;
            //string prefixStrip = string.Empty;
            string methodClassName = "Methods";


            string[] files = Directory.GetFiles("include", "*", SearchOption.AllDirectories);

            var createIndex = clang.createIndex(0, 0);
            string[] arr = { "-x", "c++" };

            arr = arr.Concat(includeDirs.Select(x => "-I" + x)).ToArray();

            List<CXTranslationUnit> translationUnits = new List<CXTranslationUnit>();

            foreach (var file in files)
            {
                CXTranslationUnit translationUnit;
                CXUnsavedFile unsavedFile;
                var translationUnitError = clang.parseTranslationUnit2(createIndex, file, arr, 3, out unsavedFile, 0, 0, out translationUnit);

                if (translationUnitError != CXErrorCode.CXError_Success)
                {
                    Console.WriteLine("Error: " + translationUnitError);
                    var numDiagnostics = clang.getNumDiagnostics(translationUnit);

                    for (uint i = 0; i < numDiagnostics; ++i)
                    {
                        var diagnostic = clang.getDiagnostic(translationUnit, i);
                        Console.WriteLine(clang.getDiagnosticSpelling(diagnostic).ToString());
                        clang.disposeDiagnostic(diagnostic);
                    }
                }

                translationUnits.Add(translationUnit);
            }

            foreach (var tu in translationUnits)
            {
                clang.visitChildren(clang.getTranslationUnitCursor(tu), visit, new CXClientData(IntPtr.Zero));
            }

            foreach (var tu in translationUnits)
            {
                clang.disposeTranslationUnit(tu);
            }
            clang.disposeIndex(createIndex);
        }

        public static CXChildVisitResult visit(CXCursor cursor, CXCursor parent, IntPtr data)
        {
            if (cursor.IsInSystemHeader())
            {
                return CXChildVisitResult.CXChildVisit_Continue;
            }

            CXCursorKind curKind = clang.getCursorKind(cursor);

            if (curKind == CXCursorKind.CXCursor_FunctionDecl)
            {
                var functionName = clang.getCursorSpelling(cursor).ToString();

                if (visitedFunctions.Contains(functionName))
                {
                    return CXChildVisitResult.CXChildVisit_Continue;
                }

                visitedFunctions.Add(functionName);

                funcs.Add(GetFunction(cursor));

                return CXChildVisitResult.CXChildVisit_Continue;
            }
            return CXChildVisitResult.CXChildVisit_Recurse;
        }

        internal static List<Function> funcs = new List<Function>(); 

        private static Function GetFunction(CXCursor cursor)
        {
            var funcType = clang.getCursorType(cursor);
            var funcName = clang.getCursorSpelling(cursor).ToString();
            var resultType = clang.getCursorResultType(cursor);

            string fixedRetType = ReturnTypeHelper(resultType);

            int numArgTypes = clang.getNumArgTypes(funcType);
            List<string> arguments = new List<string>();
            for (uint i = 0; i < numArgTypes; i++)
            {
                arguments.Add(ArgumentHelper(funcType, clang.Cursor_getArgument(cursor, i), i));
            }

            return new Function(fixedRetType, funcName, arguments.ToArray());
        }

        public static string ArgumentHelper(CXType functionType, CXCursor paramCursor, uint index)
        {
            var numArgTypes = clang.getNumArgTypes(functionType);
            var type = clang.getArgType(functionType, index);
            var cursorType = clang.getCursorType(paramCursor);

            string rettype = "";

            switch (type.kind)
            {
                case CXTypeKind.CXType_Pointer:
                    var pointee = clang.getPointeeType(type);
                    switch (pointee.kind)
                    {
                        case CXTypeKind.CXType_Pointer:
                            rettype = (pointee.IsPtrToConstChar() && clang.isConstQualifiedType(pointee) != 0 ? "string[]" : "ref IntPtr");
                            break;
                        case CXTypeKind.CXType_FunctionProto:
                            rettype = (clang.getTypeSpelling(cursorType).ToString());
                            break;
                        case CXTypeKind.CXType_Void:
                            rettype = ("IntPtr");
                            break;
                        case CXTypeKind.CXType_Char_S:
                            rettype = (type.IsPtrToConstChar() ? "string" : "IntPtr"); // if it's not a const, it's best to go with IntPtr
                            break;
                        case CXTypeKind.CXType_WChar:
                            rettype = (type.IsPtrToConstChar() ? "string" : "IntPtr");
                            break;
                        default:
                            rettype = CommonTypeHandling(pointee, "ref ");
                            break;
                    }
                    break;
                default:
                    rettype = CommonTypeHandling(type);
                    break;
            }
            return rettype;
        }

        public static bool IsPtrToConstChar(this CXType type)
        {
            var pointee = clang.getPointeeType(type);

            if (clang.isConstQualifiedType(pointee) != 0)
            {
                switch (pointee.kind)
                {
                    case CXTypeKind.CXType_Char_S:
                        return true;
                }
            }

            return false;
        }

        public static string ReturnTypeHelper(CXType type)
        {
            return CommonTypeHandling(type);
        }

        public static string CommonTypeHandling(CXType type, string refParam = "")
        {
            bool isConstQualifiedType = clang.isConstQualifiedType(type) != 0;
            string spelling;
            return refParam + type.ToPlainTypeString();
            switch (type.kind)
            {
                case CXTypeKind.CXType_Typedef:
                    var cursor = clang.getTypeDeclaration(type);
                    if (clang.Location_isInSystemHeader(clang.getCursorLocation(cursor)) != 0)
                    {
                        spelling = clang.getCanonicalType(type).ToPlainTypeString();
                    }
                    else
                    {
                        spelling = clang.getCursorSpelling(cursor).ToString();
                    }
                    break;
                case CXTypeKind.CXType_Record:
                case CXTypeKind.CXType_Enum:
                    spelling = clang.getTypeSpelling(type).ToString();
                    break;
                case CXTypeKind.CXType_IncompleteArray:
                    
                    spelling = CommonTypeHandling(clang.getArrayElementType(type)) + "[]";
                    break;
                case CXTypeKind.CXType_Unexposed: // Often these are enums and canonical type gets you the enum spelling
                    var canonical = clang.getCanonicalType(type);
                    // unexposed decl which turns into a function proto seems to be an un-typedef'd fn pointer
                    if (canonical.kind == CXTypeKind.CXType_FunctionProto)
                    {
                        spelling = "IntPtr";
                    }
                    else
                    {
                        spelling = clang.getTypeSpelling(canonical).ToString();
                    }
                    break;
                default:
                    spelling = clang.getCanonicalType(type).ToPlainTypeString();
                    break;
            }

            if (isConstQualifiedType)
            {
                spelling = spelling.Replace("const ", string.Empty); // ugh
            }
            return refParam + spelling;
        }

        public static string ToPlainTypeString(this CXType type, string unknownType = "UnknownType")
        {
            var canonical = clang.getCanonicalType(type);
            return type.ToString();
            switch (type.kind)
            {
                
                case CXTypeKind.CXType_Bool:
                    return "bool";
                case CXTypeKind.CXType_UChar:
                case CXTypeKind.CXType_Char_U:
                    return "char";
                case CXTypeKind.CXType_SChar:
                case CXTypeKind.CXType_Char_S:
                    return "sbyte";
                case CXTypeKind.CXType_UShort:
                    return "ushort";
                case CXTypeKind.CXType_Short:
                    return "short";
                case CXTypeKind.CXType_Float:
                    return "float";
                case CXTypeKind.CXType_Double:
                    return "double";
                case CXTypeKind.CXType_Int:
                    return "int";
                case CXTypeKind.CXType_UInt:
                    return "uint";
                case CXTypeKind.CXType_Pointer:
                case CXTypeKind.CXType_NullPtr: // ugh, what else can I do?
                    return "IntPtr";
                case CXTypeKind.CXType_Long:
                    return "int";
                case CXTypeKind.CXType_ULong:
                    return "int";
                case CXTypeKind.CXType_LongLong:
                    return "long";
                case CXTypeKind.CXType_ULongLong:
                    return "ulong";
                case CXTypeKind.CXType_Void:
                    return "void";
                case CXTypeKind.CXType_Unexposed:
                    if (canonical.kind == CXTypeKind.CXType_Unexposed)
                    {
                        return clang.getTypeSpelling(canonical).ToString();
                    }
                    return canonical.ToPlainTypeString();
                default:
                    return unknownType;
            }
        }

        private static readonly HashSet<string> visitedFunctions = new HashSet<string>();

        public static bool IsInSystemHeader(this CXCursor cursor)
        {
            return clang.Location_isInSystemHeader(clang.getCursorLocation(cursor)) != 0;
        }
    }
    */
    }
}
