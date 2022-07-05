using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Http
{
    public interface IWebApiClient
    {
        HttpResponseMessage Get(Uri address, Dictionary<string, string> parameters);

        HttpResponseMessage Post<T>(Uri addres, T transferObject);

        HttpResponseMessage Delete<T>(Uri addres, T transferObject);

        HttpResponseMessage Put<T>(Uri addres, T transferObject);

        HttpResponseMessage Patch<T>(Uri addres, T transferObject);
    }
}
