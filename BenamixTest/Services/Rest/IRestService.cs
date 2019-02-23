using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BenamixTest.Models;

namespace BenamixTest.Services.Rest
{
    public interface IRestService
    {
        Task<RootObject> GetData();
    }
}
