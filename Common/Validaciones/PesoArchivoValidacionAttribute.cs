using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Common.Validaciones
{
    public class PesoArchivoValidacionAttribute : ValidationAttribute
    {
        #region Campos
        private readonly int pesoMaximoEnMegaBytes;
        #endregion

        #region Constructor
        public PesoArchivoValidacionAttribute(int PesoMaximoEnMegaBytes)
        {
            pesoMaximoEnMegaBytes = PesoMaximoEnMegaBytes;
        }
        #endregion

        #region Metodos
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > pesoMaximoEnMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"El peso del archivo no debe ser mayor a {pesoMaximoEnMegaBytes}mb");
            }

            return ValidationResult.Success;
        } 
        #endregion
    }
}
