using Sat.Recruitment.Business.Abtractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sat.Recruitment.Business.Implementations
{
    public class Validations : IValidations
    {
        
        #region Constructors

        public Validations()
        {

        }

        #endregion

        public void ValidateErrors<T>(T entity, Dictionary<string, string> keyValuePairs, ref string errors)
        {
            List<string> list = new List<string>();
            foreach (PropertyInfo props in entity.GetType().GetProperties())
            {
                if (props.PropertyType.UnderlyingSystemType == typeof(string))
                    if (String.IsNullOrEmpty((string)props.GetValue(entity, null)))
                        list.Add(GetErrors(props, keyValuePairs));
            }
            
            errors = String.Join(" ,", list);
        }

        string GetErrors(PropertyInfo property, Dictionary<string, string> keyValuePairs)
            => keyValuePairs.FirstOrDefault(c => c.Key == property.Name).Value;
    }
}
