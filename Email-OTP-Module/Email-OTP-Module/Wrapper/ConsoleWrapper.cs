namespace Email_OTP_Module.Wrapper
{
    public static class ConsoleWrapper
    {
        public static Func<string> ReadLine = () => Console.ReadLine();
        public static Action<string> WriteLine = message => Console.WriteLine(message);
        public static Action<string> Write = message => Console.Write(message);
    }
}
