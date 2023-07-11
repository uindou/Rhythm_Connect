using System;
using System.Collections.Generic;
using RC.Model;

namespace RC.Interface
{
    interface IAllSongsRepository
    {
        List<FolderModel> GetRCSongData();
        List<SongModel> GetUserSongData();
    }
}