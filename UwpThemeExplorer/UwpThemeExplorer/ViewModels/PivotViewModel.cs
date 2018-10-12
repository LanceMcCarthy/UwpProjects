using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using CommonHelpers.Common;
using UwpThemeExplorer.Models;

namespace UwpThemeExplorer.ViewModels
{
    public class PivotViewModel : ViewModelBase
    {
        public PivotViewModel()
        {

        }

        public ObservableCollection<ThemeBrushItem> DefaultResources { get; set; } = new ObservableCollection<ThemeBrushItem>();

        public ObservableCollection<ThemeBrushItem> LightResources { get; set; } = new ObservableCollection<ThemeBrushItem>();

        public ObservableCollection<ThemeBrushItem> HighContrastResources { get; set; } = new ObservableCollection<ThemeBrushItem>();
        

        public void LoadResources()
        {
            IsBusy = true;
            IsBusyMessage = "Reading themes...";

            if(DefaultResources.Any())
                DefaultResources.Clear();

            if (LightResources.Any())
                LightResources.Clear();

            if (HighContrastResources.Any())
                HighContrastResources.Clear();
            
            foreach (var mergedDictionary in Application.Current.Resources.MergedDictionaries)
            {
                foreach (var resource in mergedDictionary.ThemeDictionaries)
                {
                    // This will be "Default", "Light" and "HighContrast"
                    Debug.WriteLine($"Found {resource.Key}...");

                    if (resource.Value is ResourceDictionary dictionary)
                    {
                        foreach (var themeResource in dictionary)
                        {
                            try
                            {
                                if (themeResource.Value == null)
                                    continue;
                            }
                            catch
                            {
                                continue;
                            }
                            

                            if (themeResource.Value is SolidColorBrush brush)
                            {
                                try
                                {
                                    var key = themeResource.Key.ToString();

                                    if (key.Contains("Theme") && key.Contains("BrushValue"))
                                    {
                                        var item = new ThemeBrushItem();

                                        item.Name = key;

                                        item.BrushValue = brush;

                                        item.HexValue = ThemeBrushItem.GetBrushHexValue(brush);

                                        if (resource.Key.ToString() == "Default")
                                        {
                                            DefaultResources.Add(item);
                                        }
                                        else if (resource.Key.ToString() == "Light")
                                        {
                                            LightResources.Add(item);
                                        }
                                        else if (resource.Key.ToString() == "HighContrast")
                                        {
                                            HighContrastResources.Add(item);
                                        }
                                    }

                                    IsBusyMessage = $"Discovered and adding {key}...";
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            
            IsBusyMessage = "";
            IsBusy = false;
        }
    }
}
