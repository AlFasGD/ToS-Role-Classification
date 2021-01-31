using Garyon.Extensions;
using System;

namespace TheSalem.Metaprogramming
{
    public abstract class CodeEditor
    {
        public static string CodeBaseDirectory { get; private set; }

        static CodeEditor()
        {
            CodeBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            // What the fuck is this function
            CodeBaseDirectory = $@"{CodeBaseDirectory[0]}{CodeBaseDirectory.Substring(CodeBaseDirectory[0..1], $@"\{nameof(TheSalem)}.{nameof(Metaprogramming)}\")}\";
        }

        public abstract void Run();
    }
}