using AutoMapper;
using Racing.Models;
using Racing.WebApi.REST_Models;
using Racing.WebApi.RESTModels;
using System.Security.AccessControl;

namespace Racing.WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FormulaPost, Formula>();
            CreateMap<Formula, FormulaGet>();
            CreateMap<FormulaPut, Formula>();
        }
    }
}
