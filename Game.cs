using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odev1Deneme
{
    class Game
    {
        public int uzaklikBelirleme(int tasY, int tasX,int kX, int kY)
        {
            int uzaklik=Math.Abs(kX - tasX) + Math.Abs(kY - tasY);
            return uzaklik;
        }
        public void oyunYenile(Control blueCtrl, Control redCtrl)
        {
            Button buton = new Button();
            buton = (Button)blueCtrl;
            buton.ImageKey = "";
            buton.ImageList = null;
            buton.Text = "";
            buton = (Button)redCtrl;
            buton.ImageKey = "";
            buton.ImageList = null;
            buton.Text = "";
        }
        
    }
}
