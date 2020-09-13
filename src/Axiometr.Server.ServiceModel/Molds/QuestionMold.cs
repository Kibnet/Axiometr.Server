using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Axiometr.Server.ServiceModel.Molds
{
    [Description("Вопрос")]
    public class QuestionMold
    {
        [Description("Описание вопроса")]
        public string Description { get; set; }

        [Description("Варианты ответа")]
        public List<AnswerMold> Answers { get; set; }
    }
}