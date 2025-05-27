using BeYou.Domain.Core.Models;

namespace BeYou.Infraestructure.Mappings.ValueResolvers;

public class CurrentUserIdResolverAdd(IServiceUserContext serviceUserContext) : IValueResolver<RequestBaseDto, BaseEntity, string>
{
    public string Resolve(RequestBaseDto source, BaseEntity destination, string destMember, ResolutionContext context) =>
        serviceUserContext.UserId!;
}
