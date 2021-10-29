namespace Faker
{
    public interface IValueGenerator
    {
        object Generate(GeneratorContext context);
    }
}
