using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastros
{
    public class Departamento
    {
        [DisplayName("Id")]
        public long? DepartamentoID { get; set; }

        [DisplayName("Nome do Departamento")]
        public string Nome { get; set; }

        [DisplayName("Id da Instituição")]
        public long? InstituicaoID { get; set; }
        public Instituicao Instituicao { get; set; }

        public virtual ICollection<Curso> Cursos { get; set; }
    }
}
