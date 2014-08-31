using System.Windows.Forms;

namespace DisabledControlInDataGridView
{
    public class DataGridViewDisableCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        public DataGridViewDisableCheckBoxColumn()
        {
            CellTemplate = new DataGridViewDisableCheckBoxCell();
        }

        public override sealed DataGridViewCell CellTemplate
        {
            get { return base.CellTemplate; }
            set { base.CellTemplate = value; }
        }
    }
}