using System.Collections.Generic;

namespace Axiometr.Server.Domain
{
    /// <summary>
    /// Вопрос
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Описание вопроса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Варианты ответа
        /// </summary>
        public List<Answer> Answers { get; set; }
    }
}