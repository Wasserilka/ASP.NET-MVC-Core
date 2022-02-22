using System;
using HtmlAgilityPack;

namespace Lesson_4
{
    public interface IParser
    {
        public HtmlNodeCollection GetNodes();
        public void LoadHtml(string html);
        public Model DataExtractor(HtmlNode Node);
    }

    public class Parser : IParser
    {
        private HtmlDocument Document = new HtmlDocument();

        public void LoadHtml(string HtmlBody)
        {
            Document.LoadHtml(HtmlBody);
        }

        public HtmlNodeCollection GetNodes()
        {
            var NodeCollection = Document.DocumentNode.SelectNodes("//ul[@class='swiper-wrapper']//div[contains(@class, 'forecast-briefly__day')]");
            return NodeCollection;
        }

        //Использован? паттерн Facade
        public Model DataExtractor(HtmlNode Node)
        {
            var InnerNodes = GetInnerNodes(Node);
            var NodesInnerText = new String[InnerNodes.Count];

            NodesInnerText[0] = InnerNodes[0].GetAttributeValue("datetime", "1970-01-01");

            for (int i = 1; i < InnerNodes.Count; i++)
            {
                NodesInnerText[i] = InnerNodes[i].InnerText;
            }

            var Model = new Model();
            Map(Model, NodesInnerText);

            return Model;
        }
        
        private HtmlNodeCollection GetInnerNodes(HtmlNode InnerNode)
        {
            
            var NodeCollection = InnerNode.SelectNodes("./a/div[contains(@class, 'temp')]//span[contains(@class, 'temp__value')]");
            NodeCollection.Append(InnerNode.SelectSingleNode("./a/div[@class='forecast-briefly__condition']"));
            NodeCollection.Prepend(InnerNode.SelectSingleNode("./a/time[contains(@class, 'time')]"));
            return NodeCollection;
        }

        private void Map(Model Model, string[] Data)
        {
            Model.Date = GetDateTimeFromString(Data[0]);
            Model.DayTemperature = Data[1];
            Model.NightTemperature = Data[2];
            Model.Description = Data[3];
        }

        private DateTime GetDateTimeFromString(string DateTimeString)
        {
            var DateTimeData = Array.ConvertAll(DateTimeString.Split(' ')[0].Split('-'), s => int.Parse(s));
            return new DateTime(DateTimeData[0], DateTimeData[1], DateTimeData[2]);

        }
    }
}
