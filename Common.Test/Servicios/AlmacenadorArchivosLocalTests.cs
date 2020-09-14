using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Common.Test.Servicios
{
    [TestClass()]
    public class AlmacenadorArchivosLocalTests
    {

        [TestMethod()]
        public async Task DevuelverUrlLocalDelArchivo_GuardarArchivoTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            webHostEnvironmentMock.Setup(x => x.WebRootPath).Returns("C:\\Users\\david.restrepo\\source\\repos\\jheanpiertony\\PruebasUnitariasDos\\PruebaMVC\\wwwroot");
            
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext.Request.Scheme)
                .Returns("https");
            httpContextAccessorMock.Setup(x => x.HttpContext.Request.Host)
                .Returns(new HostString("localhost:44303"));
            var almacenadorArchivosLocal = new Common.Servicios.AlmacenadorArchivosLocal(webHostEnvironmentMock.Object, httpContextAccessorMock.Object);

            //Prueba -- Act (Actuar, Acción)
            var respuesta = await almacenadorArchivosLocal.GuardarArchivo(new MemoryStream().ToArray(), ".jpg", "actores", "image/jpeg");

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta, typeof(string));
        }
    }
}