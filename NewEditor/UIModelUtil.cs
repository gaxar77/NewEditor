using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gaxar77.NewEditor
{
    public static class UIModelUtil
    {
        public static void SetPropertiesFromModel(this FileTypeForm form, 
            FileTypeModel model)
        {
            form.Extension = model.Extension;
            form.Action = model.Action;
            form.Path = model.Path;
        }

        public static FileTypeModel GetModel(this FileTypeForm form)
        {
            return new FileTypeModel()
            {
                FileType = form.FileType,
                Extension = form.Extension,
                Action = form.Action,
                Path = form.Path
            };
        }
    }
}
