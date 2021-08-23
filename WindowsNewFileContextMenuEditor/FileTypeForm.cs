using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsNewFileContextMenuEditor
{
    public partial class FileTypeForm : Form
    {
        Logic _logic = new Logic();
        FileTypeDetails[] _fileTypeDetailsArray;
        public FileTypeForm()
        {
            InitializeComponent();
        }

        //Todo: Add regular expression checking for actual valid extension.
        private void SetOkayButtonEnabled()
        {
            /*
            var isValidExtension =
                txtFileExtension.Text.Length > 1 &&
                txtFileExtension.Text[0] == '.';
            */
            //btnOkay.Enabled = isValidExtension;
        }

        private void txtFileExtension_TextChanged(object sender, EventArgs e)
        {
            SetOkayButtonEnabled();
        }

        private void txtFileExtension_KeyPress(object sender, KeyPressEventArgs e)
        {
            SetOkayButtonEnabled();
        }

        public string Extension
        {
            get { return cmbFileExtension.Text; }
            set { cmbFileExtension.Text = value; }
        }
        
        public string FileType
        {
            get { return cmbFileType.Text; }
        }
        public FileCreationAction Action
        {
            get
            {
                return (FileCreationAction) cmbAction.SelectedIndex;
            }

            set
            {
                cmbAction.SelectedIndex = (int)value;
            }
        }

        public string Path
        {
            get { return txtPath.Text; }
            set { txtPath.Text = value; }
        }

        public bool IsEditDialog { get; set; }

        private void AddFileTypeForm_Load(object sender, EventArgs e)
        {
            var extension = Extension;
            if (cmbAction.SelectedIndex == -1)
            {
                cmbAction.SelectedIndex = (int)FileCreationAction.CreateNullFile;
            }

            if (IsEditDialog)
            {
                Text = "Modify File Type";
                cmbFileExtension.Enabled = false;
                cmbFileType.Enabled = false;
            }

            _fileTypeDetailsArray = _logic.GetAllFileTypeDetails();

            foreach (var fileTypeDetails in _fileTypeDetailsArray)
            {
                cmbFileType.Items.Add(fileTypeDetails.Name);
                cmbFileExtension.Items.Add(fileTypeDetails.Extension);
            }

            cmbFileExtension.Text = extension;
        }

        private void cmbAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPath.Enabled = cmbAction.SelectedIndex > 
                (int)FileCreationAction.CreateNullFile;

            if (cmbAction.SelectedIndex == (int)FileCreationAction.LaunchApplication)
            {
                lblPath.Text = "Command:";
            }
            else
            {
                lblPath.Text = "Template Path:";
            }
        }

        private void cmbFileType_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFileExtension.SelectedIndex = cmbFileType.SelectedIndex;
        }

        private void cmbFileExtension_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFileType.SelectedIndex = cmbFileExtension.SelectedIndex;

            btnOkay.Enabled = cmbFileExtension.SelectedIndex > -1;
        }

        private void cmbFileType_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cmbFileType_TextChanged(object sender, EventArgs e)
        {
        }

        private void cmbFileType_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void cmbFileExtension_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void cmbFileExtension_TextChanged(object sender, EventArgs e)
        {
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
