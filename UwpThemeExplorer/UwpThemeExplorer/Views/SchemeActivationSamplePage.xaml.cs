﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UwpThemeExplorer.Helpers;
using UwpThemeExplorer.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpThemeExplorer.Views
{
    // TODO WTS: Remove this sample page when/if it's not needed.
    // This page is an sample of how to launch a specific page in response to a protocol launch and pass it a value.
    // It is expected that you will delete this page once you have changed the handling of a protocol launch to meet
    // your needs and redirected to another of your pages.
    public sealed partial class SchemeActivationSamplePage : Page, IPivotActivationPage
    {
        public SchemeActivationSampleViewModel ViewModel { get; } = new SchemeActivationSampleViewModel();

        public SchemeActivationSamplePage()
        {
            InitializeComponent();
        }

        public async Task OnPivotActivatedAsync(Dictionary<string, string> parameters)
        {
            ViewModel.Initialize(parameters);
            await Task.CompletedTask;
        }
    }
}
