namespace TheSalem.Metaprogramming
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Run<RoleAlignmentReplacer>();
        }

        private static void Run<T>()
            where T : CodeEditor, new()
        {
            new T().Run();
        }
    }
}
