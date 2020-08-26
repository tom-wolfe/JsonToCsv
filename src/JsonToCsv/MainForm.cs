using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JsonToCsv
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if (ofdImport.ShowDialog(this) != DialogResult.OK) { return; }
            txtJson.Text = File.ReadAllText(ofdImport.FileName);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var json = JArray.Parse(txtJson.Text);
            var flattened = json.Select(JsonFlattener.Flatten).ToList();
            var columns = flattened[0].Keys.ToArray();

            var selectDialog = new SelectColumnsDialog();
            if (selectDialog.ShowDialog(this, columns) != DialogResult.OK) { return; }
            var selectedColumns = selectDialog.SelectedColumns.ToList();

            if (sfdExport.ShowDialog(this) != DialogResult.OK) { return; }
            var fileName = sfdExport.FileName;

            CsvExporter.ExportCsv(fileName, flattened, selectedColumns);
        }
    }
}
