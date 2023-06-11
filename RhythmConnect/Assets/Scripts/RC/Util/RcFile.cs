using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace RC.Util
{
    class RcFile
    {
        /// <summary>
        /// 指定したフォルダのサブフォルダ一覧をListで返す。
        /// </summary>
        /// <param name="folderpath"></param>
        /// <returns>サブフォルダ一覧（フォルダがなければnull）</returns>
        public static List<string> LoadSubFolderToList(string folderpath)
        {
            List<string> Folders = new List<string>();

            try
            {
                string[] temp = Directory.GetDirectories(folderpath);
                foreach (string fn in temp)
                {
                    Folders.Add(fn);
                }
            }
            catch (DirectoryNotFoundException e)
            {
                return null;
            }

            return Folders;
        }

        /// <summary>
        /// 引数で受け取ったテキストファイルの内容を一行ずつListに読み込む。
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>テキストファイルの中身（ファイルがなければnull）</returns>
        public static List<string> LoadFileToList(string filename)
        {
            List<string> Lines = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    while (sr.EndOfStream == false)
                    {
                        Lines.Add(sr.ReadLine());
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                return null;
            }

            return Lines;
        }
    }
}