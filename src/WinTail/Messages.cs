namespace WinTail
{
    class Messages
    {
        #region Neutral/System messages

        public class ContinueProcessing { }

        #endregion

        #region Success messages

        public class InputSuccess
        {
            public InputSuccess(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; }
        }

        #endregion

        #region Error messages

        public class InputError
        {
            public InputError(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; }
        }

        public class NullInputError : InputError
        {
            public NullInputError(string reason) : base(reason) { }
        }

        public class ValidationError : InputError
        {
            public ValidationError(string reason) : base(reason) { }
        }

        #endregion
    }
}