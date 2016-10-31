using System;
using System.IO;
using System.Windows.Forms;

namespace SchetsEditor.Dialog
{
    class SaveImageDialog
    {
        private SaveFileDialog innerDialog { get; set; }

        public enum ImageType { Png, Jpeg, Bmp, Schets }

        public ImageType SelectedImageType { get; private set; }
        public string FileName { get; private set; }

        public SaveImageDialog()
        {
            innerDialog = new SaveFileDialog();
            innerDialog.Filter = "Schets (*.schets)|*.schets|Png (*.png)|*.png|Bitmap (*.bmp)|*.bmp|Jpeg (*.jpg, *.jpeg)|*.jpg;*.jpeg";
        }

        public DialogResult ShowDialog()
        {
            DialogResult result = innerDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.FileName = innerDialog.FileName;

                string ext = Path.GetExtension(innerDialog.FileName);
                switch (ext)
                {
                    case ".png":
                        this.SelectedImageType = ImageType.Png;
                        break;
                    case ".jpg":
                        this.SelectedImageType = ImageType.Jpeg;
                        break;
                    case ".bmp":
                        this.SelectedImageType = ImageType.Bmp;
                        break;
                    case ".schets":
                        this.SelectedImageType = ImageType.Schets;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return result;
        }
    }
}
