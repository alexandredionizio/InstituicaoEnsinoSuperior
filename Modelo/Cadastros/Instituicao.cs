using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Modelo.Cadastros
{
    public class Instituicao
    {
        [DisplayName("Id")]
        public long? InstituicaoID { get; set; }
        [DisplayName("Nome da Instituição")]
        public string Nome { get; set; }
        [DisplayName("Endereço da Instituição")]
        public string Endereco { get; set; }

        public virtual ICollection<Departamento> Departamentos { get; set; }
    }
}
