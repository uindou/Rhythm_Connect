
namespace RC.Util
{
    class RcParam
    {
        public static readonly int BorderFullScore = 1000000;
        public static readonly int BorderSSS = 990000;
        public static readonly int BorderSS = 950000;
        public static readonly int BorderS = 900000;
        public static readonly int BorderA = 800000;
        public static readonly int BorderB = 700000;
        public static readonly int BorderC = 600000;

        // TODO : これもうちょい拡張してその行がパラメータorデータ行になってるかを判断させたい
        /// <summary>
        /// パラメータ分解用のSplit
        /// source内で初めて登場したsplitterの位置でsourceを二分する
        /// </summary>
        /// <param name="source">対象文字列</param>
        /// <param name="splitter">区切り文字</param>
        /// <returns>Splitメソッドと同様</returns>
        public static string[] SplitParam(string source, char splitter)
        {
            string[] rtn = new string[2];

            rtn[0] = source.Substring(0, source.IndexOf(splitter));
            rtn[1] = source.Substring(source.IndexOf(splitter) + 1);

            return rtn;
        }

        public static string CalcRank(int score)
        {
            string rank;

            if (score >= BorderFullScore) rank = "PERFECT";
            else if (score >= BorderSSS) rank = "SSS";
            else if (score >= BorderSS) rank = "SS";
            else if (score >= BorderS) rank = "S";
            else if (score >= BorderA) rank = "A";
            else if (score >= BorderB) rank = "B";
            else if (score >= BorderC) rank = "C";
            else rank = "D";

            return rank;
        }

        public static string TrimFolderPath(string folderPath)
        {
            return folderPath.Substring(folderPath.LastIndexOf('\\') + 1);
        }
    }
}