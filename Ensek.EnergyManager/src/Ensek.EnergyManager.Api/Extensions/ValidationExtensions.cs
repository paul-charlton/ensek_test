using System.ComponentModel.DataAnnotations;

namespace Ensek.EnergyManager.Api;

internal static class ValidationExtensions
{

    public static bool TryValidate(this IValidatableObject source, object context, out string? error)
    {
        error = null;
        var validation = source.Validate(new ValidationContext(context));
        if (validation == null || !validation.Any())
            return true;

        error = string.Join("; ", validation);
        return false;
    }
}
