using Common.Validaciones;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Threading.Tasks;

namespace Common.Test.Validaciones
{
    [TestClass]
    public class ValidarFechaNacimientoAttributeTest : ValidarFechaNacimientoAttribute
    {
        [TestMethod]
        public void DevuelveBoolAlValidarFecha()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid(new DateTime(2020, 01, 31));

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta, typeof(bool));
        }
        [TestMethod()]
        public void DevuelveVerdaderoCuandoFechaMenorFechaActual()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid(new DateTime(2020, 01, 31));

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsTrue(respuesta);
        }
        [TestMethod()]
        public void DevuelveFalsoCuandoFechaMenorFechaActual_IsValid()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid(new DateTime(2020, 12, 31));

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsFalse(respuesta);
        }

        [TestMethod()]
        public void DevuelveFalsoCuandoFechaNull_IsValid()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid(null);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsFalse(respuesta);
        }

        [TestMethod()]
        public void DevuelveFalsoCuandoFechaStringVacio_IsValid()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid(string.Empty);

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsFalse(respuesta);
        }

        [TestMethod()]
        public void DevuelveFalsoCuandoFechaStringMayorFechaActual_IsValid()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid("01/12/2020");

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsFalse(respuesta);
        }

        [TestMethod()]
        public void DevuelveTrueCuandoFechaStringMenorFechaActual_IsValid()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid("01/01/2020");

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsTrue(respuesta);
        }

        [TestMethod()]
        public void DevuelveFalsoCuandoFechaStringCualquierVAlor_IsValid()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid("cualquier valor");

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsFalse(respuesta);
        }

        [TestMethod()]
        public void DevuelveFalsoCuandoFechaCualquierValor_IsValid()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid(new Object());

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsFalse(respuesta);
        }


        [TestMethod()]
        public void DevuelveValidationResulFechaCualquierValor_IsValidTest()
        {
            //Prueba -- Act (Actuar, Acción)
            var respuesta = IsValid(new DateTime(2020, 01, 01), new ValidationContext(new object()));

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.AreEqual(null, respuesta);
        }

        [TestMethod()]
        public void DevuelveVaNullCuandoFechaValorNull_IsValidTest()
        {
            //Prueba -- Act (Actuar, Acción)
            var respuesta = IsValid(null, new ValidationContext(new object()));

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.AreEqual(null, respuesta);
        }

        [TestMethod()]
        public void DevuelveVaNullCuandoFechaMayorFechaActual_IsValidTest()
        {
            //Prueba -- Act (Actuar, Acción)
            var respuesta = IsValid(new DateTime(2020, 12, 31), new ValidationContext(new object()));

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsInstanceOfType(respuesta.ErrorMessage, typeof(string));
        }

        [TestMethod()]
        public void DevuelveFalsoCuandoFechaCualquierValor_IsValidPrivateTest()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();
            MethodInfo infoMetodo = typeof(ValidarFechaNacimientoAttribute).GetMethod("IsValidPrivate", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parametros = { new DateTime(2020, 01, 01), false };
            
            //Prueba -- Act (Actuar, Acción)
            infoMetodo.Invoke(validarFechaNacimiento, parametros);
            var respuesta = (bool)parametros[1];

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.IsTrue(respuesta);
        }
    }
}
