using Common.Enums;
using Common.Validaciones;
using Microsoft.AspNetCore.Http;

namespace Common.Dtos
{
    public class ActorCreacionDto : ActorPatchDto
    {
        [PesoArchivoValidacionAttribute(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacionAttribute(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
