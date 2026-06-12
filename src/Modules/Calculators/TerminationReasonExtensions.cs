using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MeuValorLiquido.Modules.Calculators;

public static class TerminationReasonExtensions
{
    public static string GetDisplayName(this TerminationReason reason)
    {
        var member = typeof(TerminationReason).GetMember(reason.ToString()).FirstOrDefault();
        var display = member?.GetCustomAttribute<DisplayAttribute>();
        return display?.Name ?? reason.ToString();
    }
}
