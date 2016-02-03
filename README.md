# UwpProjects <img src="https://lance.visualstudio.com/DefaultCollection/_apis/public/build/definitions/99c7351f-17fc-47b4-9314-f259e58e77eb/2/badge" alt="Build status" />
A set of UWP controls and utilities (I will add more over time)
*Note: To see my Telerik UWP examples, go here: https://github.com/LanceMcCarthy/TelerikUwpProjects*

##Contents
* BandBusyIndicator in *UwpHelpers.Controls.BusyIndicators* (cool custom busy indicator)
* AdaptiveGridView in *UwpHelpers.Controls.ListControls* (maintains aspect ratio of items as it scales for column width)
* BlurElementAsync in *UwpHelpers.Examples.Helpers* (converts any UIElement into a blurred BitmapImage)
* IncrementalLoadingCollection in *UwpHelpers.Controls.Common* (demo in Examples)


###AdaptiveGridView

![alt tag](https://i.gyazo.com/8b4eda7cd246474d4e7ec4262aecc82b.gif)


**Properties**
* MinItemWidth (double)
* MinItemHeight (double)


**Example**
```
<listControls:AdaptiveGridView ItemsSource="{Binding ListItems}"
                               MinItemHeight="105"
                               MinItemWidth="315">

```


###BandBusyIndicator

![alt tag](https://i.gyazo.com/ba631921317b4f8a5a51b3506e9f53ff.gif)


**Properties**

* IsActive (boolean): shows or hides the indicator
* Direction (AnimationDirection.Uploading or Downloading): The direction of the animation
* DisplayMessage (string): message shown when active


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

