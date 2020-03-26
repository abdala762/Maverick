using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace Maverick.Application.ApiTest
{
    public static class TesteUtil
    {
        public static void VerificarClassesIguais(Type primeiroTipo, Type segundoTipo)
        {
            IList<PropertyInfo> propsPrimeiro = new List<PropertyInfo>(primeiroTipo.GetProperties());
            IList<PropertyInfo> propsSegundo = new List<PropertyInfo>(segundoTipo.GetProperties());
            foreach (PropertyInfo propPrimeiro in propsPrimeiro)
            {
                var valido = false;
                foreach (PropertyInfo propSegundo in propsSegundo)
                {
                    if (propSegundo.Name.Equals(propPrimeiro.Name) && propSegundo.PropertyType.Equals(propPrimeiro.PropertyType))
                    {
                        valido = true;
                    }
                }
                Assert.True(valido);
            }
        }
    }
}
