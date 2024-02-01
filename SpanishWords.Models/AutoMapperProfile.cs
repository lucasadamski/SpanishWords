using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SpanishWords.Models
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Statistic, StatisticDTO>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                .ForMember(dest => dest.LastUpdated, opt => opt.MapFrom(src => src.LastUpdated))
                .ForMember(dest => dest.DeleteTime, opt => opt.MapFrom(src => src.DeleteTime))
                .ForMember(dest => dest.CorrectAnswersToLearn, opt => opt.MapFrom(src => src.CorrectAnswersToLearn));

            CreateMap<Word, WordDTO>()
                .ForMember(dest => dest.Spanish, opt => opt.MapFrom(src => src.Spanish))
                .ForMember(dest => dest.English, opt => opt.MapFrom(src => src.English))
                .ForMember(dest => dest.LexicalCategoryId, opt => opt.MapFrom(src => src.LexicalCategoryId))
                .ForMember(dest => dest.GrammaticalGenderId, opt => opt.MapFrom(src => src.GrammaticalGenderId))
                .ForMember(dest => dest.Statistic, opt => opt.MapFrom(src => src.Statistic));
        }
    }
}
