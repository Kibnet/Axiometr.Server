using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace Axiometr.Server.ServiceModel.Molds
{
    [Description("Результат получения списка пиктограм")]
    public class PerkListMold
    {
        [Description("Список пиктограм")]
        public List<PerkMold> Perks { get; set; }
    }
}