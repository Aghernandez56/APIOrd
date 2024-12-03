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
                dgview.Columns.Add("rarity", "Rareza");

                dgview.DataSource = cosmetics;

                // Mapear propiedades a columnas
                dgview.Columns["id"].DataPropertyName = "id";
                dgview.Columns["name"].DataPropertyName = "name";
                dgview.Columns["description"].DataPropertyName = "description";
                dgview.Columns["rarity"].DataPropertyName = "rarity";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                throw;
            }
        }

        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(combobx.Text.ToString()))
            {
                MessageBox.Show("Por favor, selecciona una rareza.");
                return;
            }
            var cos = await ccontroller.ObtenerPorRareza(combobx.SelectedItem.ToString());

            dgview.AutoGenerateColumns = false;

            // Configurar columnas manualmente
            dgview.Columns.Clear();
            dgview.Columns.Add("id", "ID");
            dgview.Columns.Add("name", "Nombre");
            dgview.Columns.Add("description", "Descripción");
            dgview.Columns.Add("rarity", "Rareza");

            dgview.DataSource = cos;

            // Mapear propiedades a columnas
            dgview.Columns["id"].DataPropertyName = "id";
            dgview.Columns["name"].DataPropertyName = "name";
            dgview.Columns["description"].DataPropertyName = "description";
            dgview.Columns["rarity"].DataPropertyName = "rarity";


        }
    }
}
