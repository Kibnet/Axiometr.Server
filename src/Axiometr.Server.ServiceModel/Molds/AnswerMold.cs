using ServiceStack.DataAnnotations;

namespace Axiometr.Server.ServiceModel.Molds
{
    [Description("Ответ")]
    public class AnswerMold
    {
        [Description("Пиктограмка")]
        public string Perk { get; set; }

        [Description("Текст ответа")]
        public string Description { get; set; }
    }
}