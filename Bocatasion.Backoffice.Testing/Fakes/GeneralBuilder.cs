using Bogus;
using System.Diagnostics.CodeAnalysis;

namespace Bocatasion.Backoffice.Testing.Fakes
{
    [ExcludeFromCodeCoverage]
    public static class GeneralBuilder
    {
        public static int BuildRandomInt()
        {
            Faker faker = new Faker();
            int number = faker.Random.Int(1, 100);
            return number;
        }
    }
}
