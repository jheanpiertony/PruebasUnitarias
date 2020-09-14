using AutoMapper;
using Common.Data;
using Common.Dtos;
using Common.Entities;
using Common.Servicios;
using Common.Test.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Controllers.Test.ActoresController
{
    [TestClass()]
    public class ActoresControllerTests
    {
        private PruebaMVC.Controllers.ActoresController _actorController;
        private string _databaseName = Guid.NewGuid().ToString();

        private ApplicationDbContext ConstruirDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(@"Data Source = DESARROLLO-33\SQLEXPRESS; Initial Catalog = PruebaUnitariasDB; Integrated Security = True;")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
        private ApplicationDbContext ConstruirDbContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }

        [TestInitialize]
        public void Inicializar() 
        {
            var mapperMock = new Mock<IMapper>();
            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();            
            var contextoDos = ConstruirDbContext(_databaseName);
            _actorController = new PruebaMVC.Controllers.ActoresController(contextoDos,
                mapperMock.Object, almacenadorArchivosMock.Object);
        }       

        [TestMethod()]
        public void DevuelveUnaVista_IndexTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var databaseName = Guid.NewGuid().ToString();
            var listaActores = new List<Actor>();            
            for (int i = 1; i < 4; i++)
            {
                listaActores.Add(
                    new Actor()
                    {
                        Id= i,
                        FechaNacimiento = new DateTime(2020, i, i),
                        Nombre = $"Nombre - {i}",
                        Foto = string.Empty,
                    });
            }

            var listaActoresDtos = new List<ActorDto>();

            foreach (var item in listaActores)
            {
                listaActoresDtos.Add(new ActorDto() 
                {
                    FechaNacimiento = item.FechaNacimiento,
                    Foto= item.Foto,
                    Id= item.Id,
                    Nombre= $"Dtos -{item.Nombre}",                    
                });
            }

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<List<ActorDto>>(It.IsAny<List<Actor>>()))
                .Returns(listaActoresDtos);

            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();
            var contextoUno = ConstruirDbContext(databaseName);
            contextoUno.AddRange(listaActores);
            
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
            var actor = new Actor() 
            {
                FechaNacimiento= DateTime.Now,
                Foto= string.Empty,
                Id= 1,
                Nombre= "Nombre",
            };

            var contextoDos = ConstruirDbContext(_databaseName);
            contextoDos.Add(actor);
            contextoDos.SaveChanges();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = _actorController.Details(1000);

            Assert.IsInstanceOfType(respuesta.Result, typeof(NotFoundResult));
        }
        [TestMethod()]
        public void DevuelveViewIDExistente_DetailsTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var actor = new Actor()
            {
                FechaNacimiento = DateTime.Now,
                Foto = string.Empty,
                Id = 1,
                Nombre = "Nombre",
            };

            var contextoDos = ConstruirDbContext(_databaseName);
            contextoDos.Add(actor);
            contextoDos.SaveChanges();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = _actorController.Details(actor.Id);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.Result, typeof(ViewResult));
        }
        [TestMethod()]
        public void DevuelveMismoActorDTOIDExistente_DetailsTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var actor = new Actor()
            {
                FechaNacimiento = DateTime.Now,
                Foto = string.Empty,
                Id = 1,
                Nombre = "Nombre",
            };

            var actorDto = new ActorDto() 
            {
                FechaNacimiento = actor.FechaNacimiento,
                Foto = actor.Foto,
                Id = actor.Id,
                Nombre = actor.Nombre,
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<ActorDto>(It.IsAny<Actor>()))
                .Returns(actorDto);

            var contextoDos = ConstruirDbContext(_databaseName);
            contextoDos.Add(actor);
            contextoDos.SaveChanges();
            _actorController.Mapper = mapperMock.Object;

            //Prueba -- Act (Actuar, Acción)
            var respuesta = _actorController.Details(actor.Id);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            var respuestaResult = respuesta.Result as ViewResult;
            var respuestActorDto = (ActorDto)respuestaResult.ViewData.Model;
            Assert.AreEqual(actorDto, respuestActorDto);
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
        public void DevuelRedirectToActionAlCrearAutor_CreateTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var physicalFile = new FileInfo(@"D:\david.restrepo\Pictures\AngelinaJolie.jpg");
            IFormFile formFile = physicalFile.AsMockIFormFile();
            var ms = new MemoryStream();
            formFile.CopyTo(ms);
            var contenido = ms.ToArray();

            var actorCreacionDto = new ActorCreacionDto()
            {
                FechaNacimiento = DateTime.Now,
                Foto = formFile,
                Nombre = $"Nombre - {DateTime.Now.ToString()}",
            };

            var actor = new Actor()
            {
                FechaNacimiento = actorCreacionDto.FechaNacimiento,
                Foto = string.Empty,
                Nombre = actorCreacionDto.Nombre,
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<Actor>(actorCreacionDto)).Returns(actor);
            
            var almacenadorArchivosMock = new Mock<IAlmacenadorArchivos>();
            almacenadorArchivosMock.Setup(x => x.GuardarArchivo(contenido,
                formFile.ContentType, "actores", formFile.ContentType))
                .ReturnsAsync("https://localhost:44303/actores/db4b6154-07d3-4e37-b440-36a19f5d9d0e.jpg");

            _actorController.Mapper = mapperMock.Object;
            _actorController.AlmacenadorArchivos = almacenadorArchivosMock.Object;

            //Prueba -- Act (Actuar, Acción)
            var respuesta = _actorController.Create(actorCreacionDto);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.Result, typeof(RedirectToActionResult));
        }
        [TestMethod]
        public void DevuelVistaActionAlCrearAutorConModeloInvalido_CreateTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var actorCreacionDto = new ActorCreacionDto();

            _actorController.ModelState.AddModelError("Key", "Modelo invalido");

            //Prueba -- Act (Actuar, Acción)
            var respuesta = _actorController.Create(actorCreacionDto);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.Result, typeof(ViewResult));
        }
        [TestMethod]
        public void DevuelMismoActorDtoModeloInvalido_CreateTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)

            var actorCreacionDto = new ActorCreacionDto()
            {
                FechaNacimiento = DateTime.Now,
                Foto = null,
                Nombre = $"Nombre - {DateTime.Now.ToString()}",
            };
            _actorController.ModelState.AddModelError("Key", "Modelo invalido");

            //Prueba -- Act (Actuar, Acción)
            var respuesta = _actorController.Create(actorCreacionDto);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            var respuestaResult = respuesta.Result as ViewResult;
            var respuestActorDto = (ActorCreacionDto)respuestaResult.ViewData.Model;
            Assert.AreEqual(actorCreacionDto, respuestActorDto);
        }
    }
}