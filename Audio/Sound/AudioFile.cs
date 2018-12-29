using System.IO;

namespace MGS.Audio
{
    public abstract class AudioFile
    {
        /// <summary>
        /// ファイルパス
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// チャンネル数
        /// </summary>
        public int Channel { get; set; }
        /// <summary>
        /// モノラルか
        /// </summary>
        public bool IsMonoral
        {
            get
            {
                return Channel < 2;
            }
        }
        /// <summary>
        /// データ本体
        /// </summary>
        public short[] Samples { get; set; }
        /// <summary>
        /// サンプリングレート
        /// </summary>
        public int SampleRate { get; set; }
        /// <summary>
        /// 長さ(ms)
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name="Filepath"></param>
        protected abstract AudioFile Read(string Filepath);

        /// <summary>
        /// ファイル読み込み
        /// </summary>
        /// <param name="FilePath">読み込むファイルへのパス</param>
        public AudioFile Load(string FilePath)
        {
            var RawFilePath = FilePath;
            var Extension = Path.GetExtension(FilePath);

            //oggとwavのファイル存在確認
            //ToDO mp3を読み込み順位最低で処理
            var UseFilePath = RawFilePath;
            if (!File.Exists(RawFilePath))
            {
                //wavのみ対応
                //ToDO:ogg・mp3に対応する
                if (Extension != ".wav") throw new FileNotFoundException("対応音声ファイル拡張子はwavのみです(oggとmp3に対応予定)");
                //UseFilePath = FilePath.Replace(Extension, Extension == ".wav" ? ".ogg" : ".wav");
                //Extension = Extension == ".wav" ? ".ogg" : ".wav";
                //if (!File.Exists(LoadFilePath))
                //{
                //    //どちらも存在しないならmp3を検索
                //    LoadFilePath = FilePath.Replace(Extension, ".mp3");
                //    if (!File.Exists(LoadFilePath))
                //    {
                //        throw new FileNotFoundException("音声ファイルが見つかりません");
                //    }
                //    Extension = ".mp3";
                //}

                if (!File.Exists(UseFilePath)) throw new FileNotFoundException($"音声ファイル:{UseFilePath}が存在しないかファイルパスが間違っています。");
            }

            return Read(UseFilePath);
        }
    }
}
