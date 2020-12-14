using Bogus;

namespace Bocatasion.Backoffice.Testing.Fakes
{
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
