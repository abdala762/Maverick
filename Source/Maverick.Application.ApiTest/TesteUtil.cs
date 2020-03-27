using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Xunit;

namespace Maverick.Application.ApiTest
{
    public static class TesteUtil
    {
        public static void VerificarObjetosIguais(object objetoEsperado, object objetoRetornado)
        {

            IList<PropertyInfo> propsEsperado = new List<PropertyInfo>(objetoEsperado.GetType().GetProperties());
            IList<PropertyInfo> propsRetornado = new List<PropertyInfo>(objetoRetornado.GetType().GetProperties());

            Assert.Equal(propsEsperado.Count,propsRetornado.Count);

            foreach (PropertyInfo propEsperado in propsEsperado)
            {
                var valido = propsRetornado.Any(x => x.Name.Equals(propEsperado.Name) &&
                                                x.PropertyType.Equals(propEsperado.PropertyType) &&
                                                x.GetValue(objetoRetornado).Equals(propEsperado.GetValue(objetoEsperado)));

                Assert.True(valido);
            }
        }

        public static void VerificarListasClassesIguais<TEsperado,TRetornado>(List<TEsperado> listaEsperada, List<TRetornado> listaRetornada)
        {
            Assert.Equal(listaEsperada.Count, listaRetornada.Count);

            var typeEsperado = listaEsperada.FirstOrDefault().GetType().GetProperties();
            var typeRetornado= listaRetornada.FirstOrDefault().GetType().GetProperties();

            var typeEsperadoOrdenado=typeEsperado.OrderBy(x => x.Name);
            var typeRetornadoOrdenado = typeRetornado.OrderBy(x => x.Name);



            var ordenacaoEsperada = listaEsperada.OrderBy(x => typeEsperadoOrdenado.FirstOrDefault().GetValue(x ,null));

            foreach (PropertyInfo propPrimeiro in typeEsperadoOrdenado)
            {
                ordenacaoEsperada = ordenacaoEsperada.ThenBy(x => propPrimeiro.GetValue(x, null));
            }

            var ordenacaoRetornada = listaRetornada.OrderBy(x => typeRetornadoOrdenado.FirstOrDefault().GetValue(x, null));

            foreach (PropertyInfo propRetornado in typeRetornadoOrdenado)
            {
                ordenacaoRetornada = ordenacaoRetornada.ThenBy(x => propRetornado.GetValue(x, null));
            }


            for (int i = 0; i < ordenacaoEsperada.Count(); i++)
            {
                VerificarObjetosIguais(ordenacaoEsperada.ElementAt(i), ordenacaoRetornada.ElementAt(i));
            }
        }
    }
}
