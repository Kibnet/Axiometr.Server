using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Axiometr.Server.ServiceModel.Molds
{
    [Description("Профиль страницы")]
    public class PageProfileMold
    {
        [Description("Адрес страницы")]
        public string Address { get; set; }

        [Description("Пиктограмки")]
        public List<PerkInProfileMold> Perks { get; set; }
    }
}