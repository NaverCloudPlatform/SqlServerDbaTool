using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HaTool.Global
{
    public static class ControlHelpers
    {
        public static void InvokeIfRequired<T>(this T control, Action<T> action) where T : ISynchronizeInvoke
        {
            if (control.InvokeRequired)
                control.Invoke(new Action(() => action(control)), null);
            else
                action(control);
        }

        public static void ButtonStatusChange(Button button, string value)
        {
            button.Text = value;
            if (button.Text.Equals("Requested", StringComparison.OrdinalIgnoreCase))
                button.Enabled = false;
            else
                button.Enabled = true;
        }

        public static void dgvSingleCheckBox(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var columnIndex = 0;
                var dataGridView = (DataGridView)sender;
                bool isChecked = false;
                if (e.ColumnIndex == columnIndex)
                {
                    if (dataGridView.EndEdit())
                    {
                        isChecked = (bool)dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                        if (isChecked)
                        {
                            foreach (DataGridViewRow row in dataGridView.Rows)
                            {
                                if (row.Index != e.RowIndex)
                                {
                                    row.Cells[columnIndex].Value = !isChecked;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        public static void dgvLineColorChange(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dataGridView = (DataGridView)sender;
                if (dataGridView.EndEdit())
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        bool.TryParse(row.Cells[0].Value.ToString(), out bool isChecked2);
                        if (isChecked2)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(232, 237, 242);
                        else
                            row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception) { }
        }

        public static void dgvDesign(DataGridView dataGridView)
        {
            try
            {
                dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
                dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView.GridColor = Color.FromArgb(187, 192, 196);
                dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 237, 242);
                dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;
                dataGridView.BackgroundColor = Color.White;
                dataGridView.EnableHeadersVisualStyles = false;
                dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView.ColumnHeadersHeight = 23;
                dataGridView.RowTemplate.Height = 23;
                dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            }
            catch (Exception) { }
        }

        public static int CheckBoxCheckedCnt(DataGridView view)
        {
            int cnt = 0;
            foreach (DataGridViewRow item in view.Rows)
            {
                if (bool.Parse(item.Cells["CheckBox"].Value.ToString()))
                {
                    cnt++;
                }
            }
            return cnt;
        }


    }



}
