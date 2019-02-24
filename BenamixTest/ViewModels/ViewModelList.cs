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
            Initialize();
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
                    var dictionaryModel = new DictionaryModel();
                    dictionaryModel.DictionaryKey = Math.Round(Convert.ToDouble(bidsArr[i, 0]), 3);
                    dictionaryModel.DictionaryValue = Convert.ToDouble(bidsArr[i, 1]);

                    listBids.Add(dictionaryModel);
                }
            }
            var groupedBidsByPrace = listBids.GroupBy(u => u.DictionaryKey)
                                  .Select(group => new { DictionaryKey = group.Key, list = group.ToList() })
                                  .ToList();

            for (int i = 0; i < asksArrRows; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    var dictionaryModel = new DictionaryModel();
                    dictionaryModel.DictionaryKey = Math.Round(Convert.ToDouble(asksArr[i, 0]), 3);
                    dictionaryModel.DictionaryValue = Convert.ToDouble(asksArr[i, 1]);

                    listAsks.Add(dictionaryModel);
                }
            }
            var groupedAsksByPrace = listAsks.GroupBy(u => u.DictionaryKey)
                                  .Select(group => new { DictionaryKey = group.Key, list = group.ToList() })
                                  .ToList();

            var resultGrouped = groupedBidsByPrace.Concat(groupedAsksByPrace);

            List<double> totalList = new List<double>();
            double totalVolume = 0;
            foreach (var item in resultGrouped)
            {
                foreach (var i in item.list)
                {
                    totalList.Add(i.DictionaryKey * i.DictionaryValue);
                    totalVolume += i.DictionaryValue;
                }
            }

            var totalSum = totalList.ToArray().Sum();
        }

        private List<double> _responceList;
        public List<double> ResponceList
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

        private RootObject _rootObject;
        public RootObject RootObject
        {
            get { return _rootObject; }
            set
            {
                _rootObject = value;
                OnPropertyChanged();
            }
        }
    }
}
