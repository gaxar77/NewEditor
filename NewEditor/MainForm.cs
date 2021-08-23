using System;
using System.Linq;
using System.Windows.Forms;

namespace Gaxar77.NewEditor
{
    public partial class MainForm : Form
    {
        Logic _logic = new Logic();

        public MainForm()
        {
            InitializeComponent();
            lvFileTypes.Columns.Clear();
            lvFileTypes.Columns.Add(new ColumnHeader() { Text = "Type" , Width = 200 });
            lvFileTypes.Columns.Add(new ColumnHeader() { Text = "Extension" });
            lvFileTypes.FullRowSelect = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var fileTypeDetailsArray =_logic.GetFileTypesInNewMenu();
            foreach (var fileTypeDetails in fileTypeDetailsArray)
            {
                var detailsItem = new ListViewItem(fileTypeDetails.Name);
                detailsItem.SubItems.Add(fileTypeDetails.Extension);

                lvFileTypes.Items.Add(detailsItem);
            }
        }

        private FileTypeDetails[] GetFileTypeDetailsFromFileTypeView()
        {
            return lvFileTypes.Items.OfType<ListViewItem>().Select(
                item => new FileTypeDetails()
                {
                    Name = item.SubItems[0].Text,
                    Extension = item.SubItems[1].Text
                }).ToArray();
        }
        private void btnAddFileType_Click(object sender, EventArgs e)
        {
            using (var addFileTypeForm = new FileTypeForm())
            {
                if (addFileTypeForm.ShowDialog() == DialogResult.OK)
                {
                    var errorCode = _logic.AddFileTypeToNewMenu(addFileTypeForm.GetModel(),
                        GetFileTypeDetailsFromFileTypeView());

                    switch(errorCode)
                    {
                        case Logic.ErrorCode.InvalidFileType:
                            MessageBox.Show("There is no file type registered on the system with that extension.");
                            break;
                        case Logic.ErrorCode.FileTypeAlreadyOnList:
                            MessageBox.Show("That file type is already on the list.");
                            break;
                        case Logic.ErrorCode.FileTypeInNewMenuGlobally:
                            MessageBox.Show("That file type is either in the new menu globally on the system " +
                                "or already in the new menu for the current user. " +
                                "You cannot add it to the new menu.");
                            break;
                        case Logic.ErrorCode.Success:
                            var model = addFileTypeForm.GetModel();

                            var item = new ListViewItem(
                                model.FileType);

                            item.SubItems.Add(model.Extension);

                            lvFileTypes.Items.Add(item);
                            break;
                    }
                }
            }
        }

        public bool ConfirmRemoveFileTypeFromNewMenuWithHandler()
        {
            return MessageBox.Show(
                        "This file type has a handler associated with it. " +
                        "If you remove it, you will not be able to retrieve this functionality. " +
                        "Are you sure you want to continue?",
                        "Confirm",
                       MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
   
        private void btnRemoveFileType_Click(object sender, EventArgs e)
        {
            string fileExtension = lvFileTypes.SelectedItems[0].SubItems[1].Text;
            
            _logic.RemoveFileTypeFromNewMenu(fileExtension,
                ConfirmRemoveFileTypeFromNewMenuWithHandler);

            lvFileTypes.Items.Remove(
                lvFileTypes.Items.OfType<ListViewItem>()
                    .Single(i => i.SubItems[1].Text == fileExtension));
        }

        private void lvFileTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveFileType.Enabled = true;
            btnModifyFileType.Enabled = true;
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A control panel applet allowing you to modify the New File menu in Windows Explorer for the currently logged in user. Created by Guido Arbia (g.arbia777@gmail.com)", "About NewEdit 1.0");
        }

        private void btnModifyFileType_Click(object sender, EventArgs e)
        {
            var errorCode = _logic.ModifyFileTypeInNewMenu(lvFileTypes.SelectedItems[0].SubItems[1].Text,
                    GetFileTypeDetailsFromFileTypeView(),
                    (FileTypeModel model) =>
                    {
                        var modifyFileTypeForm = new FileTypeForm();
                        modifyFileTypeForm.SetPropertiesFromModel(model);
                        modifyFileTypeForm.IsEditDialog = true;

                        if (modifyFileTypeForm.ShowDialog() == DialogResult.OK)
                        {
                            return modifyFileTypeForm.GetModel();
                        }
                        else
                        {
                            return null;
                        }
                    },
                    ConfirmOverwriteCommandAndTemplate);

            if (errorCode == Logic.ErrorCode.FileTypeHasHandler)
            {
                MessageBox.Show("You cannot modify this file type.");
            }
        }

        public bool ConfirmOverwriteCommandAndTemplate()
        {
            return MessageBox.Show("This file type has both a template and command value. " +
                       "These will be erased and overwritten. Do you want to continue?",
                       "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes;
        }
    }
}