using System;

namespace Faker
{
    public interface IFaker
    {
         IValueGenerator FindGeneratorByType(Type type);
        object Create(Type type); 
    }
}
