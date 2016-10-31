using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SchetsEditor.Dialog
{
    class OpenImageDialog
    {
        private OpenFileDialog innerDialog { get; set; }

        public string FileName { get; private set; }


        public OpenImageDialog()
        {
            innerDialog = new OpenFileDialog();
            innerDialog.Filter = "Images|*.png;*.bmp;*.jpg;|All files (*.*)|*.*";
        }
        
        public DialogResult ShowDialog()
        {
            DialogResult result = innerDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.FileName = innerDialog.FileName;
            }
            return result;
        }
    }
}
