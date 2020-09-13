using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace banco.Services
{
    public interface IUsuarioService
    {
        USUARIO ValidarLogin(USUARIO usuario);
        bool RegistrarUsuario(USUARIO usuario);
    }
}
