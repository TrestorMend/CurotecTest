using FluentValidation.Results;
using Serilog;

namespace Application.Responses
{
    public class ResponseState : IResponseState
    {
        private readonly List<Notification> _notifications;
        private readonly ILogger _logger;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public object? Data { get; private set; }
        public bool Success
        {
            get
            {
                return !HasNotifications;
            }
        }

        public ResponseState(ILogger logger)
        {
            _notifications = new List<Notification>();
            _logger = logger;
        }

        public ResponseState Response(object? data = null)
        {
            Data = data;
            return this;
        }

        public ResponseState ResponseWithNotification(Type tipo, string mensagem, object? data = null, string propertyName = "")
        {
            AddNotification(tipo, mensagem, propertyName);
            Response(data);

            return this;
        }

        public ResponseState ResponseWithNotification(Type tipo, IList<ValidationFailure> validationResults, object? data = null, string propertyName = "")
        {
            AddNotifications(tipo, validationResults);
            Response(data);

            return this;
        }

        public void AddNotification(Type key, string message, string propertyName = "")
        {
            _notifications.Add(new Notification(key.Name, message, propertyName));
        }

        public void AddNotificationWithLogError(string key, string message, string propertyName = "")
        {
            _notifications.Add(new Notification(key, message));
            _logger.Error($"Key: {key} - Error : {message}");
        }

        public void AddNotificationWithLogWarning(string key, string message, string propertyName = "")
        {
            _notifications.Add(new Notification(key, message));
            _logger.Warning($"Key: {key} - Error : {message}");
        }
        public void AddNotifications(Type key, IList<ValidationFailure> validationResults)
        {
            foreach (var error in validationResults)
            {
                AddNotification(key, error.ErrorMessage, error.PropertyName);
            }
        }
    }
}
