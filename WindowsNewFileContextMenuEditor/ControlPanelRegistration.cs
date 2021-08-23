using Microsoft.Win32;

namespace WindowsNewFileContextMenuEditor
{
    class ControlPanelRegistration
    {
        const string ControlPanelNamespaceId = "{20DCE34F-2141-42D5-802E-5E328899586B}";
        const string ControlPanelNamespaceKeyName = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\Namespace\" +
                ControlPanelNamespaceId;
        const string ClsidKeyName = @"CLSID\" + ControlPanelNamespaceId;

        public static bool IsRegistered()
        {
            using (var namespaceKey = Registry.LocalMachine.OpenSubKey(
                ControlPanelNamespaceKeyName))
            {
                return (namespaceKey != null);
            }
        }

        public static void Register()
        {
            using (var namespaceKey = Registry.LocalMachine.CreateSubKey(
                    ControlPanelNamespaceKeyName, true))
            {
                namespaceKey.SetValue("", "New File Context Menu");
            }

            using (var clsidKey = Registry.ClassesRoot.CreateSubKey(
                ClsidKeyName, true))
            {
                clsidKey.SetValue("", "New File Context Menu");
                clsidKey.SetValue("LocalizedString", "New File Context Menu");
                clsidKey.SetValue("InfoTip", "Add/remove items in the new file context menu.");
                clsidKey.SetValue("System.ApplicationName", "Gaxar.NewFileContextMenuEditor");
                clsidKey.SetValue("System.ControlPanel.Category", "0");

                using (var commandKey = clsidKey.CreateSubKey(
                    @"Shell\Open\Command", true))
                {
                    commandKey.SetValue("", System
                        .Reflection
                        .Assembly
                        .GetExecutingAssembly()
                        .Location,
                        RegistryValueKind.ExpandString);
                }
            }
        }

        public static void Unregister()
        {
            using (var namespaceKey = Registry.LocalMachine.OpenSubKey(
                ControlPanelNamespaceKeyName))
            {
                if (namespaceKey != null)
                {
                    Registry.LocalMachine.DeleteSubKeyTree(ControlPanelNamespaceKeyName);
                }
            }

            using (var clsidKey = Registry.ClassesRoot.OpenSubKey(ClsidKeyName))
            {
                if (clsidKey != null)
                {
                    Registry.ClassesRoot.DeleteSubKeyTree(ClsidKeyName);
                }
            }
        }
    }
}
