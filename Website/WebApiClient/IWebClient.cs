using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
        public interface IWebClient<T>
        {
            // Syncronius
            T Get();

            bool Post(string id);
            bool Post(T model);
            bool Post(T model, Stream file);
            bool Post(T model, List<string> files);

            // Asyncronius
            Task<T> GetAsync();

            Task<bool> PostAsync(string id);
            Task<bool> PostAsync(T model);
            Task<bool> PostAsync(T model, string file);
            Task<bool> PostAsync(T model, List<string> files);

        }


 }

