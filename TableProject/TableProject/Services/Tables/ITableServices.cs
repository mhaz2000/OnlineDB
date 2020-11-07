using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableProject.Services
{
    public interface ITableServices
    {
        List<string> GetAllTableNames();
    }
}
