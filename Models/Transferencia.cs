using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace banco.Models
{
    public class Transferencia
    {
        public int CuentaOrigen { get; set; }
        public int Cantidad { get; set; }
        public int CuentaDestino { get; set; }
    }
}