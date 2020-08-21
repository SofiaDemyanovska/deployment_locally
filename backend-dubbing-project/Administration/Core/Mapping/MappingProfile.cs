using AutoMapper;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.DTOs;
using SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Entities;

namespace SoftServe.ITAcademy.BackendDubbingProject.Administration.Core.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Performance, PerformanceDTO>();
            CreateMap<PerformanceDTO, Performance>();

            CreateMap<Speech, SpeechDTO>();
            CreateMap<SpeechDTO, Speech>();

            CreateMap<Language, LanguageDTO>();
            CreateMap<LanguageDTO, Language>();

            CreateMap<Audio, AudioFileDTO>();
            CreateMap<AudioFileDTO, Audio>()
                .ForMember("FileName", cnf => cnf.MapFrom(m => m.File.FileName));

            CreateMap<Audio, AudioDTO>();
            CreateMap<AudioDTO, Audio>();
        }
    }
}