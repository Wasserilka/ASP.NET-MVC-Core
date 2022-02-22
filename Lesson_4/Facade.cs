using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System;

namespace Lesson_4
{
    public sealed class Facade
    {
        private readonly IResponse _Response;
        private readonly IParser _Parser;

        public Facade()
        {
            _Response = new Response();           
            _Parser = new Parser();
        }

        //Использован паттерн Facade
        public async Task<ObservableCollection<Model>> GetWeatherData(string Uri)
        {
            
            try
            {
                _Response.Uri = Uri;
                var HtmlBody = await _Response.GetHtmlBody();

                _Parser.LoadHtml(HtmlBody);
                var HtmlNodes = _Parser.GetNodes();

                var ModelCollection = new ObservableCollection<Model>();
                foreach (var node in HtmlNodes)
                {
                    ModelCollection.Add(_Parser.DataExtractor(node));
                }

                return ModelCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
