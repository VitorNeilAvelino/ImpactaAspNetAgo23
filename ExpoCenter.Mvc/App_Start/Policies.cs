using Microsoft.AspNetCore.Authorization;

namespace ExpoCenter.Mvc.App_Start
{
    public class Policies
    {
        internal static void ParticipantesExcluirPolicy(AuthorizationPolicyBuilder builder)
        {
            builder.RequireAssertion(p => p.User.IsInRole("Gerente") || 
                p.User.HasClaim("Participantes", "Excluir"));
        }
    }
}
