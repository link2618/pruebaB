using banco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Web;

namespace banco.Services
{
    public class ICuentaServiceImpl : ICuentaService
    {

        public bool Transferencia(Transferencia transfer)
        {
            using (bancoEntities ctx = new bancoEntities())
            {
                try
                {
                    // Se debe iniciar una transacción, para garantizar que la operación se lleve a cabo exitosamente
                    // En caso de ur error, realizar un rollback
                    using (TransactionScope scope = new TransactionScope())
                    {
                        StringBuilder sql = new StringBuilder();

                        // Validar que la cuenta de origen posea en su saldo la cantidad a transferir.

                        sql.Append("select saldo from CUENTA ");
                        sql.Append("where numero_cuenta = '"+transfer.CuentaOrigen+"'");

                        int saldoDisponible = ctx.Database.SqlQuery<int>(sql.ToString()).FirstOrDefault();
                        if(saldoDisponible >= transfer.Cantidad)
                        {
                            // Restamos la cantidad a transferir de la cuenta de origen
                            sql = new StringBuilder();
                            sql.Append("update CUENTA ");
                            sql.Append("set saldo = (saldo - " + transfer.Cantidad+") ");
                            sql.Append("where numero_cuenta = "+transfer.CuentaOrigen);

                            int updateSaldoOrigen = ctx.Database.ExecuteSqlCommand(sql.ToString());

                            if(updateSaldoOrigen == 1)
                            {
                                // Incrementamos el saldo de la cuemnta de destino
                                sql = new StringBuilder();
                                sql.Append("update CUENTA ");
                                sql.Append("set saldo = (saldo + " + transfer.Cantidad + ") ");
                                sql.Append("where numero_cuenta = " + transfer.CuentaDestino);

                                int updateSaldoDestino = ctx.Database.ExecuteSqlCommand(sql.ToString());
                            }


                        } else
                        {
                            throw new Exception();
                        }
                        scope.Complete();
                    }
                    return true;
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }

    }
}