using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContribuinte.Models
{
    [Table("telefone")]
    public class Telefone : ModelBase
    {
        [JsonIgnore]
        [Column("cnpj")]
        public int ContribuinteId { get; set; }
        [JsonIgnore]
        public Contribuinte Contribuinte { get; set; }
        [Column("numero")]
        public string Numero { get; set; }
    }
}
