using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageAtlas
{
    public partial class ButtonList : UserControl
    {
        public int _startPosX    = 10;
        public int _startPosY    = 10;
        public int _lineSpacing  = 10;

        public int _buttonNumber = 0;

        public event EventHandler MyEvent;  //버튼이 눌릴때 발생할 이벤트

        public ButtonList()
        {
            InitializeComponent();
        }

        public void AddButton(string name)
        {
            Button button   = new Button();
            button.Text     = name;

            int posY = _startPosY;
            posY += (_buttonNumber * (_lineSpacing + button.Height));

            button.Location = new Point(_startPosX, posY);
            this.Controls.Add(button);

            _buttonNumber++;
            button.Tag = _buttonNumber;
            button.Click += Button_Click;
        }

        public void SetEventHandler(EventHandler eventHandler)
        {
            MyEvent = eventHandler;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if(null != MyEvent) MyEvent(sender, e);
        }
    }
}
