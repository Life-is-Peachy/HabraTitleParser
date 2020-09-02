using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom.Html;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Parser.Core.Habra
{
    class HabraParser : IParser<string[]>
    {
        //Принимает разметку. Из неё будем вытаскивать
        public string[] Parse(IHtmlDocument document)
        {
            // Все тайтлы будем опрокидывать сюда
            var list  = new List<string>();

            // Все тайтлы помечены классом post__title_link. Их и будем вытаскивать
            var items = document.QuerySelectorAll("a").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));

            // Куда сохраняем json. По умолчанию в папку проекта
            string location = "file.json";

            // Опрокидываем
            foreach(var item in items)
            {
                list.Add(item.TextContent);
            }

            WriteJson(list, location);

            // Возвращаем массивом
            return list.ToArray();
        }

        // Генерируем JSon. 
        private void WriteJson(IEnumerable<string> list, string location)
        {
            JObject json = new JObject(new JProperty("title", list));
            if (!File.Exists(location))
                File.WriteAllText(location, json.ToString());
            else
                File.AppendAllText(location, json.ToString());
        }
    }
}
