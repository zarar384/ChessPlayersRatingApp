using AutoMapper;
using ChessPlayersRatingApp.Models.DTO;

namespace ChessPlayersRatingApp.Models.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Player, PlayerDto>();
            this.CreateMap<PlayerDto, Player>();
            this.CreateMap<Information, InformationDto>();
            this.CreateMap<InformationDto, Information>();
        }
    }
}
