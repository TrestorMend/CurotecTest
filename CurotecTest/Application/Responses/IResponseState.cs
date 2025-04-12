using FluentValidation.Results;

namespace Application.Responses
{
    public interface IResponseState
    {
        object? Data { get; }
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }
        bool Success { get; }

        void AddNotification(Type key, string message, string propertyName = "");
        void AddNotifications(Type key, IList<ValidationFailure> validationResults);
        void AddNotificationWithLogError(string key, string message, string propertyName = "");
        void AddNotificationWithLogWarning(string key, string message, string propertyName = "");
        ResponseState Response(object? data = null);
        ResponseState ResponseWithNotification(Type tipo, string mensagem, object? data = null, string propertyName = "");
        ResponseState ResponseWithNotification(Type tipo, IList<ValidationFailure> validationResults, object? data = null, string propertyName = "");
    }
}
