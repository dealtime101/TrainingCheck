using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrainingCheck
{
    public partial class Main : Form
    {
        private Dictionary<int, bool> pendingCheckStates = new Dictionary<int, bool>();

        public Main()
        {
            InitializeComponent();

            checkedListBox1.Items.Add("Master Excel Functions: Unlock the Power of Formulas for Enhanced Data Analysis");
            checkedListBox1.Items.Add("Excel's Visual Insights: Crafting Dynamic Charts for Data Analysis");

            checkedListBox1.DrawItem += CheckedListBox1_DrawItem;
            checkedListBox1.ItemCheck += CheckedListBox1_ItemCheck;

            checkedListBox1.DrawMode = DrawMode.OwnerDrawFixed;
        }

        private void CheckedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Update the pending state for the item being checked/unchecked
            pendingCheckStates[e.Index] = e.NewValue == CheckState.Checked;

            // Force the redrawing of the item when its check state changes
            checkedListBox1.Invalidate();
        }

        private void CheckedListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string text = checkedListBox1.Items[e.Index].ToString();
            bool isChecked = pendingCheckStates.ContainsKey(e.Index) ? pendingCheckStates[e.Index] : checkedListBox1.GetItemChecked(e.Index);

            // Apply the strikethrough style if the item is checked
            Font font = isChecked ? new Font(e.Font, FontStyle.Strikeout) : e.Font;

            // Draw the background and the text
            e.DrawBackground();
            using (Brush textBrush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, font, textBrush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }
    }
}
