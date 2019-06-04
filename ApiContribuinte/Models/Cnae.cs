using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContribuinte.Models
{
    [Table("cnae")]
    public class Cnae : ModelBase
    {
        [Column("codigo")]
        [JsonProperty("code")]
        public string Codigo { get; set; }
        [Column("descricao")]
        [JsonProperty("text")]
        public string Descricao { get; set; }
    }
}
