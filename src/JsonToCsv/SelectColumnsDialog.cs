using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JsonToCsv
{
    public partial class SelectColumnsDialog : Form
    {
        public SelectColumnsDialog()
        {
            InitializeComponent();
        }

        public IEnumerable<string> SelectedColumns
        {
            get { return lstColumns.CheckedItems.OfType<string>(); }
        }

        public DialogResult ShowDialog(IWin32Window owner, string[] columns)
        {
            lstColumns.Items.Clear();
            lstColumns.Items.AddRange(columns);
            SetAllSelected(true);
            return this.ShowDialog(owner);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            SetAllSelected(true);
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            SetAllSelected(false);
        }

        private void SetAllSelected(bool selected)
        {
            for (var x = 0; x < lstColumns.Items.Count; x++)
            {
                lstColumns.SetItemChecked(x, selected);
            }
        }
    }
}
