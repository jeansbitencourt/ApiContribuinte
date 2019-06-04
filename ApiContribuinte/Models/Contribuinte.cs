using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiContribuinte.Models
{
    [Table("cnpj")]
    public class Contribuinte : ModelBase
    {
        [Column("data_abertura")]
        public DateTime DataAbertura { get; set; }
        [JsonIgnore]
        [Column("natureza_juridica")]
        public int NaturezaJuridicaId { get; set; }
        public NaturezaJuridica NaturezaJuridica { get; set; }
        [Column("porte")]
        public string Porte { get; set; }
        [Column("tipo_empresa")]
        public string TipoEmpresa { get; set; }
        public List<CnpjCnae> ListaCnae { get; set; }
        public List<Telefone> ListaTelefones { get; set; }
        [Column("razao_social")]
        public string RazaoSocial { get; set; }
        [Column("fantasia")]
        public string Fantasia { get; set; }
        [MaxLength(14)]
        [Required]
        [Index(IsUnique = true)]
        [Column("cnpj")]
        public string Cnpj { get; set; }
        /*[Column("iest")]
        public string Iest { get; set; }
        [Column("enquadramento")]
        public string Enquadramento { get; set; }*/
        [Column("email")]
        public string Email { get; set; }
        [Column("logradouro")]
        public string Logradouro { get; set; }
        [Column("numero")]
        public string Numero { get; set; }
        [Column("complemento")]
        public string Complemento { get; set; }
        [Column("cep")]
        public string Cep { get; set; }
        [Column("bairro")]
        public string Bairro { get; set; }
        [Column("municipio")]
        public string Municipio { get; set; }
        [Column("uf")]
        public string Uf { get; set; }
        [Column("situacao_rfb")]
        public string SituacaoRfb { get; set; }
        [Column("motivo_situacao_rfb")]
        public string MotivoSituacaoRfb { get; set; }
        [Column("data_situacao_rfb")]
        public DateTime DataSituacaoRfb { get; set; }
        /*[Column("situacao_sefaz")]
        public string SituacaoSefaz { get; set; }
        [Column("motivo_situacao_sefaz")]
        public string MotivoSituacaoSefaz { get; set; }
        [Column("data_situacao_sefaz")]
        public DateTime DataSituacaoSefaz { get; set; }*/
        [Column("validade_consulta")]
        public DateTime ValidadeConsulta { get; set; }
    }
}
