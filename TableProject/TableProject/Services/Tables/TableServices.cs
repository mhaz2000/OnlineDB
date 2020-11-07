using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TableProject.Context;

namespace TableProject.Services
{
    public class TableServices : ITableServices
    {
        //get namesof all tables.
        public List<string> GetAllTableNames()
        {
            List<string> names = new List<string>();

            using (OnlineDB context = new OnlineDB())
            {
                ObjectContext objContext = ((IObjectContextAdapter)context).ObjectContext;
                MetadataWorkspace workspace = objContext.MetadataWorkspace;
                IEnumerable<EntityType> tables = workspace.GetItems<EntityType>(DataSpace.SSpace);

                foreach (var index in tables)
                    names.Add(index.Name);

                return names;

            }
        }
    }
}