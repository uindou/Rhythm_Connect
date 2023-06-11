using System.IO;
using System.Collections.Generic;
using RC.Model;
using RC.Interface;

namespace RC.Model
{
    class FolderListModel
    {
        public List<FolderModel> folders { get; private set; }
        private IFolderListRepository repository;

        public FolderListModel(IFolderListRepository repository)
        {
            this.repository = repository;
            this.folders = repository.LoadData();
        }
    }
}