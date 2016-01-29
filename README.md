# UwpProjects
A set of UWP controls and utilities (I will add more over time)

##Contents
* BandBusyIndicator in *UwpHelpers* (cool custom busy indicator)
* AdaptiveGridView in *UwpHelpers* (maintains aspect ratio of items as it scales for column width)
* DateRangePicker in *TelerikUwp* (select a date range with start/end overlap protection)


###AdaptiveGridView

![alt tag](https://i.gyazo.com/8b4eda7cd246474d4e7ec4262aecc82b.gif)


**Properties**
* MinItemWidth (double)
* MinItemHeight (double)


** Example **
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


###DateRangePicker

![alt tag](https://i.gyazo.com/985e926ed201a7991aee4c4110bacbcc.gif)



**Properties and Events**
* StartDate (DateTime)
* EndDate (DateTime)
* DateRangeChanged (passed DateRangeChangedEventArgs)


**Databinding use example**

```
<customControls:DateRangePicker StartDate="{Binding MyStartDate, Mode=TwoWay}"
                                EndDate="{Binding MyEndDate, Mode=TwoWay}" />
```


** Event use example **


*XAML*

```
<customControls:DateRangePicker x:Name="MyDateRangePicker"
                                DateRangeValueChanged="MyDateRangePicker_OnDateRangeValueChanged" />
```


*Event Handler*

```
private void MyDateRangePicker_OnDateRangeValueChanged(object sender, DateRangeChangedEventArgs e)
{
      var startDate = e.StartDate;
      var endDate = e.EndDate;
}
```

