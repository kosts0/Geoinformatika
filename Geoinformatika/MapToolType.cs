
namespace Geoinformatika
{
    /// <summary>
    /// Действия на меню (Center нет т.к. он не предполагает инициализации действий)
    /// </summary>
    public enum MapToolType
    {
        /// <summary>
        /// Ладошка
        /// </summary>
        Pen,
        /// <summary>
        /// Увеличить
        /// </summary>
        ZoomIn,
        /// <summary>
        /// Уменьшить
        /// </summary>
        ZoomOut,
        /// <summary>
        /// Получить значение
        /// </summary>
        GetValue,
        /// <summary>
        /// Выбрать объект
        /// </summary>
        SelectObject,
        /// <summary>
        /// Линейка
        /// </summary>
        Ruler
    }
}
