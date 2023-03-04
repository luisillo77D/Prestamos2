using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prestamos
{
    internal class PrestamosConsulta
    {
        private conexionMysql conexionMysql;
        private List<Prestamos2> mPrestamos2;
        public PrestamosConsulta()
        {
            conexionMysql = new conexionMysql();
            mPrestamos2 = new List<Prestamos2>();
        }

        internal bool agregarPrestamo(Prestamos2 mPrestamos2)
        {
            string INSERT = "INSERT INTO prestamos(idCliente,cantidad,restante,Fechainicio,Fechafin) VALUES(@idCliente,@cant,@rest,@fechainicio,@fechafin);";
            MySqlCommand mCommand = new MySqlCommand(INSERT, conexionMysql.GetConnection());
            mCommand.Parameters.Add(new MySqlParameter("@idCliente", mPrestamos2.idClient));
            mCommand.Parameters.Add(new MySqlParameter("@cant", mPrestamos2.cantidad));
            mCommand.Parameters.Add(new MySqlParameter("@rest", mPrestamos2.restante));
            mCommand.Parameters.Add(new MySqlParameter("@fechainicio", mPrestamos2.fechain));
            mCommand.Parameters.Add(new MySqlParameter("@fechafin", mPrestamos2.fechafin));

            return mCommand.ExecuteNonQuery() > 0;

        }

        internal List<Prestamos2> getPrestamos(string filtro)
        {
            string QUERY = "SELECT Prestamos.idPres,Clientes.Nombre,Clientes.ApellidoPat,Prestamos.cantidad,Prestamos.restante,Prestamos.fechainicio,Prestamos.Fechafin FROM Prestamos INNER JOIN Clientes ON prestamos.idCliente = Clientes.id";
            MySqlDataReader mReader = null;
            try
            {
                if (filtro != "")
                {
                    QUERY += " WHERE " + "Prestamos.idPres LIKE '%" + filtro + "%' OR " + "Prestamos.idCliente LIKE '%" + filtro + "%' OR " +
                        "Prestamos.fechafin LIKE '%" + filtro + "%'";
                }
                MySqlCommand mComando = new MySqlCommand(QUERY);
                mComando.Connection = conexionMysql.GetConnection();
                mReader = mComando.ExecuteReader();

                Prestamos2 mPrestamo2 = null;
                while (mReader.Read())
                {
                    mPrestamo2 = new Prestamos2();
                    mPrestamo2.idPres = mReader.GetInt16("idPres");
                    mPrestamo2.nombre = mReader.GetString("nombre");
                    mPrestamo2.ape = mReader.GetString("ApellidoPat");
                    mPrestamo2.cantidad = mReader.GetDouble("cantidad");
                    mPrestamo2.restante = mReader.GetDouble("restante");
                    mPrestamo2.fechain = mReader.GetDateTime("Fechainicio");
                    mPrestamo2.fechafin = mReader.GetDateTime("Fechafin");
                    mPrestamos2.Add(mPrestamo2);
                }
                mReader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return mPrestamos2;
        }
    }
}
