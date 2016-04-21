using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using UwpHelpers.Controls.Common;
using UwpHelpers.Examples.Annotations;

namespace UwpHelpers.Examples.ViewModels
{
    public class IncrementalScrollingViewModel : INotifyPropertyChanged
    {
        private int currentPage = 1;

        public IncrementalScrollingViewModel()
        {
            if (DesignMode.DesignModeEnabled)
                return;

            InitializeData();
        }
        
        public IncrementalLoadingCollection<IncrementedItem> InfiniteItems { get; set; }
        
        private void InitializeData()
        {
            if (DesignMode.DesignModeEnabled)
                return;

            //when instantiating the collection, you pass it the task that gets more item, in this case it's "GetMoreData"
            //NOTE: This is for infinite items. if you have a max or total count, use the overload and pass the total as the 2nd parameter
            InfiniteItems = new IncrementalLoadingCollection<IncrementedItem>((cancellationToken, count) => Task.Run(GetMoreData, cancellationToken));
        }

        //all you need to do is return a list of your data in this task
        private async Task<ObservableCollection<IncrementedItem>> GetMoreData()
        {
            //I'm just simulating an API that supports paging
            return await FakeApiCallAsync(currentPage++, 50);
        }

        #region unrelated demo code

        //Super Awesome API :D
        private async Task<ObservableCollection<IncrementedItem>> FakeApiCallAsync(int pageNumber, int itemsPerPage)
        {
            return await Task.Run(() =>
            {
                var items = new ObservableCollection<IncrementedItem>();

                var startingRecordToUse = itemsPerPage * (pageNumber - 1);
                var endingRecordToUse = startingRecordToUse + itemsPerPage;

                for (int i = startingRecordToUse; i < endingRecordToUse; i++)
                {
                    items.Add(new IncrementedItem { Id = i, Title = $"Item {i}" });
                }

                return items;
            });
        }
        
        #region INPC
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #endregion
    }

    public class IncrementedItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
