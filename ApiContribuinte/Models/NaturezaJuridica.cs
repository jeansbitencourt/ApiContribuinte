using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContribuinte.Models
{
    [Table("natureza_juridica")]
    public class NaturezaJuridica : ModelBase
    {
        [Column("codigo")]
        public string Codigo { get; set; }
        [Column("descricao")]
        public string Descricao { get; set; }
    }
}
