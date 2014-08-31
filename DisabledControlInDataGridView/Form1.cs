using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DisabledControlInDataGridView
{
    internal partial class Form1 : Form
    {
        private readonly DataGridView _dataGridView1 = new DataGridView();

        private readonly List<Info> _list = new List<Info>(new[]
        {
            new Info(1, "a"),
            new Info(2, "c"),
            new Info(3, "b")
        });

        public Form1()
        {
            AutoSize = true;

            Load += Form1Load;
        }

        public override sealed bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = value; }
        }

        public void Form1Load(object sender, EventArgs e)
        {
            var column0 = new DataGridViewCheckBoxColumn();
            var column1 = new DataGridViewDisableCheckBoxColumn();
            column0.Name = "CheckBoxes";
            column1.Name = "DisableCheckBoxes";

            var tid = new DataGridViewTextBoxColumn {DataPropertyName = "Id"};
            _dataGridView1.Columns.Add(tid);
            var tname = new DataGridViewTextBoxColumn {DataPropertyName = "Name"};
            _dataGridView1.Columns.Add(tname);

            _dataGridView1.Columns.Add(column0);
            _dataGridView1.Columns.Add(column1);

            _dataGridView1.DataSource = _list;
            _dataGridView1.AutoSize = true;

            _dataGridView1.AllowUserToAddRows = false;
            _dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            _dataGridView1.CellValueChanged += DataGridView1CellValueChanged;
            _dataGridView1.CurrentCellDirtyStateChanged += DataGridView1CurrentCellDirtyStateChanged;
            Controls.Add(_dataGridView1);
        }

        // This event handler manually raises the CellValueChanged event
        // by calling the CommitEdit method.
        private void DataGridView1CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (_dataGridView1.IsCurrentCellDirty)
                _dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        // If a check box cell is clicked, this event handler disables
        // or enables the checkBox in the same row as the clicked cell.
        public void DataGridView1CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_dataGridView1.Columns[e.ColumnIndex].Name == "CheckBoxes")
            {
                var checkBoxCell =
                    (DataGridViewDisableCheckBoxCell) _dataGridView1.Rows[e.RowIndex].Cells["DisableCheckBoxes"];

                var checkCell =
                    (DataGridViewCheckBoxCell) _dataGridView1.Rows[e.RowIndex].Cells["CheckBoxes"];

                checkBoxCell.Enabled = !(Boolean) checkCell.Value;
                checkBoxCell.ReadOnly = (Boolean) checkCell.Value;

                _dataGridView1.Invalidate();
            }
        }

        #region Nested type: Info

        private class Info
        {
            public Info(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public string Name { get; set; }

            public int Id { get; set; }
        }

        #endregion
    }
}