using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TheSalem.Metaprogramming
{
    public class RoleAlignmentReplacer : CodeEditor
    {
        public override void Run()
        {
            var directory = AppDomain.CurrentDomain.BaseDirectory;

            string[] factions = Enum.GetNames(typeof(Faction));

            var baseDirectory = $@"{CodeBaseDirectory}\{nameof(TheSalem)}";

            var files = Directory.GetFiles(baseDirectory);
            files = files.Where(name => factions.Any(faction => name[(name.LastIndexOf('\\') + 1)..].StartsWith(faction))).ToArray();

            var regex = new Regex(@"class (?<alignment>[\w]*) : Role { }", RegexOptions.ECMAScript);

            foreach (var f in files)
            {
                var code = File.ReadAllText(f);
                var captures = regex.MatchNamedCaptures(code);

                if (!captures.Any())
                    continue;

                var alignment = captures["alignment"];

                code = code.Replace(" { }",
$@"
    {{
        public sealed override RoleAlignment FullAlignment => RoleAlignment.{alignment};
    }}");

                File.WriteAllText(f, code);
            }
        }
    }
}
