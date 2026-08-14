namespace WebhookBin.App.Shared.Bins.Notifications;

public static class ConfigureBinNotifications
{
    extension(WebApplication app)
    {
        public WebApplication MapBinsHub()
        {
            app.MapHub<BinsHub>(BinsHub.HubRoute);
            
            return app;
        }
    }
}