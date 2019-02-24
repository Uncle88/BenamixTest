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

            //List<double> priceMassiv1 = new List<double>();
            //List<double> priceMassiv2 = new List<double>();

            var mass1 = RootObject.bids;
            var mass2 = RootObject.asks;

            int rowsMass1 = mass1.GetUpperBound(0) + 1;
            int rowsMass2 = mass2.GetUpperBound(0) + 1;

            List<IGrouping<double, DictionaryModel>> output = new List<IGrouping<double, DictionaryModel>>();

            for (int i = 0; i < rowsMass1; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    DictionaryModel dictionaryModel = new DictionaryModel();
                    dictionaryModel.DictionaryKey = Math.Round(Convert.ToDouble(mass1[i, 0]), 3);
                    dictionaryModel.DictionaryValue = Convert.ToDouble(mass1[i, 1]);

                    List<DictionaryModel> list = new List<DictionaryModel>();
                    list.Add(dictionaryModel);

                    var groupedCustomerList = list.GroupBy(u => u.DictionaryKey)
                                          .Select(group => new { DictionaryKey = group.Key, list = group.ToList() })
                                          .ToList();
                }
            }


            for (int i = 0; i < rowsMass2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    var roundedElementPrice = Math.Round(Convert.ToDouble(mass2[i, j]), 3);
                    mass2[i, j] = roundedElementPrice;
                }
            }
        }

        private Dictionary<double, double> _responceList;
        public Dictionary<double,double> ResponceList
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
