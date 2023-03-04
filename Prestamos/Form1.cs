using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prestamos
{
    public partial class Form1: Form
    {
        private List<Clientes> mClientes;
        private ClientesConsulta mClientesConsulta;
        private Clientes mCliente;
        public Form1()
        {
            InitializeComponent();
            mClientes= new List<Clientes>();
            mClientesConsulta= new ClientesConsulta();
            cargarClientes();
            mCliente= new Clientes();
            txtId.Enabled= false;
        }

        private void cargarClientes(string filtro = "")
        {
            dgvClientes.Rows.Clear();
            dgvClientes.Refresh();
            mClientes.Clear();
            mClientes = mClientesConsulta.getProductos(filtro);

            for (int i = 0; i < mClientes.Count(); i++)
            {
                dgvClientes.RowTemplate.Height = 50;
                dgvClientes.Rows.Add(
                mClientes[i].id,
                mClientes[i].nombre,
                mClientes[i].apeP,
                mClientes[i].apeM,
                mClientes[i].fechaN,
                mClientes[i].cel,
                mClientes[i].domicilio
                    );
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cargarClientes(txtBusqueda.Text.Trim());
        }
        private bool Check() {
            if (txtNombre.Text.Trim().Equals(""))
            {
                MessageBox.Show("ingrese el nombre");
                return false;
            }
            if (txtAp.Text.Trim().Equals(""))
            {
                MessageBox.Show("ingrese el nombre");
                return false;
            }
            if (txtAm.Text.Trim().Equals(""))
            {
                MessageBox.Show("ingrese el nombre");
                return false;
            }
            if (txtDirec.Text.Trim().Equals(""))
            {
                MessageBox.Show("ingrese el nombre");
                return false;
            }
            if (dtFecha.Text.Trim().Equals(""))
            {
                MessageBox.Show("ingrese el nombre");
                return false;
            }
            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Check())
            {
                return;
            }
            cargarDatosClientes();

            if (mClientesConsulta.agregarCliente(mCliente))
            {
                MessageBox.Show("Cliente agregado");
                cargarClientes();
                limpiarCampos();
            }           
        }

        private void limpiarCampos()
        {
            txtId.Clear();
            txtNombre.Clear();
            txtAp.Clear();
            txtAm.Clear();
            txtDirec.Clear();
            txtCel.Clear();
        }

        private void cargarDatosClientes()
        {
            //mCliente.id=Convert.ToInt32 (txtId.Text);
            mCliente.nombre= txtNombre.Text.Trim();
            mCliente.apeP= txtAp.Text.Trim();
            mCliente.apeM= txtAm.Text.Trim();
            mCliente.domicilio= txtDirec.Text.Trim();
            mCliente.fechaN=dtFecha.Value.Date;
            mCliente.cel= txtCel.Text.Trim();
        }

        private void dataGridView1_CellntentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila= dgvClientes.Rows[e.RowIndex];
            txtId.Text = fila.Cells["Folio"].Value.ToString();
            txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
            txtAp.Text = fila.Cells["ApellidoP"].Value.ToString();
            txtAm.Text = fila.Cells["ApellidoM"].Value.ToString() ;
            dtFecha.Value = Convert.ToDateTime(fila.Cells["FechaNacimiento"].Value);
            txtCel.Text = fila.Cells["Celular"].Value.ToString();
            txtDirec.Text = fila.Cells["Direccion"].Value.ToString();

            DateTime fechafin;
            fechafin = dtFecha.Value.AddDays(105);           
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
        }

        private void button1_Click(object sender, EventArgs e)
        {          
            NuevoPrestamo frm= new NuevoPrestamo();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Check())
            {
                return;
            }
            cargarDatosClientes();
            if (mClientesConsulta.ActualizarCliente(mCliente,Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("Cliente actualizado");
                cargarClientes();
                limpiarCampos();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Check())
            {
                return;
            }
            if (MessageBox.Show("Desea eliminar?","xd",MessageBoxButtons.YesNo)==DialogResult.No)
            {
                return;
            }
            else {
            if (mClientesConsulta.EliminarCliente(Convert.ToInt32(txtId.Text)))
            {
                MessageBox.Show("Cliente eliminado");
                cargarClientes();
                limpiarCampos();
            }
            }
        }
    }
}
