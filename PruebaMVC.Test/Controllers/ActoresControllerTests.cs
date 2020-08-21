using AutoMapper;
using Common.Data;
using Common.Dtos;
using Common.Entities;
using Common.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Controllers.Test.ActoresController
{
    [TestClass()]
    public class ActoresControllerTests
    {
        private ApplicationDbContext ConstruirDbContext()
        {
            var dbContext = new ApplicationDbContext();
            return dbContext;
        }

        private ApplicationDbContext ConstruirDbContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        [TestMethod()]
        public void DevuelveUnaVista_IndexTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var databaseName = Guid.NewGuid().ToString();
            var mapperMock = new Mock<IMapper>();
            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();
            var contextoUno = ConstruirDbContext(databaseName);

            for (int i = 1; i < 4; i++)
            {
                contextoUno.Actores.Add(
                    new Actor() 
                    {
                        FechaNacimiento = new DateTime(2020, i, i),
                        Nombre = $"Nombre - {i}",
                        Foto= string.Empty,
                    });
            }
            contextoUno.SaveChanges();

            var contextoDos = ConstruirDbContext(databaseName);
            var actorController = new PruebaMVC.Controllers.ActoresController(contextoDos, 
                mapperMock.Object, almacenadorArchivosMock.Object);

            //Prueba -- Act (Actuar, Acción)
            var respuesta = actorController.Index();

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.Result, typeof(IActionResult));
        }

        [TestMethod()]
        public void DevuelveNotFoundPasarIDNoExistente_DetailsTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var idActor = 200000;
            var mapperMock = new Mock<IMapper>();
            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();
            //var contextMock = new Mock<ApplicationDbContext>();
            //contextMock.Setup(x => x.Actores.FirstOrDefault<Actor>(y => y.Id == idActor)).Returns(default(Actor));
            var context = ConstruirDbContext();

            var actoresController = new PruebaMVC.Controllers.ActoresController(
                context,
                mapperMock.Object,
                almacenadorArchivosMock.Object);

            //Prueba -- Act (Actuar, Acción)
            var respuesta = actoresController.Details(idActor);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            //Assert.IsInstanceOfType(respuesta.Result, typeof(IActionResult));
            Assert.IsInstanceOfType(respuesta.Result, typeof(NotFoundResult));
        }
        
        [TestMethod()]
        public void  DevuelveUnaVistaPasarIDExistente_DetailsTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var idActor = 1;
            var mapperMock = new Mock<IMapper>();
            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();
            var context = ConstruirDbContext();

            var actoresController = new PruebaMVC.Controllers.ActoresController(
                context,
                mapperMock.Object,
                almacenadorArchivosMock.Object);

            //Prueba -- Act (Actuar, Acción)
            var respuesta = actoresController.Details(idActor);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.Result, typeof(IActionResult));
        }

        [TestMethod()]
        public void RedireccionaOtroAccionPasarIDNulo_DetailsTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var mapperMock = new Mock<IMapper>();
            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();
            var context = ConstruirDbContext();

            var actoresController = new PruebaMVC.Controllers.ActoresController(
                context,
                mapperMock.Object,
                almacenadorArchivosMock.Object);

            //Prueba -- Act (Actuar, Acción)
            var respuesta = actoresController.Details(null);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.Result, typeof(RedirectToActionResult));
        }

        [TestMethod()]
        public void DevuelIActionAlCrearAutor_CreateTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var mapperMock = new Mock<IMapper>();
            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();
            var dataBaseName = Guid.NewGuid().ToString();
            var context = ConstruirDbContext(dataBaseName);
            var actorController = new PruebaMVC.Controllers.ActoresController(
                context,
                mapperMock.Object,
                almacenadorArchivosMock.Object);
            var actorCreacionDto = new ActorCreacionDto()
            {
                FechaNacimiento = DateTime.Now,
                Foto = null,
                Nombre = $"Nombre - {DateTime.Now.ToString()}",
            };

            //Prueba -- Act (Actuar, Acción)
            var respuesta = actorController.Create(actorCreacionDto);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.Result, typeof(IActionResult));
        }

        [TestMethod()]
        public void EditTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            Assert.Fail();
        }
    }
}