using System.Linq;

namespace Shop.Domain.NHibernate.Dto
{
    public class DtoMapper<T> where T : new()
    {
        private T mapTo;

        public T MapFrom<TP>(TP mapFrom)
        {
            mapTo = new T();
            var toFields = typeof(T).GetProperties();
            var fromFields = typeof(TP).GetProperties();

            foreach (var toField in toFields)
            {
                var fromField = fromFields.FirstOrDefault(f => f.Name == toField.Name);

                if (fromField == null 
                    || fromField.PropertyType.Name.StartsWith("List")
                    || fromField.PropertyType.Name.StartsWith("IList"))
                {
                    continue;
                }

                var value = fromField.GetValue(mapFrom, new object[0]);
                
                toField.SetValue(mapTo, value, new object[0]);
            }

            return mapTo;
        }
    }
}
