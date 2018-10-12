﻿using System;
using System.Threading.Tasks;

using UwpThemeExplorer.Services;
using UwpThemeExplorer.Views;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpThemeExplorer.Activation
{
    internal class SchemeActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
    {
        // By default, this handler expects URIs of the format 'wtsapp:sample?paramName1=paramValue1&paramName2=paramValue2'
        protected override async Task HandleInternalAsync(ProtocolActivatedEventArgs args)
        {
            // Create data from activation Uri in ProtocolActivatedEventArgs
            var data = new SchemeActivationData(args.Uri);
            if (data.IsValid)
            {
                var frame = Window.Current.Content as Frame;
                if (frame.Content is PivotPage pivotPage)
                {
                    await pivotPage.InitializeFromSchemeActivationAsync(data);
                }
                else
                {
                    NavigationService.Navigate(typeof(PivotPage), data);
                }
            }
            else if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                // If the app isn't running and not navigating to a specific page based on the URI, navigate to the home page
                NavigationService.Navigate(typeof(Views.PivotPage));
            }

            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(ProtocolActivatedEventArgs args)
        {
            // If your app has multiple handlers of ProtocolActivationEventArgs
            // use this method to determine which to use. (possibly checking args.Uri.Scheme)
            return true;
        }
    }
}
