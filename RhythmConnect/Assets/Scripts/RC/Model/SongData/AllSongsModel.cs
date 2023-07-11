using System.IO;
using System.Collections.Generic;
using RC.Model;
using RC.Interface;

namespace RC.Model
{
    class AllSongsModel
    {
        public List<FolderModel> RcFolders { get; private set; }
        public List<SongModel> UserSongs { get; private set; }
        private IAllSongsRepository repository;

        public AllSongsModel(IAllSongsRepository repository)
        {
            this.repository = repository;
        }

        public void LoadRCSongData()
        {
            this.RcFolders = repository.GetRCSongData();
        }

        public void LoadUserSongData()
        {
            this.UserSongs = repository.GetUserSongData();
        }
    }
}