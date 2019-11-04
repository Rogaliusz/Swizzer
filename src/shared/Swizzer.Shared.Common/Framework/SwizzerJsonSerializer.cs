using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Common.Framework
{
    public class SwizzerJsonSerializer
    {
        public static string Serialize(object obj)
            => JsonConvert.SerializeObject(obj);

        public static TEntity Deserialize<TEntity>(string json)
            => JsonConvert.DeserializeObject<TEntity>(json);
    }
}
