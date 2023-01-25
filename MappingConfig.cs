using AutoMapper;
using MagicVilla_VillaApi.Models;
using MagicVilla_VillaApi.Models.DTO;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaApi;

public class MappingConfig: Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaDTO>();
        CreateMap<VillaDTO, Villa>();

        CreateMap<VillaDTO, VillaUpdateDTO>();
        CreateMap<VillaUpdateDTO, VillaDTO>();

    }
}