using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LampSwitch
{
    public static class WebUtil
    {
        private static WebClient webClient = new WebClient();

        public static JObject GETAsync(string requestUri, Dictionary<string, string> param)
        {
            webClient.QueryString.Clear();
            foreach (KeyValuePair<string, string> kv in param)
            {
                webClient.QueryString.Add(kv.Key, kv.Value);
            }
            webClient.QueryString.Add("token", Settings.Token);

            string response;
            try
            {
                response = webClient.DownloadString("http://" + Settings.Address + ":" + Settings.Port + "/" + requestUri);
            }
            catch (Exception e)
            {
                throw new Exception("WebUtil: connect to server failed. \nMessage: " + e.Message);
            }
            string status, error, level = null, position = null;
            try
            {
                JObject obj = JObject.Parse(response);
                JObject _status = (JObject)obj["status"];
                status = (string)_status["status"];
                if (status == "success")
                {
                    level = (string)_status["level"];
                    position = (string)_status["position"];
                }
                error = (string)obj["error"];
            }
            catch (Exception e)
            {
                throw new Exception("WebUtil: JSON Parsing failed. \nMessage: " + e.Message);
            }

            if (status == null)
            {
                throw new Exception("WebUtil: status received null.");
            }

            if (status != "success")
            {
                throw new Exception("WebUtil: operation failed, error: " + error);
            }

            return new JObject
            {
                ["level"] = level,
                ["position"] = position
            };
        }
    }
}
