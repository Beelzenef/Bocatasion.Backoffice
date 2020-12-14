using Bocatasion.Backoffice.Models.Food;
using Bogus;
using System.Collections.Generic;
using System.Linq;

namespace Bocatasion.Backoffice.Testing.Fakes
{
    public static class FoodBuilder
    {
        public static SandwichModel BuildValidSandwichModel(int? id = null, string name = null)
        {
            var fake = new Faker<SandwichModel>()
            .CustomInstantiator(_ => new SandwichModel())
            .RuleFor(v => v.Name, f => name ?? f.Random.String2(10))
            .RuleFor(v => v.Description, f => f.Random.String2(20))
            .RuleFor(v => v.Price, f => f.Random.Double(1))
            .RuleFor(v => v.Id, f => id ?? f.Random.Int(1, 100))
            .RuleFor(v => v.Disabled, f => f.Random.Bool())
            .RuleFor(v => v.ImageUrl, f => f.Internet.Avatar());

            return fake.Generate();
        }

        public static SandwichCreatableDto BuildValidSandwichCreatableDto()
        {
            var fake = new Faker<SandwichCreatableDto>()
            .CustomInstantiator(_ => new SandwichCreatableDto())
            .RuleFor(v => v.Name, f => f.Random.String2(10))
            .RuleFor(v => v.Description, f => f.Random.String2(20))
            .RuleFor(v => v.Price, f => f.Random.Double(1))
            .RuleFor(v => v.Disabled, f => f.Random.Bool())
            .RuleFor(v => v.ImageUrl, f => f.Internet.Avatar());

            return fake.Generate();
        }

        public static List<SandwichModel> BuildValidSandwichModelList(int? count = 3)
        {
            Faker faker = new Faker();
            IList<SandwichModel> items = faker.Make(count.Value, () => BuildValidSandwichModel());
            return items.ToList();
        }
    }
}
