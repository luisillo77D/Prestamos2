using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prestamos
{
    internal class ClientesConsulta
    {
        private conexionMysql conexionMysql;
        private List<Clientes> mClientes;

        public ClientesConsulta() 
        {
            conexionMysql= new conexionMysql();
            mClientes= new List<Clientes>();
        }

        internal bool agregarCliente(Clientes mCliente)
        {
            string INSERT = "INSERT INTO clientes(nombre,apep,apem,Fecha,Celular,Direccion) VALUES(@nombre,@ap,@am,@fecha,@cel,@direc);";
            MySqlCommand mCommand = new MySqlCommand(INSERT, conexionMysql.GetConnection());
            mCommand.Parameters.Add(new MySqlParameter("@nombre", mCliente.nombre));
            mCommand.Parameters.Add(new MySqlParameter("@ap", mCliente.apeP));
            mCommand.Parameters.Add(new MySqlParameter("@am", mCliente.apeM));
            mCommand.Parameters.Add(new MySqlParameter("@fecha", mCliente.fechaN));
            mCommand.Parameters.Add(new MySqlParameter("@cel", mCliente.cel));
            mCommand.Parameters.Add(new MySqlParameter("@direc", mCliente.domicilio));

            return mCommand.ExecuteNonQuery() > 0;      
        }

        internal bool EliminarCliente(int id)
        {
            String DELETE = "DELETE FROM clientes WHERE id=@id";
            MySqlCommand mcoman= new MySqlCommand(DELETE, conexionMysql.GetConnection());
            mcoman.Parameters.Add(new MySqlParameter("@id", id));
            return mcoman.ExecuteNonQuery() > 0;
        }

        internal bool ActualizarCliente(Clientes mCliente,int id)
        {
            String UPDATE = "UPDATE clientes SET nombre=@nombre,apep=@apep,apem=@apem,Fecha=@fecha,Celular=@celular,Direccion=@dir WHERE id=@id;";
            MySqlCommand mcoman = new MySqlCommand(UPDATE, conexionMysql.GetConnection());
            mcoman.Parameters.Add(new MySqlParameter("@nombre", mCliente.nombre));
            mcoman.Parameters.Add(new MySqlParameter("@apep", mCliente.apeP));
            mcoman.Parameters.Add(new MySqlParameter("@apem", mCliente.apeM));
            mcoman.Parameters.Add(new MySqlParameter("@fecha", mCliente.fechaN));
            mcoman.Parameters.Add(new MySqlParameter("@celular", mCliente.cel));
            mcoman.Parameters.Add(new MySqlParameter("@dir", mCliente.domicilio));
            mcoman.Parameters.Add(new MySqlParameter("@id",id));
            return mcoman.ExecuteNonQuery() > 0;
        }


        internal List<Clientes> getProductos(string filtro)
        {
            string QUERY = "SELECT * FROM Clientes";
            MySqlDataReader mReader = null;
            try
            {
                if (filtro!= "")
                {
                    QUERY += " WHERE " + "id LIKE '%" + filtro + "%' OR " + "nombre LIKE '%" + filtro + "%' OR " +
                        "apep LIKE '%" + filtro + "%'";
                }
                MySqlCommand mComando = new MySqlCommand(QUERY);
                mComando.Connection= conexionMysql.GetConnection();
                mReader= mComando.ExecuteReader();

                Clientes mCliente = null;
                while (mReader.Read())
                {
                    mCliente= new Clientes();
                    mCliente.id = mReader.GetInt16("id");
                    mCliente.nombre=mReader.GetString("nombre");
                    mCliente.apeP = mReader.GetString("apep");
                    mCliente.apeM = mReader.GetString("apem");
                    mCliente.fechaN = mReader.GetDateTime("fecha");                
                    mCliente.cel = mReader.GetString("Celular");
                    mCliente.domicilio = mReader.GetString("Direccion");
                    mClientes.Add(mCliente);
                }
                mReader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return mClientes;
        }
    }
}
