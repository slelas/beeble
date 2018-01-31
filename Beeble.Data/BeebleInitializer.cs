using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Beeble.Data.Models;

namespace Beeble.Data
{
    public class BeebleInitializer : DropCreateDatabaseAlways<AuthContext>
        // public class BeebleInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {
        protected override void Seed(AuthContext context)
        {
			

            base.Seed(context);
        }
    }
}
