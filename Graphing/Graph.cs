using System.Text.Json;
using System.Collections;
using System.IO;
using Microsoft.Data.Analysis;

namespace Bot_Core.Commands
{
    public class Graph
    {
        string dataValues;
        string labelValues;
        string firstValue;
        string secondValue;

        public async Task createLabels(string stockType, int days)
        {
            Data test = new Data();
            await test.fetchData(stockType);
            DataFrame df = test.returnData();

            List<string> data = new List<string>();

            for(int i = 0; i < days; ++i)
            {
                string temp = df[i, 0].ToString().Substring(8, 2);
                data.Add(temp);
            }
            data.Reverse();
            
            string datas = "";

            foreach(string value in data)
            {
                datas += value + ",";
            }

            string newDatas = datas.Trim(',');
            labelValues = newDatas;
        }

        public async Task createString(string stockType, int days)
        {
            Data test = new Data();
            await test.fetchData(stockType);
            DataFrame df = test.returnData();

            List<string> data = new List<string>();

            for(int i = 0; i < days; ++i)
            {
                data.Add(df[i, 4].ToString());
                if(i == 0)
                {
                    secondValue = df[i, 4].ToString();
                }
                else if(i == days - 1)
                {
                    firstValue = df[i, 4].ToString();
                }
            }
            data.Reverse();

            string datas = "";

            foreach(string value in data)
            {
                datas += value + ",";
            }

            string newDatas = datas.Trim(',');
            dataValues = newDatas;
        }

        public string returnData()
        {
            return dataValues;
        }

        public string returnLabels()
        {
            return labelValues;
        }

        public decimal returnFirstValue()
        {
            return decimal.Parse(firstValue);
        }

        public decimal returnSecondValue()
        {
            return decimal.Parse(secondValue);
        }
    }
}