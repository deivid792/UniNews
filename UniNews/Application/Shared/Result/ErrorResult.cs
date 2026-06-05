using Uninews.Domain.Shared;

namespace Uninews.Application.Shared.Result;

    public class Error
    {
        public string Message { get; }

        public Error(string message)
        {
            Message = message;
        }

        public static implicit operator Error(string message)
            => new Error(message);

        public static implicit operator Error(Notifications item)
            => new Error(item.Message);
    }

