using System;
using System.Collections.Generic;
using System.Text;
using AffiliateMarketing.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AffiliateMarketing.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Publisher> publishers { get; set; }
        public DbSet<Marchant> marchants { get; set; }
    }
}
