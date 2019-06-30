# UwpProjects <img src="https://lance.visualstudio.com/DefaultCollection/_apis/public/build/definitions/99c7351f-17fc-47b4-9314-f259e58e77eb/2/badge" alt="Build status" />
A set of UWP controls and utilities that I have built and used as a Windows dev. I will continue to add to this from my personal colleciton of do-dads and bobbles and as a I build new ones.

##Contents

* [AdaptiveGridView](https://github.com/LanceMcCarthy/UwpProjects#adaptivegridview) in *UwpHelpers.Controls.ListControls* (maintains aspect ratio of items as it scales for column width)
* [BusyIndicators](https://github.com/LanceMcCarthy/UwpProjects#busyindicators) in *UwpHelpers.Controls.BusyIndicators* (custom busy indicators)
* [AnimatedBusyIndicator](https://github.com/LanceMcCarthy/UwpProjects#animatedbusyindicators) in *UwpHelpers.Controls.BusyIndicators* (custom busy indicators)
* [BlurElementAsync](https://github.com/LanceMcCarthy/UwpProjects#blurelementasync) in *UwpHelpers.Examples.Helpers* (converts any UIElement into a blurred bitmap)
* [IncrementalLoadingCollection](https://github.com/LanceMcCarthy/UwpProjects#incrementalloadingcollection) in *UwpHelpers.Controls.Common* (Use this for lazy-loading scenarios, demo in Examples)
* [NetworkImage](https://github.com/LanceMcCarthy/UwpProjects#networkimage) in *UwpHelpers.Controls.ImageControls* (an Image control that shows download progress)
* [DownloadStreamWithProgressAsync](https://github.com/LanceMcCarthy/UwpProjects#downloadstreamwithprogressasync-httpclient-extension) in *UwpHelpers.Controls.Extensions* (HttpClient Extension methods that reports download progress)
* [ReleaseNotesDialog](https://github.com/LanceMcCarthy/UwpProjects#releasenotesdialog) in *UwpHelpers.Controls.Dialogs* (shows a list of Features and Fixes using the familiar ContentDialog approach)


### AdaptiveGridView

![alt tag](https://i.gyazo.com/9d19b70d72c65c3d24fb81a857cdf4f8.gif)

### AdaptiveGridView with grouping

![alt tag](https://i.gyazo.com/d51eb22c60cbab80363b0e2976f9867d.gif)

**Properties**

* MinItemWidth (`double`)
* MinItemHeight (`double`)


**Example**

```
<listControls:AdaptiveGridView ItemsSource="{Binding ListItems}"
       MinItemHeight="105"
       MinItemWidth="315">
       
```

### AnimatedBusyIndicator
A UWP control leveraging WinUI and Composition

![Demo GIF](https://dvlup.blob.core.windows.net/general-app-files/GIFs/AnimatedBusyIndicator.gif)

##### Default use

```xml
<controls:AnimatedBusyIndicator IsActive="True"
                                BusyContent="I'm busy right now...">
    <controls:AnimatedBusyIndicator.AnimationContent>
        <lottie:LottieVisualSource UriSource="ms-appx:///Assets/LottieFiles/AlienIdScan.json" />
    </controls:AnimatedBusyIndicator.AnimationContent>
</controls:AnimatedBusyIndicator>
```

##### Custom content and overridden animation settings
```xml
<controls:AnimatedBusyIndicator IsActive="True"
                                AnimationWidth="100"
                                AnimationHeight="100"
                                AnimationHorizontalAlignment="Center"
                                AnimationVerticalAlignment="Center">
    <controls:AnimatedBusyIndicator.BusyContent>
        <!-- The internal ContentPresenter will faithfully display your content, but can still take advantage of inherited properties like Foreground -->
        <TextBlock Text="I'm custom content, yay!" FontWeight="Bold" />
    </controls:AnimatedBusyIndicator.BusyContent>
    <controls:AnimatedBusyIndicator.AnimationContent>
        <lottie:LottieVisualSource UriSource="ms-appx:///Assets/LottieFiles/ChartLoading.json" />
    </controls:AnimatedBusyIndicator.AnimationContent>
</controls:AnimatedBusyIndicator>
```

### BusyIndicators

![alt tag](https://i.gyazo.com/f95e8b62627551bec566f45aa7b77655.gif)

* BandBusyIndicator
* DownloadUploadIndicator



**Properties**

* IsActive (`boolean`): shows or hides the indicator
* Direction (`AnimationDirection`): The direction of the animation
* DisplayMessage (`string`): message shown when active
* DisplayMessageSize (`double`): message font size

```
<busyIndicators:BandBusyIndicator IsActive="{Binding IsBusy}"
      DisplayMessage="busy..."
      Direction="Uploading"  />
```

### BlurElementAsync

![alt tag](https://i.gyazo.com/b1ef38ded3e6428e607595d8638bb88f.gif)


**Example**

```
//You can pass any UIElement to the method and it will render all of the children into a bitmap with a Blur applied
var blurredElement = await ContentToBlur.BlurElementAsync();

//example: you can then set Background brush of a Grid
ContentRootGrid.Background = new ImageBrush
{
  ImageSource = blurredBitmapImage,
  Stretch = Stretch.UniformToFill
};

```


### IncrementalLoadingCollection

![alt tag](https://i.gyazo.com/450b257a52ece99e59052af9ff28d825.gif)


**Example**

*XAML*

```

<ListView ItemsSource="{Binding InfiniteItems}" />

```

*ViewModel or Code-Behind*


```

InfiniteItems = new IncrementalLoadingCollection<T>((cancellationToken, count) => Task.Run(GetMoreData, cancellationToken));
    
//and GetMoreData is
private async Task<ObservableCollection<T>> GetMoreData()
{
   return more items of type ObservableCollection<T>
}

```


### NetworkImage

![alt tag](https://i.gyazo.com/3cfc9b6d98bd5b060440a308edc45df7.gif)


**Properties**

* ImageUrl (`string`): string url of the photo
* IsActive (`bool`) - the control manages this automatically, but you can manually enable/disable if needed 
* DownloadPercentageVisibility (`Visibility`) - If you want to hide the progress percentage
* ProgressRingVisibility (`Visibility`) - If you want to hide the ProgressRing animation
* ImageStretch (`Stretch`) - Stretch property passed ot the underlying Image control


**Example**

*XAML*

```

<imageControls:NetworkImage ImageUrl="http://bigimages.com/MyHugeImage.jpg" />

``` 


### DownloadStreamWithProgressAsync (HttpClient Extension)

![alt tag](https://i.gyazo.com/70dc4afcd36b9a04a9c3159c67000a1c.gif)


**Properties**

* Url (`string`): url of the thing you want to download
* Reporter (`Progress<DownloadProgressArgs>`) - reports the progress via event, see example below

Note: There are a couple more methods in the helper class (i.e. DownloadStringwithProgressAsync)


**Example**

*C# - usage*

```

var reporter = new Progress<DownloadProgressArgs>();
reporter.ProgressChanged += Reporter_ProgressChanged;

var imageStream = await new HttpClient(myFavoriteHandler).DownloadStreamWithProgressAsync(bigImageUrl, reporter)
    
```

*C# - event handler*

```
private void Reporter_ProgressChanged(object sender, DownloadProgressArgs e)
{
    SomeProgressBar.Value = e.PercentComplete;
}
    
```


### ReleaseNotesDialog 

![alt tag](https://i.gyazo.com/25a89e98e5846e4ffff36b1b14e6399b.gif)


**Properties**

* AppName (`string`): Set the Dialog's title, default value is "Release Notes"
* Features (`ObservableCollection<string>`): List of new features
* Fixes (`ObservableCollection<string>`): List of fixes
* UseFullVersionNumber (`bool`): Determines whether to show the Build number (1.0.X).




**Example**

*C# - usage*

```

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




