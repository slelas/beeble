using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beeble.Data
{
    public class BeebleInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
    {

    }
}
