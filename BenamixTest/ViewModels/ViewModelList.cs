using System;
using System.Collections.Generic;
using System.Linq;
using BenamixTest.Models;
using BenamixTest.Services.Rest;

namespace BenamixTest.ViewModels
{
    public class ViewModelList : BaseViewModel
    {
        private readonly IRestService _restService;

        public ViewModelList()
        {
            _restService = new RestService();
        }

        public override async void Initialize()
        {
            RootObject = await _restService.GetData();

            var bidsArr = RootObject.bids;
            var asksArr = RootObject.asks;

            int bidsArrRows = bidsArr.GetUpperBound(0) + 1;
            int asksArrRows = asksArr.GetUpperBound(0) + 1;

            var listBids = new List<DictionaryModel>();
            var listAsks = new List<DictionaryModel>();

            for (int i = 0; i < bidsArrRows; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    var dictionaryBidsModel = new DictionaryModel();
                    dictionaryBidsModel.DictionaryKey = Math.Round(Convert.ToDouble(bidsArr[i, 0]), 3);
                    dictionaryBidsModel.DictionaryValue = Convert.ToDouble(bidsArr[i, 1]);
                    dictionaryBidsModel.Total = dictionaryBidsModel.DictionaryKey * dictionaryBidsModel.DictionaryValue;
                    listBids.Add(dictionaryBidsModel);
                }
            }
            var groupedBidsByPrace = listBids.GroupBy(u => u.DictionaryKey)
                                  .Select(group => new { DictionaryKey = group.Key, list = group.ToList() })
                                  .ToList();

            for (int i = 0; i < asksArrRows; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    var dictionaryAsksModel = new DictionaryModel();
                    dictionaryAsksModel.DictionaryKey = Math.Round(Convert.ToDouble(asksArr[i, 0]), 3);
                    dictionaryAsksModel.DictionaryValue = Convert.ToDouble(asksArr[i, 1]);
                    dictionaryAsksModel.Total = dictionaryAsksModel.DictionaryKey * dictionaryAsksModel.DictionaryValue;
                    listAsks.Add(dictionaryAsksModel);
                }
            }
            var groupedAsksByPrace = listAsks.GroupBy(u => u.DictionaryKey)
                                  .Select(group => new { DictionaryKey = group.Key, list = group.ToList() })
                                  .ToList();

            var resultGrouped = groupedBidsByPrace.Concat(groupedAsksByPrace);
            ResponceList = listBids.Concat(listAsks).ToList();
            var totalList = new List<double>();
            
            foreach (var item in resultGrouped)
            {
                foreach (var i in item.list)
                {
                    totalList.Add(i.DictionaryKey * i.DictionaryValue);
                    TotalVolume += i.DictionaryValue;
                }
            }

            TotalSum = totalList.ToArray().Sum();
        }

        private List<DictionaryModel> _responceList;
        public List<DictionaryModel> ResponceList
        {
            get { return _responceList; }
            set
            {
                if (_responceList != value)
                {
                    _responceList = value;
                }
                OnPropertyChanged();
            }
        }

        private RootObject RootObject { get; set; }

        private double _totalSum;
        public double TotalSum
        {
            get { return _totalSum; }
            set
            {
                _totalSum = value;
                OnPropertyChanged();
            }
        }

        private double _totalVolume;
        public double TotalVolume
        {
            get { return _totalVolume; }
            set
            {
                _totalVolume = value;
                OnPropertyChanged();
            }
        }

        //private double _total;
        //public double Total
        //{
        //    get { return _total; }
        //    set
        //    {
        //        _total = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}
