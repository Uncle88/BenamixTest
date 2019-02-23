using System;
using System.Collections;
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
            ResponceList = Enumerable.Range(0, RootObject.asks.Length).ToDictionary(i => KeysCount(RootObject.asks), i => ValueCount(RootObject.bids));
        }

        private double ValueCount(object[,] bids)
        {
            double key = double.Parse(bids[0, 0].ToString()) + int.Parse(bids[1, 0].ToString());
            return Convert.ToDouble(Math.Pow(key, 3));
        }

        private double KeysCount(object[,] asks)
        {
            double key = double.Parse(asks[0, 0].ToString()) + int.Parse(asks[1, 0].ToString());
            return Convert.ToDouble(Math.Pow(key, 3));
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
