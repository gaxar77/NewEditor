using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Microsoft.Win32;
using System;

namespace WindowsNewFileContextMenuEditor
{
    public class FileTypeDetails
    {
        public string Name { get; set; }
        public string Extension { get; set; }
    }
    public class Logic
    {
        public enum ErrorCode
        {
            Success,
            Aborted,
            InvalidFileType,
            FileTypeInNewMenuGlobally,
            FileTypeAlreadyOnList,
            FileTypeHasHandler
        }
        private RegistryKey OpenCurrentUserClassesKey()
        {
            return Registry.CurrentUser.OpenSubKey(@"Software\Classes", true);
        }

        public ErrorCode AddFileTypeToNewMenu(FileTypeModel model, FileTypeDetails[] fileTypesOnList)
        {
            if (!IsValidFileType(model.Extension))
            {
                return ErrorCode.InvalidFileType;
            }
            else if (fileTypesOnList != null &&
                     IsFileTypeInNewMenuGlobally(model.Extension))
            {
                return ErrorCode.FileTypeInNewMenuGlobally;
            }
            else if (fileTypesOnList != null &&
                     fileTypesOnList.Select(d => d.Extension)
                        .FirstOrDefaultStringIgnoreCase(
                    model.Extension) != null)
            {
                return ErrorCode.FileTypeAlreadyOnList;
            }

            using (var classesKey = OpenCurrentUserClassesKey())
            using (var fileTypeKey = classesKey.CreateSubKey(
                model.Extension, true))
            {
                using (var shellNewKey = fileTypeKey.CreateSubKey("ShellNew", true))
                {
                    switch (model.Action)
                    {
                        case FileCreationAction.CreateNullFile:
                            shellNewKey.SetValue("NullFile", "");
                            break;
                        case FileCreationAction.CreateFileFromTemplate:
                            shellNewKey.SetValue("FileName", model.Path);
                            break;
                        case FileCreationAction.LaunchApplication:
                            shellNewKey.SetValue("Command", model.Path);
                            break;
                    }
                }
            }

            return ErrorCode.Success;
        }
        public void RemoveFileTypeFromNewMenu(string extension, 
            Func<bool> confirmRemoveWithHandler)
        {
            if (FileTypeHasHandler(extension))
            {
                if (!confirmRemoveWithHandler())
                {
                    return;
                }
            }

            using (var classesKey = OpenCurrentUserClassesKey())
            using (var fileTypeKey = classesKey.OpenSubKey(extension, true))
            {
                fileTypeKey.DeleteSubKeyTree("ShellNew");
            }

        }

        public bool IsValidFileType(string extension)
        {
            if (extension.Length < 2 || extension[0] != '.')
                return false;

            using (var classesKey = OpenCurrentUserClassesKey())
            using (var fileTypeKey = classesKey.OpenSubKey(extension))
            {
                if (fileTypeKey != null)
                    return true;
            }

            using (var fileTypeKey = Registry.ClassesRoot.OpenSubKey(extension))
            {
                if (fileTypeKey != null)
                    return true;
            }

            return false;
        }

        public bool IsFileTypeInNewMenuGlobally(string extension)
        {
            using (var fileTypeKey = Registry.ClassesRoot.OpenSubKey(extension))
            using (var shellNewKey = fileTypeKey.OpenSubKey("ShellNew"))
            {
                return shellNewKey != null;
            }
        }

        public bool FileTypeHasHandler(string extension)
        {
            var shellNewKeyName = extension + @"\ShellNew";
            using (var classesKey = OpenCurrentUserClassesKey())
            using (var shellNewKey = classesKey.OpenSubKey(shellNewKeyName))
            {
                return shellNewKey.GetValue("Handler") != null;
            }
        }

        public FileTypeDetails[] GetAllFileTypeDetails()
        {
            var fileTypeDetailsList = new List<FileTypeDetails>();

            var subKeyNames = Registry.ClassesRoot.GetSubKeyNames();

            foreach (var subKeyName in subKeyNames)
            {
                if (subKeyName[0] == '.')
                {
                    using (var subKey = Registry.ClassesRoot.OpenSubKey(subKeyName))
                    {       
                        var className = subKey.GetValue(null, String.Empty).ToString();
                        var fileTypeDetails = new FileTypeDetails();
                        fileTypeDetails.Extension = subKeyName;

                        if (className == String.Empty)
                        {
                            fileTypeDetails.Name = String.Empty;
                        }
                        else
                        {
                            using (var fileTypeKey = Registry.ClassesRoot.OpenSubKey(className))
                            {
                                if (fileTypeKey != null)
                                {
                                    fileTypeDetails.Name =
                                        fileTypeKey.GetValue(null, String.Empty).ToString();
                                }
                                else
                                {
                                    fileTypeDetails.Name = String.Empty;
                                }
                            }
                        }

                        fileTypeDetailsList.Add(fileTypeDetails);
                    }
                }
            }

            return fileTypeDetailsList.ToArray();
        }
        public FileTypeDetails[] GetFileTypesInNewMenu()
        {
            var fileTypes = new List<FileTypeDetails>();
            using (var classesKey = OpenCurrentUserClassesKey())
            {
                var keyNames = classesKey.GetSubKeyNames();

                foreach (var keyName in keyNames)
                {
                    if (keyName[0] == '.')
                    {
                        using (var subKey = classesKey.OpenSubKey(keyName))
                        {
                            if (subKey.GetSubKeyNames().FirstOrDefaultStringIgnoreCase(
                                    "ShellNew") != null)
                            {
                                var className = subKey.GetValue(null, String.Empty).ToString();
                                var fileTypeName = className;

                                if (className != String.Empty)
                                {
                                    using (var fileTypeKey = Registry.ClassesRoot.OpenSubKey(className))
                                    {
                                        if (fileTypeKey != null)
                                        {
                                            fileTypeName = fileTypeKey.GetValue(null, String.Empty).ToString();
                                        }
                                    }
                                }
                                var fileTypeDetails = new FileTypeDetails()
                                {
                                    Name = fileTypeName,
                                    Extension = keyName
                                };

                                fileTypes.Add(fileTypeDetails);
                            }
                        }
                    }
                }
            }

            return fileTypes.ToArray();
        }

        public ErrorCode ModifyFileTypeInNewMenu(string extension,
            FileTypeDetails[] fileTypesOnList,
            Func<FileTypeModel, FileTypeModel> updateFileTypeModelFunc,
            Func<bool> confirmOverwriteCommandAndTemplateFunc)
        {
            if (FileTypeHasHandler(extension))
            {
                return ErrorCode.FileTypeHasHandler;
            }

            var shellNewKeyName = extension + @"\ShellNew";

            using (var classesKey = OpenCurrentUserClassesKey())
            using (var shellNewKey = classesKey.OpenSubKey(shellNewKeyName, true))
            {
                var nullFileValue = shellNewKey.GetValue("NullFile") as string;
                var fileNameValue = shellNewKey.GetValue("FileName") as string;
                var commandValue = shellNewKey.GetValue("Command") as string;

                var model = new FileTypeModel();

                model.Extension = extension;

                if (nullFileValue != null)
                {
                    model.Action = FileCreationAction.CreateNullFile;
                }

                if (fileNameValue != null && commandValue != null)
                {
                    if (!confirmOverwriteCommandAndTemplateFunc())
                        return ErrorCode.Aborted;
                }

                if (fileNameValue != null)
                {
                    model.Action = FileCreationAction.CreateFileFromTemplate;
                    model.Path = fileNameValue;
                }

                if (commandValue != null)
                {
                    model.Action = FileCreationAction.LaunchApplication;
                    model.Path = commandValue;
                }

                if ((model = updateFileTypeModelFunc(model)) != null)
                {
                    if (fileNameValue != null)
                    {
                        shellNewKey.DeleteValue("FileName");
                    }

                    if (commandValue != null)
                    {
                        shellNewKey.DeleteValue("Command");
                    }

                    shellNewKey.Dispose();
                    AddFileTypeToNewMenu(model, null);
                }

                return ErrorCode.Success;
            }
        }
    }
}