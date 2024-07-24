namespace BlazorApp1.Services
{
    public class NotificationService
    {
        public event Action<string, string> OnNotify;

        public void Notify(string message, string alertType = "alert-info")
        {
            OnNotify?.Invoke(message, alertType);
        }
    }
}
