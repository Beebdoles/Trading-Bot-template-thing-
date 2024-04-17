using Microsoft.Data.Analysis;

namespace Bot_Core.Commands
{
    public class Data
    {
        DataFrame data;

        public async Task fetchData(string stockType)
        {
            AVConnection conn = new AVConnection("8F5L0CP0KH25GVRL");
            await conn.SaveCSVFromURL(stockType);
            DataFrame df = DataFrame.LoadCsv("stockdata1.csv");
            data = df;
        }

        public DataFrame returnData()
        {
            return data;
        }

    }

    public class AVConnection
    {
        private readonly string apiKey;

        public AVConnection(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task SaveCSVFromURL(string symbol)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response  = await client.GetAsync("https://" + $@"www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&apikey={this.apiKey}&datatype=csv");

            string responseBody = await response.Content.ReadAsStringAsync();
            File.WriteAllText("stockdata1.csv", responseBody);
        }
    }
}