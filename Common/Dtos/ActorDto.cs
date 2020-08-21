using Common.Validaciones;
using System;
using System.ComponentModel.DataAnnotations;

namespace Common.Dtos
{
    public class ActorDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(120)]
        [Display(Name = "Actor")]
        public string Nombre { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [ValidarFechaNacimiento]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Foto")]
        public string Foto { get; set; }
    }
}
