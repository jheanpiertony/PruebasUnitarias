using Common.Validaciones;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Common.Test.Validaciones
{
    [TestClass]
    public class ValidarFechaNacimientoAttributeTest
    {
        [TestMethod()]
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
        public void DevuelveFalsoCuandoFechaMenorFechaActual()
        {
            //Prepración -- Arrange (Arreglar, Organizar, Ordenar)
            var validarFechaNacimiento = new ValidarFechaNacimientoAttribute();

            //Prueba -- Act (Actuar, Acción)
            var respuesta = validarFechaNacimiento.IsValid(new DateTime(2020, 01, 31));

            //Verifiación -- Assert (Afirmar, Asegurar, Hacer valer)
            Assert.AreNotEqual<bool>(false, respuesta);
        } 
    }
}
