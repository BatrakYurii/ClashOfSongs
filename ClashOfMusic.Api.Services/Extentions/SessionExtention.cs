using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Services.Extentions
{
    public static class SessionExtention
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var dataInString = session.GetString(key);

            return dataInString == null ? default(T) : JsonConvert.DeserializeObject<T>(session.GetString(key));
        }
    }
}
