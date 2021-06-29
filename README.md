# UWP Projects
A set of custom UWP controls and utilities that I have built for my apps. I will continue to add to this repo from my personal collection of doo-dads and bobbles and as a I build new ones 🚀

| Branch   | Build Status  |
|----------|---------------|
| `master` | [![Components Build](https://dev.azure.com/lance/UwpProjects/_apis/build/status/Components%20Build)](https://dev.azure.com/lance/UwpProjects/_build/latest?definitionId=2) |

## Components

| Component (w/ jumplink) | Namespace | Description |
|-----------|-----------|-------------|
| [AdaptiveGridView](https://github.com/LanceMcCarthy/UwpProjects#adaptivegridview) | `UwpHelpers.Controls.ListControls` | Maintains aspect ratio of items as column width changes |
| [BusyIndicators](https://github.com/LanceMcCarthy/UwpProjects#busyindicators) | `UwpHelpers.Controls.BusyIndicators` | Custom busy indicators |
| [BlurElementAsync](https://github.com/LanceMcCarthy/UwpProjects#blurelementasync) | `UwpHelpers.Examples.Helpers` | Converts any UIElement into a blurred bitmap |
| [IncrementalLoadingCollection](https://github.com/LanceMcCarthy/UwpProjects#incrementalloadingcollection) | `UwpHelpers.Controls.Common` | Use this for lazy-loading scenarios, demo in Examples |
| [NetworkImage](https://github.com/LanceMcCarthy/UwpProjects#networkimage) | `UwpHelpers.Controls.ImageControls` | An Image control that shows download progress |
| [DownloadStreamWithProgressAsync](https://github.com/LanceMcCarthy/UwpProjects#downloadstreamwithprogressasync-httpclient-extension) | `UwpHelpers.Controls.Extensions` | HttpClient Extension methods that reports download progress|
| [ReleaseNotesDialog](https://github.com/LanceMcCarthy/UwpProjects#releasenotesdialog) | `UwpHelpers.Controls.Dialogs` | Shows a list of Features and Fixes using the familiar ContentDialog approach |

#### AdaptiveGridView

![alt tag](https://i.gyazo.com/9d19b70d72c65c3d24fb81a857cdf4f8.gif)

##### With grouping

![alt tag](https://i.gyazo.com/d51eb22c60cbab80363b0e2976f9867d.gif)

##### Properties

* **MinItemWidth** - `double`: Sets minimum width of GridView items.
* **MinItemHeight** - `double`: Sets the minimum height of GridView items.

##### Example

```xml
<listControls:AdaptiveGridView ItemsSource="{Binding ListItems}"
       MinItemHeight="105"
       MinItemWidth="315">
```

#### BusyIndicators

* **BandBusyIndicator**
* **DownloadUploadIndicator**

![alt tag](https://i.gyazo.com/f95e8b62627551bec566f45aa7b77655.gif)

##### Properties

* **IsActive** - `boolean`: Shows or hides the indicator.
* **Direction** - `AnimationDirection`: The direction of the animation.
* **DisplayMessage** - `string`: Message shown when active.
* **DisplayMessageSize** - `double`: Message font size.

```xml
<busyIndicators:BandBusyIndicator IsActive="{Binding IsBusy}"
      DisplayMessage="busy..."
      Direction="Uploading"  />
```

#### BlurElementAsync

![alt tag](https://i.gyazo.com/b1ef38ded3e6428e607595d8638bb88f.gif)

##### Example

```csharp
//You can pass any UIElement to the method and it will render all of the children into a bitmap with a Blur applied
var blurredElement = await ContentToBlur.BlurElementAsync();

//example: you can then set Background brush of a Grid
ContentRootGrid.Background = new ImageBrush
{
  ImageSource = blurredBitmapImage,
  Stretch = Stretch.UniformToFill
};
```

#### IncrementalLoadingCollection

![alt tag](https://i.gyazo.com/450b257a52ece99e59052af9ff28d825.gif)

##### Example

```xml
<ListView ItemsSource="{Binding InfiniteItems}" />
```

*ViewModel or Code-Behind*

```csharp
InfiniteItems = new IncrementalLoadingCollection<T>((cancellationToken, count) => Task.Run(GetMoreData, cancellationToken));
    
//and GetMoreData is
private async Task<ObservableCollection<T>> GetMoreData()
{
   return more items of type ObservableCollection<T>
}
```
#### NetworkImage

![alt tag](https://i.gyazo.com/3cfc9b6d98bd5b060440a308edc45df7.gif)

##### Properties

* **ImageUrl** - `string`: String URL of the photo.
* **IsActive** - `bool`: The control manages this automatically, but you can manually enable/disable if needed .
* **DownloadPercentageVisibility** - `Visibility`: If you want to hide the progress percentage.
* **ProgressRingVisibility** - `Visibility`: If you want to hide the ProgressRing animation.
* **ImageStretch** -`Stretch`: Stretch property passed to the underlying Image control.

##### Example

```xml
<imageControls:NetworkImage ImageUrl="http://bigimages.com/MyHugeImage.jpg" />
``` 

### DownloadStreamWithProgressAsync (HttpClient Extension)

![alt tag](https://i.gyazo.com/70dc4afcd36b9a04a9c3159c67000a1c.gif)

##### Properties

* **Url** - `string`: Url of the thing you want to download.
* **Reporter** - `Progress<DownloadProgressArgs>` - reports the progress via event args.

> Note: There are a couple more methods in the helper class (i.e. DownloadStringwithProgressAsync)

##### Example

```csharp
void SetupReporter()
{
    var reporter = new Progress<DownloadProgressArgs>();
    reporter.ProgressChanged += Reporter_ProgressChanged;

    var imageStream = await new HttpClient(myFavoriteHandler).DownloadStreamWithProgressAsync(bigImageUrl, reporter)
}

private void Reporter_ProgressChanged(object sender, DownloadProgressArgs e)
{
    SomeProgressBar.Value = e.PercentComplete;
}
```

#### ReleaseNotesDialog 

![alt tag](https://i.gyazo.com/25a89e98e5846e4ffff36b1b14e6399b.gif)

##### Properties

* **AppName** - `string`: Sets the dialog title (default value is "Release Notes").
* **Features** - `ObservableCollection<string>`: List of new features.
* **Fixes** - `ObservableCollection<string>`: List of fixes.
* **UseFullVersionNumber** - `bool`: Determines whether to show the manifest build number (e.g. 1.0.0).

##### Example

```csharp
var rnd = new ReleaseNotesDialog();
    
rnd.AppName = "My App Name";
rnd.Message = "Thank you for checking out ReleaseNotesDialog! Here's a list of what's new and what's fixed:";
    
rnd.Features = new ObservableCollection<string>
{ 
    "Amazing feature!", 
    "Added theming", 
    "Backup and restore added!" 
};
    
rnd.Fixes = new ObservableCollection<string>
{
    "Fixed crash when opening",
    "Added text wrapping to fix text being cut off"
};
    
await rnd.ShowAsync();
```


> Lancelot Software © 2010-2021
