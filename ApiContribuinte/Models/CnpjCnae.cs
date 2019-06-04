using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContribuinte.Models
{
    [Table("cnpj_cnae")]
    public class CnpjCnae : ModelBase
    {
        [JsonIgnore]
        [Column("cnpj")]
        public int ContribuinteId { get; set; }
        [JsonIgnore]
        public Contribuinte Contribuinte { get; set; }
        [JsonIgnore]
        [Column("cnae")]
        public int CnaeId { get; set; }
        public Cnae Cnae { get; set; }
        [Column("principal")]
        public bool Principal { get; set; }
    }
}
