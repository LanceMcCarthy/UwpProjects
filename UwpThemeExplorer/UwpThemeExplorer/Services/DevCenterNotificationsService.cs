using System;
using System.Threading.Tasks;

using Microsoft.Services.Store.Engagement;

using UwpThemeExplorer.Activation;

using Windows.ApplicationModel.Activation;

namespace UwpThemeExplorer.Services
{
    internal class DevCenterNotificationsService : ActivationHandler<ToastNotificationActivatedEventArgs>
    {
        public async Task InitializeAsync()
        {
            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            await engagementManager.RegisterNotificationChannelAsync();
        }

        protected override async Task HandleInternalAsync(ToastNotificationActivatedEventArgs args)
        {
            var toastActivationArgs = args as ToastNotificationActivatedEventArgs;

            StoreServicesEngagementManager engagementManager = StoreServicesEngagementManager.GetDefault();
            string originalArgs = engagementManager.ParseArgumentsAndTrackAppLaunch(toastActivationArgs.Argument);

            //// Use the originalArgs variable to access the original arguments passed to the app.

            await Task.CompletedTask;
        }
    }
}
