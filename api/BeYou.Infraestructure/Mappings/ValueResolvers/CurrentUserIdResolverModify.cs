using BeYou.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeYou.Infraestructure.Mappings.ValueResolvers;
public class CurrentUserIdResolverModify(IServiceUserContext serviceUserContext) : IValueResolver<RequestBaseDto, BaseEntity, string?>
{
    public string? Resolve(RequestBaseDto source, BaseEntity destination, string? destMember, ResolutionContext context) =>
        serviceUserContext.UserId!;
}