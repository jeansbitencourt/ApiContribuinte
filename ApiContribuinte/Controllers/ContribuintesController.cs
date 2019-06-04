using ApiContribuinte.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ApiContribuinte.Controllers
{
    [Route("api/cnpj")]
    [ApiController]
    public class ContribuintesController : ControllerBase
    {
        private readonly ApiContribuinteContext _context;

        public ContribuintesController(ApiContribuinteContext context)
        {
            _context = context;
        }

        // GET: api/Cnpj/{cnpj}
        [HttpGet("{cnpj}")]
        public async Task<ActionResult<Contribuinte>> GetContribuinte(string cnpj)
        {
            Contribuinte contribuinte = await _context.Contribuinte
                .Include(c => c.NaturezaJuridica)
                .Include(c => c.ListaCnae)
                .Include("ListaCnae.Cnae")
                .Include(c => c.ListaTelefones)
                .SingleOrDefaultAsync(c => c.Cnpj == cnpj);

            if (contribuinte == null || DateTime.Now > contribuinte.ValidadeConsulta)
            {
                var response = await ConsultarReceitaWS(cnpj);

                if (response == null || response.Contains("Too many requests, please try again later."))
                {
                    if (contribuinte == null || contribuinte.Id.Equals(0))
                        return StatusCode(400, new Erro { Code = 400, Message = "Erro ao tentar consultar CNPJ informado, tente novamente mais tarde." });
                    return contribuinte;
                }

                ReceitaWSResponse rcWsResp = JsonConvert.DeserializeObject<ReceitaWSResponse>(response);

                if (!rcWsResp.Status.Equals("OK"))
                    return StatusCode(400, new Erro { Code = 400, Message = rcWsResp.Message });

                if (contribuinte == null)
                    contribuinte = new Contribuinte();

                string[] split = rcWsResp.NaturezaJuridica.Split(" - ");
                string codigo = split[0].Trim();
                string texto = split[1].Trim();

                NaturezaJuridica naturezaJuridica = await _context.NaturezaJuridica.SingleOrDefaultAsync(n => n.Codigo == codigo);

                if (naturezaJuridica == null)
                {
                    naturezaJuridica = new NaturezaJuridica { Codigo = codigo, Descricao = texto };
                    await _context.NaturezaJuridica.AddAsync(naturezaJuridica);
                }

                contribuinte.Cnpj = cnpj;
                contribuinte.NaturezaJuridica = naturezaJuridica;
                await _context.SaveChangesAsync();

                Cnae cnae = await _context.Cnae.SingleOrDefaultAsync(c => c.Codigo == rcWsResp.AtividadePrincipal[0].Codigo);

                if (cnae == null)
                {
                    cnae = new Cnae { Codigo = rcWsResp.AtividadePrincipal[0].Codigo, Descricao = rcWsResp.AtividadePrincipal[0].Descricao };
                    await _context.Cnae.AddAsync(cnae);
                    await _context.SaveChangesAsync();
                }

                if (contribuinte.ListaCnae != null)
                {
                    for (int i = 0; i < contribuinte.ListaCnae.Count; i++)
                    {
                        _context.CnpjCnae.Remove(contribuinte.ListaCnae[i]);
                        await _context.SaveChangesAsync();
                    }
                }

                contribuinte.ListaCnae = new List<CnpjCnae>
                {
                    new CnpjCnae { Cnae = new Cnae { Id = cnae.Id, Codigo = cnae.Codigo, Descricao = cnae.Descricao }, Principal = true, Contribuinte = contribuinte, ContribuinteId = contribuinte.Id }
                };

                foreach (Cnae cn in rcWsResp.AtividadesSecundarias)
                {
                    cnae = await _context.Cnae.SingleOrDefaultAsync(c => c.Codigo == cn.Codigo);

                    if (cnae == null)
                    {
                        cnae = new Cnae { Codigo = cn.Codigo, Descricao = cn.Descricao };
                        await _context.Cnae.AddAsync(cnae);
                        await _context.SaveChangesAsync();
                    }

                    CnpjCnae cnpjCnae = new CnpjCnae
                    {
                        Cnae = new Cnae { Id = cnae.Id, Codigo = cnae.Codigo, Descricao = cnae.Descricao },
                        Principal = false,
                        Contribuinte = contribuinte,
                        ContribuinteId = contribuinte.Id
                    };

                    await _context.CnpjCnae.AddAsync(cnpjCnae);
                    await _context.SaveChangesAsync();
                }

                foreach (string sTelefone in rcWsResp.Telefone.Split(" / "))
                {
                    if (contribuinte.ListaTelefones == null)
                        contribuinte.ListaTelefones = new List<Telefone>();

                    Telefone telefone = await _context.Telefone.SingleOrDefaultAsync(t => t.Numero == sTelefone.Trim());

                    if (telefone == null)
                    {
                        telefone = new Telefone { Numero = sTelefone.Trim(), Contribuinte = contribuinte, ContribuinteId = contribuinte.Id };
                        await _context.Telefone.AddAsync(telefone);
                        await _context.SaveChangesAsync();
                    }
                }

                string[] dt;

                dt = rcWsResp.DataAbertura.Split("/");
                contribuinte.DataAbertura = new DateTime(int.Parse(dt[2]), int.Parse(dt[1]), int.Parse(dt[0]));
                contribuinte.Porte = rcWsResp.Porte;
                contribuinte.TipoEmpresa = rcWsResp.Tipo;
                contribuinte.RazaoSocial = rcWsResp.RazaoSocial;
                contribuinte.Fantasia = rcWsResp.Fantasia;
                contribuinte.Email = rcWsResp.Email;
                contribuinte.Logradouro = rcWsResp.Logradouro;
                contribuinte.Numero = rcWsResp.Numero;
                contribuinte.Complemento = rcWsResp.Complemento;
                contribuinte.Cep = rcWsResp.Cep;
                contribuinte.Bairro = rcWsResp.Bairro;
                contribuinte.Municipio = rcWsResp.Municipio;
                contribuinte.Uf = rcWsResp.Uf;
                contribuinte.SituacaoRfb = rcWsResp.Situacao;
                contribuinte.MotivoSituacaoRfb = rcWsResp.MotivoSituacao;
                dt = rcWsResp.DataSituacao.Split("/");
                contribuinte.DataSituacaoRfb = new DateTime(int.Parse(dt[2]), int.Parse(dt[1]), int.Parse(dt[0]));
                contribuinte.ValidadeConsulta = DateTime.Now.AddDays(10);

                if (contribuinte.Id.Equals(0))
                    await _context.Contribuinte.AddAsync(contribuinte);

                await _context.SaveChangesAsync();
            }

            return contribuinte;
        }

        private async Task<string> ConsultarReceitaWS(string cnpj)
        {
            var request = (HttpWebRequest)WebRequest.Create("https://www.receitaws.com.br/v1/cnpj/" + cnpj);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Method = "GET";
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            request.ContentType = "application/json; charset=utf-8";
            request.Timeout = 2500;

            try
            {
                WebResponse response = await request.GetResponseAsync();
                StreamReader sjson = new StreamReader(response.GetResponseStream());
                return sjson.ReadToEnd();
            }
            catch (WebException e) when (e.Status == WebExceptionStatus.Timeout)
            {
                return null;
            }
        }
    }
}
