using DollarStore.Common.Report.Model;
using DollarStore.Common.Report.Service;
using DollarStore.Interface;
using DollarStore.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DollarStore.Common.Report
{
    public class PurchaseReportVeiwModel:BaseViewModel
    {
        public ObservableRangeCollection<PurchaseReportModel> LpurchaseReport { get; set; }
        public List<PurchaseReportModel> FpurchaseReport { get; set; }
        public Command GetReportCommand { get; }
        public Command<string> SearchItemCommand { get; }
        public Command LoadVendorCommand { get; }
        public PurchaseReportVeiwModel()
        {
            LpurchaseReport = new ObservableRangeCollection<PurchaseReportModel>();
            FpurchaseReport = new List<PurchaseReportModel>();
            TVendors = new ObservableRangeCollection<Vendor>();
            TShops = new List<Shop>();
            Users = new ObservableRangeCollection<UserProfile>();

            GetReportCommand = new Command(async () => await GetReport());

            SearchItemCommand = new Command<string>(async (string text) => await SearchReport(text));

            LoadVendorCommand = new Command(async () => await GetVendors());

            Status = "In progress";
            var _mounth = DateTime.Now.Month;
            var _day = DateTime.Now.Day;
            var _year = DateTime.Now.Year;
            if (_mounth == 1)
            {
                _mounth = 12;
                _year -= 1;
            }
            else
            {
                _mounth -= 1;
            }
            DateFrom = $"{_mounth}/{DateTime.Now.Day}/{_year}";
            DateTo = DateTime.Now.ToShortDateString();
        }

        #region OBSERVABLE PROPERTIES
        List<Shop> _shops = new List<Shop>();
        public List<Shop> TShops
        {
            get { return _shops; }
            set { SetProperty(ref _shops, value); }
        }

        ObservableRangeCollection<Vendor> _vendor;
        public ObservableRangeCollection<Vendor> TVendors
        {
            get => _vendor;
            set => SetProperty(ref _vendor, value);

        }
        public ObservableRangeCollection<UserProfile> Users { get; set; }

        #endregion

        #region FILTERPROPERTIES
        long _pagination = 0;
        public long Pagination
        {
            get => _pagination;
            set => SetProperty(ref _pagination, value);
        }
        string _status = "";
        public string Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        string _vendorId = string.Empty;
        public string VendorID
        {
            get { return _vendorId; }
            set { SetProperty(ref _vendorId, value); }
        }

        string _storeid = string.Empty;
        public string StoreId
        {
            get { return _storeid; }
            set { SetProperty(ref _storeid, value); }
        }

        Vendor _selectVendor;
        public Vendor SelectVendor
        {
            get { return _selectVendor; }
            set
            {
                if (SetProperty(ref _selectVendor, value))
                {
                    if (_selectVendor != null)
                    {
                        TShops = _selectVendor.Stores;
                        VendorID = _selectVendor.Id;
                    }
                }
            }
        }

        Shop _selectShop;
        public Shop SelectShop
        {
            get { return _selectShop; }
            set
            {
                if (SetProperty(ref _selectShop, value))
                {
                    if (_selectShop != null)
                    {
                        StoreId = _selectShop.Id;
                    }
                }
            }
        }


        string _dateFrom = "";
        public string DateFrom
        {
            get => _dateFrom;
            set => SetProperty(ref _dateFrom, value);
        }
        string _dateTo = "";
        public string DateTo
        {
            get => _dateTo;
            set => SetProperty(ref _dateTo, value);
        }

        DateTime _minimumDate = DateTime.Now;
        public DateTime MinimumDate
        {
            get => _minimumDate;
            set => SetProperty(ref _minimumDate, value);
        }


        public void ClearDate()
        {
            DateFrom = string.Empty;
            DateTo = string.Empty;
        }

        public void ClearVendor()
        {
            VendorID = string.Empty;
            SelectVendor = null;
            StoreId = string.Empty;
            SelectShop = null;
        }

        #endregion

        #region GETREPORT
        public async Task GetReport()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var poProvider = DependencyService.Get<IReportService>();
                var Pos = await poProvider.GetCurrentPurchaseReport(
                   Pagination,Status,VendorID,StoreId, DateFrom, DateTo);

                LpurchaseReport.ReplaceRange(Pos);
                FpurchaseReport.Clear();
                FpurchaseReport.AddRange(Pos);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Report", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
           
        }
        #endregion

        #region SEARCHIN REPORT
        public async Task SearchReport(string searchText)
        {
            try
            {
                IsBusy = true;

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    var data = FpurchaseReport;

                    var search = await Task.Run(() =>
                    {
                        var s = data.Where(t => t.ItemBarcode == searchText).ToList();
                        if(s.Count()< 1)
                        {
                            s = data.Where(t => t.ItemName.ToString().ToLower()
                                        .IndexOf(searchText.ToString().ToLower()) != -1).ToList();
                        }

                        return s;
                    });

                    LpurchaseReport.ReplaceRange(search);
                }
                else
                {
                    LpurchaseReport.ReplaceRange(FpurchaseReport);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region SEARCHEMPTY
        public void SearchEmpty()
        {
            LpurchaseReport.ReplaceRange(FpurchaseReport);
        }
        #endregion

        #region GET FILTER VENDOR
        public async Task GetVendors()
        {

            try
            {

                IsBusy = true;
                SelectShop = null;
                TShops = new List<Shop>();
                SelectVendor = null;
                TVendors = new ObservableRangeCollection<Vendor>();

                var fbVendorUser = DependencyService.Get<IVendorService>();

                var Getvendors = await fbVendorUser.GetVendors();

                TVendors.AddRange(Getvendors);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Getting Vendor", ex.Message, "ok");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
