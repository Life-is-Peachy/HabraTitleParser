using AngleSharp.Parser.Html;
using System;
using System.Net;


namespace Parser.Core
{
    class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        IParserSettings parserSettings;

        HtmlLoader loader;

        bool isActive;

        #region Свойства

        public IParser<T> Parser { get; set; }
        public IParserSettings Settings
        {
            get
            {
                return parserSettings;
            }
            set
            {
                parserSettings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive { get; }

        #endregion
        // Возрващаем данные за новую итерацию
        public event Action<object, T> OnNewData;

        // Уведомляем о завершении
        public event Action<object> OnCompleted;

        public ParserWorker(IParser<T> parser)
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings) : this(parser)
        {
            this.parserSettings = parserSettings;
        }

        // Запускаем
        public void Start()
        {
            isActive = true;
            Worker();
        }
        // Останавливаем
        public void Abort()
        {
            isActive = false;
        }

        // Парсируем
        private async void Worker()
        {
            // Устанавливаем протокол подключения
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            // Проходимся по страницам
            for (int i = parserSettings.StartPoint; i <= parserSettings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                // Получаем исходный код
                var source = await loader.GetSourceByPageId(i);

                var domParser = new HtmlParser();

                // Получаем документ
                var document = await domParser.ParseAsync(source);

                // Получаем данные
                var result = parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
            isActive = false;
        }
    }
}
