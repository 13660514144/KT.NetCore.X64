using ProwatchAPICS;
using KT.Prowatch.Service.DllModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.Helpers
{
    public static class ModelConvert
    {
        public static AccessCodeData ToModel(this sPA_AccessCode source)
        {
            return new AccessCodeData(source);
        }
        public static List<AccessCodeData> ToModels(this List<sPA_AccessCode> sources)
        {
            var results = new List<AccessCodeData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new AccessCodeData(item));
            }
            return results;
        }


        public static AccessDurationData ToModel(this sPA_Access_Duration source)
        {
            return new AccessDurationData(source);
        }
        public static List<AccessDurationData> ToModels(this List<sPA_Access_Duration> sources)
        {
            var results = new List<AccessDurationData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new AccessDurationData(item));
            }
            return results;
        }


        public static AreaData ToModel(this sPA_Area source)
        {
            return new AreaData(source);
        }
        public static List<AreaData> ToModels(this List<sPA_Area> sources)
        {
            var results = new List<AreaData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new AreaData(item));
            }
            return results;
        }


        public static BadgeTypeData ToModel(this sPA_BadgeType source)
        {
            return new BadgeTypeData(source);
        }
        public static List<BadgeTypeData> ToModels(this List<sPA_BadgeType> sources)
        {
            var results = new List<BadgeTypeData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new BadgeTypeData(item));
            }
            return results;
        }


        public static CardData ToModel(this sPA_Card source)
        {
            return new CardData(source);
        }
        public static List<CardData> ToModels(this List<sPA_Card> sources)
        {
            var results = new List<CardData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new CardData(item));
            }
            return results;
        }


        public static CompanyData ToModel(this sPA_Company source)
        {
            return new CompanyData(source);
        }
        public static List<CompanyData> ToModels(this List<sPA_Company> sources)
        {
            var results = new List<CompanyData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new CompanyData(item));
            }
            return results;
        }


        public static EventData ToModel(this sPA_Event source)
        {
            return new EventData(source);
        }
        public static List<EventData> ToModels(this List<sPA_Event> sources)
        {
            var results = new List<EventData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new EventData(item));
            }
            return results;
        }


        public static IdData ToModel(this sPA_ID source)
        {
            return new IdData(source);
        }
        public static List<IdData> ToModels(this List<sPA_ID> sources)
        {
            var results = new List<IdData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new IdData(item));
            }
            return results;
        }


        public static PersonData ToModel(this sPA_Person source)
        {
            return new PersonData(source);
        }
        public static List<PersonData> ToModels(this List<sPA_Person> sources)
        {
            var results = new List<PersonData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new PersonData(item));
            }
            return results;
        }


        public static PersonDetailData ToModel(this sPA_PersonDetail source)
        {
            return new PersonDetailData(source);
        }
        public static List<PersonDetailData> ToModels(this List<sPA_PersonDetail> sources)
        {
            var results = new List<PersonDetailData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new PersonDetailData(item));
            }
            return results;
        }


        public static ReaderData ToModel(this sPA_Reader source)
        {
            return new ReaderData(source);
        }
        public static List<ReaderData> ToModels(this List<sPA_Reader> sources)
        {
            var results = new List<ReaderData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new ReaderData(item));
            }
            return results;
        }


        public static TimeZoneData ToModel(this sPA_TimeZone source)
        {
            return new TimeZoneData(source);
        }
        public static List<TimeZoneData> ToModels(this List<sPA_TimeZone> sources)
        {
            var results = new List<TimeZoneData>();
            if (sources == null || sources.FirstOrDefault() == null)
            {
                return results;
            }
            foreach (var item in sources)
            {
                results.Add(new TimeZoneData(item));
            }
            return results;
        }
    }
}
