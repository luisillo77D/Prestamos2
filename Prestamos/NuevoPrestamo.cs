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
    public partial class NuevoPrestamo : Form
    {
        private List<Prestamos2> mPrestamos2;
        private PrestamosConsulta mPrestamosConsulta;
        private Prestamos2 mPrestamo;
        private List<Clientes> mClientes;
        private ClientesConsulta mClientesConsulta;
        private Clientes mCliente;
        private double interes=1.15;
        public NuevoPrestamo()
        {
            InitializeComponent();
            mPrestamos2 = new List<Prestamos2>();
            mPrestamosConsulta = new PrestamosConsulta();
            cargarPrestamos();
            mPrestamo = new Prestamos2();
            txtId.Enabled = false;
            mClientes = new List<Clientes>();
            mClientesConsulta = new ClientesConsulta();
            cargarClientes();
            mCliente = new Clientes();
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
                    mClientes[i].domicilio,
                    mClientes[i].cel
                    );
            }
        }

        private void cargarPrestamos(string filtro = "")
        {
            dgvPrestamos.Rows.Clear();
            dgvPrestamos.Refresh();
            mPrestamos2.Clear();
            mPrestamos2 = mPrestamosConsulta.getPrestamos(filtro);

            for (int i = 0; i < mPrestamos2.Count(); i++)
            {
                dgvPrestamos.RowTemplate.Height = 50;
                dgvPrestamos.Rows.Add(
                    mPrestamos2[i].idPres,
                    mPrestamos2[i].nombre,
                    mPrestamos2[i].ape,
                    mPrestamos2[i].cantidad,
                    mPrestamos2[i].restante,
                    mPrestamos2[i].fechain,
                    mPrestamos2[i].fechafin                   
                    );
            }
        }

        private void NuevoPrestamo_Load(object sender, EventArgs e)
        {

        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dgvClientes.Rows[e.RowIndex];
            txtId.Text = fila.Cells["id"].Value.ToString();
            txtNombre.Text = fila.Cells["Nombre2"].Value.ToString();
            txtAp.Text = fila.Cells["ap"].Value.ToString();
            txtAm.Text = fila.Cells["am"].Value.ToString();
            dtFecha.Value = Convert.ToDateTime(fila.Cells["FechaNac"].Value);
                  
        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
            DateTime fechafin= dtpFechaInicio.Value.AddDays(105);
            dtpFechafin.Value = fechafin;
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }

        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            actRatios();          
        }
        public void actRatios()
        {
            if (txtCantidad.Text.Equals(""))
            {
                txtPagosem.Text = 0.ToString();
                txtTotal.Text = 0.ToString();

            }
            else
            {
                double total, pagos;
                total = Convert.ToDouble(txtCantidad.Text) * interes;
                pagos = total / 15;
                txtPagosem.Text = pagos.ToString();
                txtTotal.Text = total.ToString();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    interes = 1.15;
                    actRatios();
                    break;
                case 1:
                    interes= 1.20;
                    actRatios();
                    break;
                case 2:
                    interes = 1.12;
                    actRatios();
                    break;
            }
        }
    }
}
