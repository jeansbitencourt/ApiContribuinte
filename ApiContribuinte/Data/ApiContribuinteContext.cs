using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiContribuinte.Models;

namespace ApiContribuinte
{
    public class ApiContribuinteContext : DbContext
    {
        public ApiContribuinteContext (DbContextOptions<ApiContribuinteContext> options)
            : base(options)
        {
        }

        public DbSet<ApiContribuinte.Models.Contribuinte> Contribuinte { get; set; }

        public DbSet<ApiContribuinte.Models.NaturezaJuridica> NaturezaJuridica { get; set; }

        public DbSet<ApiContribuinte.Models.Cnae> Cnae { get; set; }

        public DbSet<ApiContribuinte.Models.CnpjCnae> CnpjCnae { get; set; }

        public DbSet<ApiContribuinte.Models.Telefone> Telefone { get; set; }
    }
}
