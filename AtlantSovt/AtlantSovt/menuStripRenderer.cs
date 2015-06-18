using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtlantSovt
{
    class menuStripRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs myMenu)
        {
            if (!myMenu.Item.Selected)
                base.OnRenderMenuItemBackground(myMenu);
            else
            {
                Rectangle menuRectangle = new Rectangle(Point.Empty, myMenu.Item.Size);
                //Fill Color
                myMenu.Graphics.FillRectangle(Brushes.AliceBlue, menuRectangle);
                // Border Color
                myMenu.Graphics.DrawRectangle(Pens.AliceBlue, 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1);
            }
        }
    }
}
