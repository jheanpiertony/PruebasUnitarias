using Common.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaAPI.Controllers.Tests
{
    [TestClass()]
    public class ActoresControllerTests
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Inicializador()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod()]
        public async Task DevuelveListadoActoresDeTresActores_GetActoresAsyncTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var url = "api/Actores";
            var cliente = _factory.CreateClient();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = await cliente.GetAsync(url);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            if (!respuesta.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Codigo de estatus no exitoso: " + respuesta.StatusCode);
            }

            var resultado =  JsonConvert
                .DeserializeObject<List<Actor>>(await respuesta.Content.ReadAsStringAsync());


            Assert.AreEqual(expected: 3, actual: resultado.Count);
        }

        [TestMethod()]
        public async Task DevuelveListadoActores_GetActoresAsyncTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var url = "api/Actores";
            var cliente = _factory.CreateClient();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = await cliente.GetAsync(url);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            if (!respuesta.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, $"Codigo de estatus no exitoso: {respuesta.StatusCode}");
            }

            var resultado = JsonConvert
                .DeserializeObject<List<Actor>>(await respuesta.Content.ReadAsStringAsync());


            Assert.AreEqual("Arnold Alois Schwarzenegger",resultado[0].Nombre);
        }
    }
}