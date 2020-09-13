using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Axiometr.Server.ServiceModel.Molds
{
    [Description("Пиктограмка в профиле")]
    public class PerkInProfileMold
    {
        [Description("Название")]
        public string Id { get; set; }

        [Description("Текст подсказки")]
        public string Name { get; set; }

        [Description("Иконка")]
        public string ImagePath { get; set; }
    }

    [Description("Пиктограмка")]
    public class PerkMold
    {
        [Description("Название")]
        public string Id { get; set; }

        [Description("Текст подсказки")]
        public string Name { get; set; }

        [Description("Иконка")]
        public string ImagePath { get; set; }

        [Description("Ключевые слова")]
        public List<string> KeyWords { get; set; }
    }
}