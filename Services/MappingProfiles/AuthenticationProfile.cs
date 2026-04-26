using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Address = Domain.Entities.IdentityModule.Address;

namespace Services.MappingProfiles
{
    public  class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
          CreateMap<Address,AddressDtos>().ReverseMap();
        }
    }
}
