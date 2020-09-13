using banco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco.Services
{
    public interface ICuentaService
    {
        bool Transferencia(Transferencia transfer);
    }
}
