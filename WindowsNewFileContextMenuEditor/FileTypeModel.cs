using System;

namespace WindowsNewFileContextMenuEditor
{
    public class FileTypeModel
    {
        public string Extension { get; set; } = String.Empty;
        public string FileType { get; set; } = String.Empty;
        public FileCreationAction Action { get; set; } = FileCreationAction.CreateNullFile;
        public string Path { get; set; } = String.Empty;
    }
}