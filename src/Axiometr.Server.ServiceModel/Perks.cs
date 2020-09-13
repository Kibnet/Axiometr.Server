using System;
using System.Net;
using System.Text;
using Axiometr.Server.ServiceModel.Molds;
using ServiceStack;

namespace Axiometr.Server.ServiceModel
{
    [Api("Perk")]
    [ApiResponse(HttpStatusCode.BadRequest, "Неверно составлен запрос", ResponseType = typeof(void))]
    [Route("/pageprofile", "GET", Summary = "Получение профиля страницы", Notes = "Получение профиля страницы")]
    public class GetPageProfile : IReturn<PageProfileMold>
    {
        [ApiMember(IsRequired = true, Description = "Адрес страницы")]
        public string PageAddress { get; set; }
    }
    
   [Api("Perk")]
    [ApiResponse(HttpStatusCode.BadRequest, "Неверно составлен запрос", ResponseType = typeof(void))]
    [Route("/perks", "GET", Summary = "Получение списка пиктограмм", Notes = "Получение списка пиктограмм")]
    public class GetPerks : IReturn<PerkListMold> { }

    [Api("Perk")]
    [ApiResponse(HttpStatusCode.BadRequest, "Неверно составлен запрос", ResponseType = typeof(void))]
    [Route("/perks", "POST", Summary = "Создание списка пиктограмм", Notes = "Создание списка пиктограмм")]
    public class CreatePerks : IReturnVoid { }

    [Api("Question")]
    [ApiResponse(HttpStatusCode.BadRequest, "Неверно составлен запрос", ResponseType = typeof(void))]
    [Route("/question", "GET", Summary = "Получение вопросов", Notes = "Получение вопросов")]
    public class GetQuestions : IReturn<QuestionListMold> { }


    //	Ответ на вопрос
    //		Входные параметры
    //			Адрес страницы
    //			Id вопроса
    //			Id ответов[]
    //		Ответ
    //			200
    //				Результат
    //			401
    //				Ещё не авторизован
}
