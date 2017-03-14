using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FT232_SVF_Player
{
    public enum ProgressBarDisplayText
    {
        Percentage,
        CustomText
    }

    // Straight from Stackoverflow :)
    class CustomProgressBar : ProgressBar
    {
        //Property to set to decide whether to print a % or Text
        public ProgressBarDisplayText DisplayStyle { get; set; }

        //Property to hold the custom text
        public string CustomText { get; set; }
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Font TextFont { get; set; }
        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
        public Brush TextBrush { get; set; }

        public CustomProgressBar()
        {
            // Modify the ControlStyles flags
            //http://msdn.microsoft.com/en-us/library/system.windows.forms.controlstyles.aspx
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            TextFont = new Font(FontFamily.GenericSansSerif, 10);
            TextBrush = Brushes.Black;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;

            ProgressBarRenderer.DrawHorizontalBar(g, rect);
            rect.Inflate(-3, -3);
            if (Value > 0)
            {
                // As we doing this ourselves we need to draw the chunks on the progress bar
                Rectangle clip = new Rectangle(rect.X, rect.Y, (int)Math.Round(((float)Value / Maximum) * rect.Width), rect.Height);
                ProgressBarRenderer.DrawHorizontalChunks(g, clip);
            }

            // Set the Display text (Either a % amount or our custom text
            string text = DisplayStyle == ProgressBarDisplayText.Percentage ? (Value / 10).ToString() + '%' : CustomText;

            SizeF len = g.MeasureString(text, TextFont);
            // Calculate the location of the text (the middle of progress bar)
            // Point location = new Point(Convert.ToInt32((rect.Width / 2) - (len.Width / 2)), Convert.ToInt32((rect.Height / 2) - (len.Height / 2)));
            Point location = new Point(Convert.ToInt32((Width / 2) - len.Width / 2), Convert.ToInt32((Height / 2) - len.Height / 2));
            // The commented-out code will centre the text into the highlighted area only. This will centre the text regardless of the highlighted area.
            // Draw the custom text
            g.DrawString(text, TextFont, TextBrush, location);

        }
    }
}
