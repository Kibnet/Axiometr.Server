using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using Serilog;
using ServiceStack;
using Axiometr.Server.Domain;
using Axiometr.Server.ServiceModel;
using Axiometr.Server.ServiceModel.Molds;
using Microsoft.AspNetCore.WebUtilities;
using Raven.Client.Documents.Operations.Counters;

namespace Axiometr.Server.ServiceInterface
{
    public class PerksService : Service
    {
        public IAsyncDocumentSession RavenSession { get; set; }

        //[Authenticate]
        public async Task<PageProfileMold> Get(GetPageProfile request)
        {
            var pageProfileMold = new PageProfileMold { };
            var address = UTF32Encoding.UTF8.GetString(Base64UrlTextEncoder.Decode(request.PageAddress));
            pageProfileMold.Address = address;

            var query = RavenSession.Query<PageProfile>();

            //Выделяем возможные пути
            var pathes = new HashSet<string>();
            var sb = new StringBuilder();
            for (int i = 0; i < address.Length; i++)
            {
                var c = address[i];
                sb.Append(c);
                if (i>8)
                {
                    switch (c)
                    {
                        case '/':
                        {
                            pathes.Add(sb.ToString());
                            break;
                        }
                        default:
                            break;
                    }
                }
            }
            pathes.Add(sb.ToString());

            query = query.Where(e => e.Address.In(pathes));

            //Запрашиваем
            var profiles = await query.ToListAsync();

            var perkList = new List<string>{ };

            foreach (var profile in profiles)
            {
                var id = RavenSession.Advanced.GetDocumentId(profile);
                CountersDetail operationResult = RavenSession.Advanced.DocumentStore.Operations
                    .Send(new GetCountersOperation(id));
                perkList.AddRange(operationResult.Counters.Select(x=>$"{nameof(Perk)}/{x.CounterName}"));
            }

            var perks = await RavenSession.LoadAsync<Perk>(perkList);
            pageProfileMold.Perks = new List<PerkInProfileMold>();

            var mapper = new Mapper(new MapperConfiguration(expression =>
            {
                expression.CreateMap<Perk, PerkInProfileMold>();
            }));
            foreach (var perkPair in perks)
            {
                var perk = mapper.Map<Perk, PerkInProfileMold>(perkPair.Value);
                pageProfileMold.Perks.Add(perk);
            }

            return pageProfileMold;
        }

        public async Task<PerkListMold> Get(GetPerks request)
        {
            var perks = await RavenSession.Query<Perk>().ToListAsync();
            var result = new PerkListMold();
            var mapper = new Mapper(new MapperConfiguration(expression =>
            {
                expression.CreateMap<Perk, PerkMold>();
            }));
            result.Perks = new List<PerkMold>();
            foreach (var perk in perks)
            {
                var mapped = mapper.Map<Perk, PerkMold>(perk);
                result.Perks.Add(mapped);
            }
            return result;
        }

        public async Task Post(CreatePerks request)
        {

            var perks = new List<Perk>()
            {
                new Perk
                {
                    Id = "Perk/vegan",
                    Name = "Веганство",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/vegan.png",
                    KeyWords = new List<string>
                    {
                        "сыроед",
                        "веган",
                        "вегетариан",
                    }
                },
                new Perk
                {
                    Id = "Perk/eatmeet",
                    Name = "Мясоедство",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/eatmeet.png",
                    KeyWords = new List<string>
                    {
                        "мясо",
                        "мясоед",
                    }
                },
                new Perk
                {
                    Id = "Perk/antivaccination",
                    Name = "Против прививок",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/antivaccination.png",
                    KeyWords = new List<string>
                    {
                        "прививка",
                        "вакцина",
                        "грип",
                        "covid",
                        "ковид",
                    }
                },
                new Perk
                {
                    Id = "Perk/vaccination",
                    Name = "За прививки",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/vaccination.png",
                    KeyWords = new List<string>
                    {
                        "прививка",
                        "вакцина",
                        "грип",
                        "covid",
                        "ковид",
                    }
                },
                new Perk
                {
                    Id = "Perk/krymru",
                    Name = "Крым - РФ",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/krymru.png",
                    KeyWords = new List<string>
                    {
                        "крым",
                    }
                },
                new Perk
                {
                    Id = "Perk/krymua",
                    Name = "Крым - Украина",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/krymua.png",
                    KeyWords = new List<string>
                    {
                        "крым",
                    }
                },
                new Perk
                {
                    Id = "Perk/aborting",
                    Name = "За аборты",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/aborting.png",
                    KeyWords = new List<string>
                    {
                        "аборт",
                    }
                },
                new Perk
                {
                    Id = "Perk/antiaborting",
                    Name = "Против абортов",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/antiaborting.png",
                    KeyWords = new List<string>
                    {
                        "аборт",
                    }
                },
                new Perk
                {
                    Id = "Perk/traditionsex",
                    Name = "Традиционные половые отношения",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/traditionsex.png",
                    KeyWords = new List<string>
                    {
                        "семья",
                        "дети",
                        "ребенок",
                        "любов",
                    }
                },
                new Perk
                {
                    Id = "Perk/nontraditionsex",
                    Name = "Не традиционные половые отношения",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/nontraditionsex.png",
                    KeyWords = new List<string>
                    {
                        "геи",
                        "лесби",
                        "лгбт",
                        "транссексуал",
                        "прайд",
                        "любов",
                    }
                },
                new Perk
                {
                    Id = "Perk/filthylanguage",
                    Name = "Мат",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/filthylanguage.png",
                    KeyWords = new List<string>
                    {
                        "редиска",
                        "fuck",
                    }
                },
                new Perk
                {
                    Id = "Perk/adult",
                    Name = "18+",
                    ImagePath = "https://github.com/Kibnet/Axiometr.Server/raw/master/resources/adult.png",
                    KeyWords = new List<string>
                    {
                        "порно",
                        "секс",
                        "убить",
                        "кровь",
                    }
                }
            };

            foreach (var perk in perks)
            {
                await RavenSession.StoreAsync(perk);
            }

            var medusa = new PageProfile
            {
                Address = "https://meduza.io/"
            };
            var wonderzine = new PageProfile
            {
                Address = "https://www.wonderzine.com/"
            };
            var profiles = new List<PageProfile>
            {
                medusa,
                wonderzine,
            };

            foreach (var profile in profiles)
            {
                await RavenSession.StoreAsync(profile);
            }

            await RavenSession.SaveChangesAsync();

            DocumentCountersOperation operation1 = new DocumentCountersOperation
            {
                DocumentId = RavenSession.Advanced.GetDocumentId(medusa),
                Operations = new List<CounterOperation>
                {
                    new CounterOperation()
                    {
                        CounterName = "krymua",
                        Delta = 10,
                        Type = CounterOperationType.Increment,
                    },
                }
            };

            CounterBatch counterBatch = new CounterBatch();
            counterBatch.Documents.Add(operation1);
            var counteBatch = new CounterBatchOperation(counterBatch);

            RavenSession.Advanced.DocumentStore.Operations.Send(counteBatch);

        }
    }
}