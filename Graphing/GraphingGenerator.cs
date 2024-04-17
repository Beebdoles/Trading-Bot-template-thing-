using System;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using QuickChart;
using System.IO;

namespace Bot_Core.Commands
{
    public class GraphingGenerator : BaseCommandModule
    {

        [Command("viewStock")]
        [Description("View the progress of a company over the past 1-2 months")]
        public async Task viewStock(CommandContext ctx, [Description("Stock ticker")] string stock, [Description("Number of days to view (30 or 60")] int days)
        {
            if(days == 30 || days == 60){
                Chart qc = new Chart();

                qc.Width = 500;
                qc.Height = 300;
                qc.BackgroundColor = "rgba(255,255,255,1)";

                Graph graph = new Graph();

                await graph.createString(stock, days);
                await graph.createLabels(stock, days);

                string numbers = graph.returnData();
                string labels = graph.returnLabels();

                string stockNew = "'" + stock + "'";

                string colour1 = "";
                string colour2= "";

                if(graph.returnFirstValue() < graph.returnSecondValue())
                {
                    colour1 = "'rgb(18, 168, 43)'";
                    colour2 = "'rgba(18, 168, 43, 0.2)'";
                }
                else
                {
                    colour1 = "'rgb(191, 0, 0)'";
                    colour2 = "'rgba(191, 0, 0, 0.2)'";
                }

                string display = @"{type: 'line', data: {labels: [" + labels + "], datasets: [{label: " + stockNew + ", backgroundColor: " + colour2 + ", borderColor: " + colour1 + ", data: [" + numbers + "]}]},options: {scales: {yAxes: [{ticks: {beginAtZero: false,callback: (val) => {return '$' + val.toLocaleString(); },}}]}},}";
                qc.Config = display;

                await ctx.Channel.SendMessageAsync(qc.GetUrl()).ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("An error occured. Double check your parameters (eg: days must be either 30 or 60").ConfigureAwait(false);
            }
        }

    }
}