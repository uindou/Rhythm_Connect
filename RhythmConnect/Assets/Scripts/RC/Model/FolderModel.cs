using System.IO;
using System.Collections.Generic;
using RC.Model;
using RC.Util;

namespace RC.Model
{
    class FolderModel
    {
        public List<SongModel> Songs { get; private set;}
        public string Name { get; private set; }
        public string Path { get; private set; }
        public string Description { get; private set; }

        public FolderModel(string name, string description, List<SongModel> songs)
        {
            this.Name = name;
            this.Description = description;
            this.Songs = songs;
        }
    }
}
