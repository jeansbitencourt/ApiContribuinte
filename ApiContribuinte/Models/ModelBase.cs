using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContribuinte.Models
{
    public class ModelBase
    {
        [JsonIgnore]
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}
