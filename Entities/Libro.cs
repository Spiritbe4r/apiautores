using System.ComponentModel.DataAnnotations;

namespace webapiautores.Entities
{
    public class Libro
    {
        [Key]
        public int Id{get;set;}
        public string Titulo{get;set;}
        public int AutorId{get;set;}
        public Autor Autor{get;set;}
    }
}