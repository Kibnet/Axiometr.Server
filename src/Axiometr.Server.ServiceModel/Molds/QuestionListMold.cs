using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Axiometr.Server.ServiceModel.Molds
{
    [Description("Результат получения списка вопросов")]
    public class QuestionListMold
    {
        [Description("Список вопросов")]
        public List<QuestionMold> Questions { get; set; }
    }
}