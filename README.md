# UwpProjects <img src="https://lance.visualstudio.com/DefaultCollection/_apis/public/build/definitions/99c7351f-17fc-47b4-9314-f259e58e77eb/2/badge" alt="Build status" />
A set of UWP controls and utilities (I will add more over time)
*Note: To see my Telerik UWP examples, go here: https://github.com/LanceMcCarthy/TelerikUwpProjects*

##Contents
* BusyIndicators in *UwpHelpers.Controls.BusyIndicators* (cool custom busy indicator)
* AdaptiveGridView in *UwpHelpers.Controls.ListControls* (maintains aspect ratio of items as it scales for column width)
* BlurElementAsync in *UwpHelpers.Examples.Helpers* (converts any UIElement into a blurred BitmapImage)
* IncrementalLoadingCollection in *UwpHelpers.Controls.Common* (demo in Examples)
* NetworkImage in *UwpHelpers.Controls.ImageControls* (an Image control that shows download progress)
* DownloadStreamWithProgressAsync in *UwpHelpers.Controls.Extensions* (HttpClient Extension method that reports download progress)


###AdaptiveGridView

![alt tag](https://i.gyazo.com/9d19b70d72c65c3d24fb81a857cdf4f8.gif)


**Properties**
* MinItemWidth (double)
* MinItemHeight (double)


**Example**
```
<listControls:AdaptiveGridView ItemsSource="{Binding ListItems}"
                               MinItemHeight="105"
                               MinItemWidth="315">

```


###BusyIndicators

![alt tag](https://i.gyazo.com/f95e8b62627551bec566f45aa7b77655.gif)


* BandBusyIndicator
* DownloadUploadIndicator

**Properties**

* IsActive (boolean): shows or hides the indicator
* Direction (AnimationDirection.Uploading or Downloading): The direction of the animation
* DisplayMessage (string): message shown when active
* DisplayMessageSize (double): message font size


```
<busyIndicators:BandBusyIndicator IsActive="{Binding IsBusy}"
                                  DisplayMessage="busy..."
                                  Direction="Uploading"  />
```

###BlurElementAsync

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

###IncrementalLoadingCollection

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

###NetworkImage

![alt tag](https://i.gyazo.com/3cfc9b6d98bd5b060440a308edc45df7.gif)


**Properties**

* ImageUrl (string): string url of the photo
* IsActive (bool) - the control manages this automatically, but you can manually enable/disable if needed 
* DownloadPercentageVisibility (Visibility) - If you want to hide the progress percentage
* ProgressRingVisibility (Visibility) - If you want to hide the ProgressRing animation
* ImageStretch (Stretch) - Stretch property passed ot the underlying Image control


**Example**

*XAML*
```
<imageControls:NetworkImage ImageUrl="http://bigimages.com/MyHugeImage.jpg" />
```

	
###DownloadStreamWithProgressAsync (HttpClient Extension)

![alt tag](https://i.gyazo.com/70dc4afcd36b9a04a9c3159c67000a1c.gif)


**Properties**

* Url (string): url of the thing you want to download
* Reporter (Progress<DownloadProgressArgs> ) - reports the progress via event, see example below

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


