using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prova.Infra.Data.Migrations
{
    public class Configuration : DbMigrationsConfiguration<Contexto.ProvaContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
    }
}
