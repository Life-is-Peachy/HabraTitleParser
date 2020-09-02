namespace Parser.Core.Habra
{
    class HabraSettings : IParserSettings
    {
        public string BaseUrl { get; set; } = "https://habr.com/ru/all/";

        public string Prefix { get; set; } = "page{CurrentId}";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }

        // Экземпляру будем передавать от какой и до какой страницы выбирать заголовки
        public HabraSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }
    }
}
