using Common.Enums;
using Common.Validaciones;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos
{
    public class ActorEdicionDto : ActorDto
    {
        [PesoArchivoValidacionAttribute(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacionAttribute(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        [Display(Name = "Foto")]
        public IFormFile FotoFile { get; set; }
    }
}
