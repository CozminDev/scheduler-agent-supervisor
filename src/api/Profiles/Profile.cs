using AutoMapper;

public class MappingProfile : Profile 
{
   public MappingProfile()
   {
      CreateMap<Models.Job, Entities.Job>();
   }
}