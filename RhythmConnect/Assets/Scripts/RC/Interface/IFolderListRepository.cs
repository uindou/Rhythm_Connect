using System;
using System.Collections.Generic;
using RC.Model;

namespace RC.Interface
{
    interface IFolderListRepository
    {
        List<FolderModel> LoadData();
    }
}