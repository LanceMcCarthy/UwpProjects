using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace UwpHelpers.Controls.Common
{
    public class IncrementalLoadingCollection<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        private readonly Func<CancellationToken, uint, Task<ObservableCollection<T>>> func;
        private uint maxItems;
        private bool isInfinite;
        private CancellationToken token;

        /// <summary>
        /// Infinite, incremental scrolling list supported by ListView and GridView
        /// </summary>
        /// <param name="func"></param>
        public IncrementalLoadingCollection(Func<CancellationToken, uint, Task<ObservableCollection<T>>> func)
            : this(func, 0)
        {
        }

        /// <summary>
        /// Incremental scrolling list supported by ListView and GridView
        /// </summary>
        /// <param name="func">Task that retrieves the items</param>
        /// <param name="maxItems">Set to the maximum number of items to expect</param>
        public IncrementalLoadingCollection(Func<CancellationToken, uint, Task<ObservableCollection<T>>> func, uint maxItems)
        {
            this.func = func;
            if (maxItems == 0) //Infinite
            {
                isInfinite = true;
            }
            else
            {
                this.maxItems = maxItems;
                isInfinite = false;
            }
        }

        public bool HasMoreItems
        {
            get
            {
                if (this.token.IsCancellationRequested)
                    return false;

                if (isInfinite)
                    return true;

                return this.Count < maxItems;
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return AsyncInfo.Run(tkn => InternalLoadMoreItemsAsync(tkn, count));
        }

        private async Task<LoadMoreItemsResult> InternalLoadMoreItemsAsync(CancellationToken passedToken, uint count)
        {
            ObservableCollection<T> tempList = null;
            this.token = passedToken;
            var baseIndex = this.Count;
            uint numberOfItemsToGet = 0;

            if (!isInfinite)
            {
                if (baseIndex + count < maxItems)
                {
                    numberOfItemsToGet = count;
                }
                else
                {
                    numberOfItemsToGet = maxItems - (uint) (baseIndex);
                }
            }
            else
            {
                numberOfItemsToGet = count;
            }

            tempList = await func(passedToken, numberOfItemsToGet);

            if (tempList.Count == 0) //no more items, stop the incremental loading 
            {
                maxItems = (uint) this.Count;
                isInfinite = false;
            }
            else
            {
                foreach (var item in tempList)
                {
                    this.Add(item);
                }
            }

            return new LoadMoreItemsResult { Count = (uint) tempList.Count };
        }
    }
}
