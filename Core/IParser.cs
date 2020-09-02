using AngleSharp.Dom.Html;

namespace Parser.Core
{
    // Классы реализации будут возвращать данные любого типа
    interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
