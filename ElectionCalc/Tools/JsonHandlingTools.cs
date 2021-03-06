﻿namespace ElectionCalc
{
    using Newtonsoft.Json.Linq;
    using System.Net;
    using System.Text;

    public static class JsonHandlingTools
    {
        // TODO: awaiter
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
