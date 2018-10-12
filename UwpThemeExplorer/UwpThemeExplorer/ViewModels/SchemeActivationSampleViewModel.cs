﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using UwpThemeExplorer.Helpers;

namespace UwpThemeExplorer.ViewModels
{
    // TODO WTS: Remove this sample page when/if it's not needed.
    // This page is an sample of how to launch a specific page in response to a protocol launch and pass it a value.
    // It is expected that you will delete this page once you have changed the handling of a protocol launch to meet
    // your needs and redirected to another of your pages.
    public class SchemeActivationSampleViewModel : Observable
    {
        public ObservableCollection<string> Parameters { get; } = new ObservableCollection<string>();

        public SchemeActivationSampleViewModel()
        {
        }

        public void Initialize(Dictionary<string, string> parameters)
        {
            Parameters.Clear();
            foreach (var param in parameters)
            {
                if (param.Key == "ticks" && long.TryParse(param.Value, out long ticks))
                {
                    var dateTime = new DateTime(ticks);
                    Parameters.Add($"{param.Key}: {dateTime.ToString()}");
                }
                else
                {
                    Parameters.Add($"{param.Key}: {param.Value}");
                }
            }
        }
    }
}
