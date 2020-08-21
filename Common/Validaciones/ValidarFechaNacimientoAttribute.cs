using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Validaciones
{
    public class ValidarFechaNacimientoAttribute : ValidationAttribute
    {
        #region Metodos
        public override bool IsValid(object value)
        {
            var fechaNacimiento = (DateTime)value;
            return fechaNacimiento <= DateTime.Now;
        }
        #endregion
    }
}
