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

            List<double> priceMassiv1 = new List<double>();
            List<double> priceMassiv2 = new List<double>();

            var mass1 = RootObject.bids;
            var mass2 = RootObject.asks;

            int rowsMass1 = mass1.GetUpperBound(0) + 1;
            int rowsMass2 = mass2.GetUpperBound(0) + 1;


            for (int i = 0; i < rowsMass1; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    var roundedElementPrice = Math.Round(Convert.ToDouble(mass1[i,j]),3);
                    priceMassiv1.Add(roundedElementPrice); 
                }
            }

            for (int i = 0; i < rowsMass2; i++)
            {
                for (int j = 0; j < 1; j++)
                {
                    var roundedElementPrice = Math.Round(Convert.ToDouble(mass2[i, j]), 3);
                    priceMassiv2.Add(roundedElementPrice);
                }
            }

            var resultPriceMassiv = priceMassiv1.Concat(priceMassiv2);
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
