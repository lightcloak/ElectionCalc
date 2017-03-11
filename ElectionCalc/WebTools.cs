using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace ElectionCalc
{
    public static class WebTools
    {
        // Zaimplementowac awaiter
        public static JObject JSONFromUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                var json = wc.DownloadString(url);
                return JObject.Parse(json);
            }
        }
    }
}
