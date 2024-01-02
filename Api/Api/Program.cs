using System.Text;
using System.Net.Http;
using System.Drawing;

namespace Api
{
    internal class Program
    {

        static async Task Main()
        {

            Console.WriteLine("Enter username ");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter point");
            string point = Console.ReadLine();

            // Replace the API endpoint with your actual Web API URL
            string apiUrl = "https://pbl2-backend-c2aeef5ab7a5.herokuapp.com/api/logplayerscores";

            // Create an instance of HttpClient
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Create a sample JSON payload (you can replace it with your own data)
                    Dictionary<string, string> postData = new Dictionary<string, string>
                    {
                        {"username", userName},
                    {"score", point}
                    // Add more key-value pairs as needed
                    };

                    string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

                    // Create the HttpContent for the POST request
                    HttpContent content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Check if the request was successful (HTTP status code 2xx)
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content as a string
                        string responseContent = await response.Content.ReadAsStringAsync();

                        Console.WriteLine($"POST request successful. Response: {responseContent}");
                    }
                    else
                    {
                        Console.WriteLine($"POST request failed. Status Code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

    }
}