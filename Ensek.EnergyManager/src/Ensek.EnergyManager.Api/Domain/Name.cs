namespace Ensek.EnergyManager.Api.Domain;

internal record Name(string FirstName, string Surname)
{
    public override string ToString()
    {
        return $"{FirstName} {Surname}";
    }
}
