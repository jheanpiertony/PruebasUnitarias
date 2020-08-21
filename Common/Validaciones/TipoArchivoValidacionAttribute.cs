using Common.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Common.Validaciones
{
    public class TipoArchivoValidacionAttribute : ValidationAttribute
    {
        #region Campos
        private readonly string[] tiposValidos;
        #endregion

        #region Constructor
        public TipoArchivoValidacionAttribute(string[] tiposValidos)
        {
            this.tiposValidos = tiposValidos;
        }

        public TipoArchivoValidacionAttribute(GrupoTipoArchivo grupoTipoArchivo)
        {
            if (grupoTipoArchivo == GrupoTipoArchivo.Imagen)
            {
                tiposValidos = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
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

            if (!tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"El tipo del archivo debe ser uno de los siguientes: {string.Join(", ", tiposValidos)}");
            }

            return ValidationResult.Success;
        } 
        #endregion
    }
}
