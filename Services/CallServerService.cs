using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TitanBot.Services
{
    public class CallServerService
    {
        public T GetResponse<T>(string url)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(url);

            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}
