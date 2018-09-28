using System;
using System.Net;
using System.Threading.Tasks;
using IO.Ably;
using IO.Ably.Realtime;
using Newtonsoft.Json;

namespace example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var options = new ClientOptions
            {
                AuthCallback = async tokenParams =>
                {
                    return await GetTokenRequestStringFromYourServer();
                    
                    // or return a TokenDetails object 
                    // return await GetTokenDetailsFromYourServer();
                }
            };
            var client = new AblyRealtime(options);
            client.Connection.Once(ConnectionState.Connected, change =>
            {
                Console.WriteLine("connected");
            });
            client.Connection.On(x =>
            {
                Console.WriteLine(x.Reason.ToString());
            });
            Console.ReadLine();    
        }

        static async Task<object> GetTokenDetailsFromYourServer()
        {
            Console.WriteLine("Getting token details from web server.");
            var webClient = new WebClient();
            string tokenJson = string.Empty;
            try
            {
                tokenJson = await webClient.DownloadStringTaskAsync("http://localhost:5000/ablyauth/tokendetails");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting token from web server, is started?");
                Console.WriteLine(ex.Message);
            }
            
            return JsonConvert.DeserializeObject<TokenDetails>(tokenJson);
        }
        
        static async Task<object> GetTokenRequestStringFromYourServer()
        {
            Console.WriteLine("Getting token request from web server.");
            var webClient = new WebClient();
            string tokenJson = string.Empty;
            try
            {
                tokenJson = await webClient.DownloadStringTaskAsync("http://localhost:5000/ablyauth/tokenrequest");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting token from web server, is started?");
                Console.WriteLine(ex.Message);
            }

            return tokenJson;
        }
    }
}