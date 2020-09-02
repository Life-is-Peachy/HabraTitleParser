using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parser.Core
{
    // Загружаем разметку из указанных настроек парсера
    class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;

        public HtmlLoader(IParserSettings settings)
        {
            client = new HttpClient();
            url = $"{settings.BaseUrl}{settings.Prefix}/";
        }

        // Получаем код страницы
        public async Task<string> GetSourceByPageId(int id)
        {
            // Редактируем URL для запроса
            var currentUrl = url.Replace("{CurrentId}", id.ToString());

            // Получаем ответ от сервера
            var response = await client.GetAsync(currentUrl);

            // Сюда скопируем исходный код страницы
            string source = null;

            //Проверяем подключение. Копируем исходный код страницы
            if(response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
