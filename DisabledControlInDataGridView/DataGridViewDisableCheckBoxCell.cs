using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DisabledControlInDataGridView
{
    public class DataGridViewDisableCheckBoxCell : DataGridViewCheckBoxCell
    {
        private bool _enabledValue;

        public DataGridViewDisableCheckBoxCell()
        {
            _enabledValue = true;
        }

        public bool Enabled
        {
            get { return _enabledValue; }
            set { _enabledValue = value; }
        }

        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            var cell = (DataGridViewDisableCheckBoxCell) base.Clone();
            cell.Enabled = Enabled;
            if (cell.OwningRow != null)
                cell.ReadOnly = !Enabled;
            return cell;
        }

        // By default, enable the CheckBox cell.
        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // The checkBox cell is disabled, so paint the border,
            // background, and disabled checkBox for the cell.
            if (!_enabledValue)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    Brush cellBackground = new SolidBrush(Selected ? cellStyle.SelectionBackColor : cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

                var checkState = CheckState.Unchecked;
                if (formattedValue != null)
                {
                    if (formattedValue is CheckState)
                        checkState = (CheckState) formattedValue;
                    else if (formattedValue is bool)
                    {
                        if ((bool) formattedValue)
                            checkState = CheckState.Checked;
                    }
                }

                var state = checkState == CheckState.Checked
                    ? CheckBoxState.CheckedDisabled
                    : CheckBoxState.UncheckedDisabled;

                // Calculate the area in which to draw the checkBox.
                // force to unchecked!!
                var size = CheckBoxRenderer.GetGlyphSize(graphics, state);
                var center = new Point(cellBounds.X, cellBounds.Y);
                center.X += (cellBounds.Width - size.Width)/2;
                center.Y += (cellBounds.Height - size.Height)/2;
                // Draw the disabled checkBox.
                // We prevent painting of the checkbox if the Width,
                // plus a little padding, is too small.
                if (size.Width + 4 < cellBounds.Width)
                    CheckBoxRenderer.DrawCheckBox(graphics, center, state);
            }
            else
            {
                // The checkBox cell is enabled, so let the base class
                // handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
}