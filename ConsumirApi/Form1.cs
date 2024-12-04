using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConsumirApi.Controllers;
using ConsumirApi.Models;
using static System.Net.Mime.MediaTypeNames;
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
                dgview.Columns.Add("images", "Url");

                dgview.DataSource = cosmetics;

                // Mapear propiedades a columnas
                dgview.Columns["id"].DataPropertyName = "id";
                dgview.Columns["name"].DataPropertyName = "name";
                dgview.Columns["description"].DataPropertyName = "description";
                dgview.Columns["rarity"].DataPropertyName = "rarity";
                dgview.Columns["images"].DataPropertyName = "images";

                dgview.CellClick += (s, ev) =>
                {
                    if (ev.RowIndex >= 0)
                    {
                        var iconUrl = dgview.Rows[ev.RowIndex].Cells["images"].Value?.ToString();
                        if (!string.IsNullOrEmpty(iconUrl))
                        {
                            pic.Load(iconUrl); // Carga la imagen desde la URL
                        }
                    }
                };

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
            dgview.Columns.Add("images", "Url");

            dgview.DataSource = cos;

            // Mapear propiedades a columnas
            dgview.Columns["id"].DataPropertyName = "id";
            dgview.Columns["name"].DataPropertyName = "name";
            dgview.Columns["description"].DataPropertyName = "description";
            dgview.Columns["rarity"].DataPropertyName = "rarity";
            dgview.Columns["images"].DataPropertyName = "images";

            dgview.CellClick += (s, ev) =>
            {
                if (ev.RowIndex >= 0)
                {
                    var iconUrl = dgview.Rows[ev.RowIndex].Cells["images"].Value?.ToString();
                    if (!string.IsNullOrEmpty(iconUrl))
                    {
                        pic.Load(iconUrl); // Carga la imagen desde la URL
                    }
                }
            };
        }
    }
}
