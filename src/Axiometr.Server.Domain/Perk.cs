using System;
using System.Collections.Generic;

namespace Axiometr.Server.Domain
{
    /// <summary>
    /// Пиктограмка
    /// </summary>
    public class Perk
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Текст подсказки
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Иконка
        /// </summary>
        public string ImagePath { get; set; }
        
        /// <summary>
        /// Ключевые слова
        /// </summary>
        public List<string> KeyWords { get; set; }
    }
}