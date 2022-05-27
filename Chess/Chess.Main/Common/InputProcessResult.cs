namespace Chess.Main.Common
{
    public sealed class InputProcessResult
    {
        public string Message { get; set; }
        public bool Result { get; set; }

        public InputProcessResult(bool result, string? message)
        {
            Result = result;
            Message = message ?? string.Empty;
        }

        public static InputProcessResult Create(bool result, string? message = null)
            => new InputProcessResult(result, message);
        public static InputProcessResult Fail(string? message = null)
            => new InputProcessResult(false, message);
        public static InputProcessResult Succese(string? message = null)
            => new InputProcessResult(true, message);
    }
}
