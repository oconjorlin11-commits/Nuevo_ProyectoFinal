using System;
using System.Data;
using System.Windows.Forms;

namespace Nuevo_Proyecto.Services.Helpers
{
    public static class ComboBoxBindingHelper
    {
        public static void Bind(ComboBox comboBox, object? dataSource, string displayMember, string valueMember)
        {
            if (comboBox == null)
                throw new ArgumentNullException(nameof(comboBox));

            comboBox.SuspendLayout();
            try
            {
                comboBox.DataSource = null;
                comboBox.DisplayMember = string.Empty;
                comboBox.ValueMember = string.Empty;
                comboBox.Items.Clear();

                if (dataSource == null)
                    return;

                if (dataSource is DataTable table)
                {
                    if (!table.Columns.Contains(displayMember) || !table.Columns.Contains(valueMember))
                    {
                        table = new DataTable();
                        if (string.Equals(displayMember, valueMember, StringComparison.OrdinalIgnoreCase))
                        {
                            table.Columns.Add(valueMember, typeof(string));
                        }
                        else
                        {
                            table.Columns.Add(valueMember, typeof(int));
                            table.Columns.Add(displayMember, typeof(string));
                        }
                    }

                    comboBox.DataSource = table;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                    comboBox.SelectedIndex = -1;
                    return;
                }

                try
                {
                    comboBox.DataSource = dataSource;
                    comboBox.DisplayMember = displayMember;
                    comboBox.ValueMember = valueMember;
                }
                catch (ArgumentException)
                {
                    comboBox.DataSource = null;
                    comboBox.Items.Clear();
                }
            }
            finally
            {
                comboBox.ResumeLayout();
            }
        }
    }
}
