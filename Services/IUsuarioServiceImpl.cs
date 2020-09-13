using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web;

namespace banco.Services
{
    public class IUsuarioServiceImpl: IUsuarioService
    {

        public USUARIO ValidarLogin(USUARIO usuario)
        {
            using (bancoEntities ctx = new bancoEntities())
            {
                try
                {
                    StringBuilder sql = new StringBuilder();

                    sql.Append("select * from USUARIO ");
                    sql.Append("where email = '" + usuario.email + "' and clave = '" + usuario.clave + "'");

                    var result = ctx.Database.SqlQuery<USUARIO>(sql.ToString()).FirstOrDefault();
                    return result;

                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }


        public bool RegistrarUsuario(USUARIO usuario)
        {
            using (bancoEntities ctx = new bancoEntities())
            {
                try
                {
                    // Validar que el correo electrónico no exista
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(email) as total from USUARIO ");
                    sql.Append("where email = '" + usuario.email + "'");

                    int existeEmail = ctx.Database.SqlQuery<int>(sql.ToString()).FirstOrDefault();

                    if (existeEmail > 0)
                    {
                        throw new Exception("El email esta siendo usado por otro usuario.");
                    }


                    // Validar que el documento no este siendo usado
                    sql = new StringBuilder();
                    sql.Append("select count(identificacion) as total from USUARIO ");
                    sql.Append("where identificacion = '" + usuario.identificacion + "'");

                    int existeIdentificacion = ctx.Database.SqlQuery<int>(sql.ToString()).FirstOrDefault();

                    if (existeIdentificacion > 0)
                    {
                        throw new Exception("La identificación está siendo usada por otro usuario.");
                    }


                    ctx.Entry(usuario).State = EntityState.Added;
                    ctx.SaveChanges();
                    var LastIdInsert  = usuario.id_usuario;

                    var random = new Random();
                    int randomAccount = random.Next();

                    sql = new StringBuilder();
                    sql.Append("insert into CUENTA ");
                    sql.Append("(id_usuario, numero_cuenta, saldo) ");
                    sql.Append("VALUES ('" + LastIdInsert + "', '" + randomAccount + "', " + 1000000 + ")");

                    var result = ctx.Database.ExecuteSqlCommand(sql.ToString());

                    return true;
                                    
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}