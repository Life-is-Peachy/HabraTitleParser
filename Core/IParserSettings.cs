namespace Parser.Core
{
    // Настройки парсера
    interface IParserSettings
    {
        // Ресурс
        string BaseUrl { get; set; }

        // Добавочный
        string Prefix { get; set; }

        // С какой страницы
        int StartPoint { get; set; }

        // До какой
        int EndPoint { get; set; }
    }
}
