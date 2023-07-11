using System.IO;
using UnityEngine;

namespace RC.Util
{
    public class RcImage
    {
        /// <summary>
        /// 画像を読んでくるための奴
        /// Png用っぽい。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] ReadPngFile(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryReader bin = new BinaryReader(fileStream);
                byte[] values = bin.ReadBytes((int)bin.BaseStream.Length);
                bin.Close();
                return values;
            }
        }

        /// <summary>
        /// バイナリで取ってきた画像データを読むメソッドぽい
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Texture2D ReadByBinary(byte[] bytes)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return texture;
        }
    }
}