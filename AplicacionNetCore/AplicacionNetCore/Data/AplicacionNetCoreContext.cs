using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AplicacionNetCore.Models
{
    public class AplicacionNetCoreContext : DbContext
    {
        public AplicacionNetCoreContext (DbContextOptions<AplicacionNetCoreContext> options)
            : base(options)
        {
        }

        public DbSet<AplicacionNetCore.Models.Contacto> Contacto { get; set; }
    }
}
