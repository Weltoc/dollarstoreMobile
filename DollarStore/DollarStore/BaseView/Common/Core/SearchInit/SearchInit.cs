using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DollarStore.BaseView.Common.Core.SearchInit
{
    public class SearchInit: BaseViewModel
    {
        bool _initSearch;
        public bool InitSearch
        {
            get => _initSearch;
            set => SetProperty(ref _initSearch, value);
        }
        public SearchInit()
        {
            InitSearch = true;
        }
    }
}
