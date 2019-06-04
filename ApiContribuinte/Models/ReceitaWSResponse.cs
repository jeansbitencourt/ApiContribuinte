using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ApiContribuinte.Models
{
    public class ReceitaWSResponse
    {
        [JsonProperty("atividade_principal")]
        public List<Cnae> AtividadePrincipal { get; set; }
        [JsonProperty("data_situacao")]
        public String DataSituacao { get; set; }
        [JsonProperty("nome")]
        public string RazaoSocial { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("situacao")]
        public string Situacao { get; set; }
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("numero")]
        public string Numero { get; set; }
        [JsonProperty("cep")]
        public string Cep { get; set; }
        [JsonProperty("municipio")]
        public string Municipio { get; set; }
        [JsonProperty("abertura")]
        public string DataAbertura { get; set; }
        [JsonProperty("natureza_juridica")]
        public string NaturezaJuridica { get; set; }
        [JsonProperty("fantasia")]
        public string Fantasia { get; set; }
        [JsonProperty("porte")]
        public string Porte { get; set; }
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }
        [JsonProperty("ultima_atualizacao")]
        public string UltimaAtualizacao { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("tipo")]
        public string Tipo { get; set; }
        [JsonProperty("complemento")]
        public string Complemento { get; set; }
        [JsonProperty("efr")]
        public string Efr { get; set; }
        [JsonProperty("motivo_situacao")]
        public string MotivoSituacao { get; set; }
        [JsonProperty("situacao_especial")]
        public string SituacaoEspecial { get; set; }
        [JsonProperty("data_situacao_especial")]
        public string DataSituacaoEspecial { get; set; }
        [JsonProperty("atividades_secundarias")]
        public List<Cnae> AtividadesSecundarias { get; set; }
        [JsonProperty("capital_social")]
        public string CapitalSocial { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

    }
}
