using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Modelo.Cadastros
{
    public class CursoDisciplina
    {
        [DisplayName("Id")]
        public long? CursoID { get; set; }
        public Curso Curso { get; set; }
        public long? DisciplinaID { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
