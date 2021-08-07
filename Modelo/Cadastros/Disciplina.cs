using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Modelo.Cadastros
{
    public class Disciplina
    {
        [DisplayName("Id")]
        public long? DisciplinaID { get; set; }

        [DisplayName("Nome da Disciplina")]
        public string Nome { get; set; }

        public virtual ICollection<CursoDisciplina> CursosDisciplinas { get; set; }
    }
}
