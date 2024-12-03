using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsumirApi.Controllers;
using ConsumirApi.Models;
namespace ConsumirApi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        CosmeticsController ccontroller = new CosmeticsController();
        private async void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                var cosmetics = await ccontroller.ObtenerCosmeticsAsync();

                dgview.AutoGenerateColumns = false;

                // Configurar columnas manualmente
                dgview.Columns.Clear();
                dgview.Columns.Add("id", "ID");
                dgview.Columns.Add("name", "Nombre");
                dgview.Columns.Add("description", "Descripción");

                dgview.DataSource = cosmetics;

                // Mapear propiedades a columnas
                dgview.Columns["id"].DataPropertyName = "id";
                dgview.Columns["name"].DataPropertyName = "name";
                dgview.Columns["description"].DataPropertyName = "description";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
