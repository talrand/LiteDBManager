using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiteDBManager.Controls
{
    public partial class ClosableTabControl : UserControl
    {
        private Point _imageLocation = new Point(13, 5);
        private Point _imgHitArea = new Point(13, 2);

        public ClosableTabControl()
        {
            InitializeComponent();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Image img = new Bitmap(imgList.Images[0]);
                Rectangle rectangle = e.Bounds;
                rectangle = this.tabControl1.GetTabRect(e.Index);
                rectangle.Offset(2, 2);
                Brush TitleBrush = new SolidBrush(Color.Black);
                Font font = this.Font;
                string title = this.tabControl1.TabPages[e.Index].Text;

                e.Graphics.DrawString(title, font, TitleBrush, new PointF(rectangle.X, rectangle.Y));

                if (tabControl1.SelectedIndex >= 1)
                {
                    e.Graphics.DrawImage(img, new Point(rectangle.X + (this.tabControl1.GetTabRect(e.Index).Width - _imageLocation.X), _imageLocation.Y));
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);            
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                TabControl tc = (TabControl)sender;
                Point p = e.Location;
                int _tabWidth = 0;
                _tabWidth = this.tabControl1.GetTabRect(tc.SelectedIndex).Width - (_imgHitArea.X);
                Rectangle r = this.tabControl1.GetTabRect(tc.SelectedIndex);
                r.Offset(_tabWidth, _imgHitArea.Y);
                r.Width = 16;
                r.Height = 16;
                if (tabControl1.SelectedIndex >= 1)
                {
                    if (r.Contains(p))
                    {
                        TabPage TabP = (TabPage)tc.TabPages[tc.SelectedIndex];
                        tc.TabPages.Remove(TabP);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateNewQueryPane(string queryText = "")
        {
            try
            {
                // Create new query tab
                TabPage queryTab = new TabPage() { Text = $"Query {tabControl1.TabPages.Count + 1}" };

                // Create new query pane
                QueryPane queryPane = new QueryPane() { Dock = DockStyle.Fill };
                queryPane.SetQueryText(queryText);
                queryTab.Controls.Add(queryPane);

                // Add to new tab
                tabControl1.Controls.Add(queryTab);

                // Select new tab
                tabControl1.SelectedTab = queryTab;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}