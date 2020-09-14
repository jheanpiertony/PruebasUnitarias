using AutoMapper;
using Common.Dtos;
using Common.Servicios;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaAPI.Controllers.Tests
{
    [TestClass()]
    public class ActoresDtoControllerTests
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Inicializador()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        public WebApplicationFactory<Startup> WebApplicationFactoryConfigurado()
        {
            return _factory.WithWebHostBuilder(builder => 
            {
                builder.ConfigureServices(servicios =>
                {
                    servicios.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
                    servicios.AddAutoMapper(typeof(Common.Helpers.AutoMapperProfiles));
                });
            });
        }

        //public WebApplicationFactory<Startup> WebApplicationFactoryConfigurado()
        //{
        //    return _factory.WithWebHostBuilder(builder =>
        //    {
        //        builder.ConfigureServices(servicios =>
        //        {
        //            servicios.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
        //            servicios.AddAutoMapper(typeof(Common.Helpers.AutoMapperProfiles));
        //            servicios.AddSingleton<IAuthorizationHandler, SaltarRequerimientosJWT>();
        //        });
        //    });
        //}


        [TestMethod()]
        public async Task DevuelveListadoActoresDeTresActores_GetActoresAsyncTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var url = "api/ActoresDto";
            var cliente = WebApplicationFactoryConfigurado().CreateClient();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = await cliente.GetAsync(url);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            if (!respuesta.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Codigo de estatus no exitoso: " + respuesta.StatusCode);
            }

            var resultado = JsonConvert
                .DeserializeObject<List<ActorDto>>(await respuesta.Content.ReadAsStringAsync());


            Assert.AreEqual(expected: 3, actual: resultado.Count);
        }
        [TestMethod()]
        public async Task DevuelveListadoActores_GetActoresAsyncTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var url = "api/ActoresDto";
            var cliente = WebApplicationFactoryConfigurado().CreateClient();


            //Prueba -- Act (Actuar, Acción)
            var respuesta = await cliente.GetAsync(url);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            if (!respuesta.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Codigo de estatus no exitoso: " + respuesta.StatusCode);
            }

            var resultado = JsonConvert
                .DeserializeObject<List<ActorDto>>(await respuesta.Content.ReadAsStringAsync());


            Assert.AreEqual("Arnold Alois Schwarzenegger", resultado[0].Nombre);
        }
        [TestMethod()]
        public async Task DevuelveDeTipoListadoActoresDto_GetActoresAsyncTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var url = "api/Actores";
            var cliente = WebApplicationFactoryConfigurado().CreateClient();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = await cliente.GetAsync(url);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            if (!respuesta.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Codigo de estatus no exitoso: " + respuesta.StatusCode);
            }

            try
            {
                var resultado = JsonConvert
                .DeserializeObject<List<ActorDto>>(await respuesta.Content.ReadAsStringAsync());
                Assert.IsTrue(true, $"Es un Listado de DTO");
            }
            catch (Exception)
            {

                Assert.Fail("No se puede hacer Cast a Listado de Actores Dto");
            }
        }

        [TestMethod()]
        public async Task Devuelve404_PorIdValor0_GetActoresAsyncTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var url = "api/ActoresDto/0";
            var cliente = WebApplicationFactoryConfigurado().CreateClient();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = await cliente.GetAsync(url);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.AreEqual(expected: 404, actual: (int)respuesta.StatusCode);
        }

        [TestMethod()]
        public async Task DevuelveActorDto_SiAutorExiste_GetActoresAsyncTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var url = "api/ActoresDto/1";
            var cliente = WebApplicationFactoryConfigurado().CreateClient();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = await cliente.GetAsync(url);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            if (!respuesta.IsSuccessStatusCode)
            {
                Assert.IsTrue(false, "Codigo de estatus no exitoso: " + respuesta.StatusCode);
            }

            var resultado = JsonConvert
                .DeserializeObject<ActorDto>(await respuesta.Content.ReadAsStringAsync());

            Assert.IsNotNull(resultado);
            Assert.AreEqual(expected: 1, actual: resultado.Id);
            Assert.AreEqual("Arnold Alois Schwarzenegger", resultado.Nombre);
        }
    }
}