using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;
using System.IO;

namespace StampLib.util
{
    public class IO
    {
        /// <summary>
        /// 標準入力から文字列を読み取り、お手本およびスタンプ一覧をセットする。
        /// </summary>
        public static Instance InputProblemFromConsole()
        {
            Field field = new Field();
            List<Stamp> stamp_list = new List<Stamp>();
            Instance instance = new Instance();

            // フィールドの読み取り
            field.SetTargetField(Console.ReadLine());

            // スタンプオブジェクトの読み取り
            short count = 0;
            while (true)
            {
                string buf_str = Console.ReadLine();
                if (buf_str == "" || buf_str == null)
                {
                    break;
                }
                Stamp tmp_stamp = new Stamp(count++, buf_str);
                stamp_list.Add(tmp_stamp);
            }

            instance.SetOriginStampObject(stamp_list);
            instance.SetField(field);

            return instance;
        }

        /// <summary>
        /// 解の情報を受け取り標準出力に出力する
        /// </summary>
        /// <param name="solution"></param>
        public static void OutputSolutionToConsole(Solution solution)
        {
            var answer_list = new List<Tuple<short, short, short>>();
            foreach (var pressing_info in solution.GetStampAnswerList())
            {
                Stamp combined_stamp = pressing_info.Item1;
                short slide_x = pressing_info.Item2;
                short slide_y = pressing_info.Item3;

                // スタンプを構成するorigin stampを平行移動したのちanswer_listに追加
                foreach (var origin_stamp in combined_stamp.GetOriginStampList())
                {
                    var after_stamp = new Tuple<short, short, short>(origin_stamp.Item1,
                                                                     (short)(origin_stamp.Item2 + slide_x),
                                                                     (short)(origin_stamp.Item3 + slide_y));
                    // スタンプがフィールド外にある場合には解に追加しない
                    // combined stamp を使わない方針になりそうなので、いったんここのチェックをはずす
                    /*
                    if (after_stamp.Item2 > Field.x_size || after_stamp.Item3 > Field.y_size)
                    {
                        continue;
                    }
                    */
                    answer_list.Add(after_stamp);
                }
            }

            // スタンプを押す回数を出力
            short len_answer_list = (short)answer_list.Count();
            Console.WriteLine(len_answer_list);

            // スタンプの押し方を出力
            for (short i = 0; i < len_answer_list; ++i)
            {
                short stamp_number = answer_list[i].Item1;
                short slide_x = answer_list[i].Item2;
                short slide_y = answer_list[i].Item3;
                string end_line = "";
                if (i == len_answer_list - 1)
                {
                    end_line = "\n";
                }
                Console.WriteLine(stamp_number + ";" + slide_x + "," + slide_y + end_line);
            }
        }

        /// <summary>
        /// 指定されたパス配下のtxtファイルをすべて読み込み、Instanceのリストを生成する
        /// </summary>
        /// <param name="folder_path">txtファイルのあるフォルダパス</param>
        /// <returns>Instanceのリスト</returns>
        public List<Instance> InputProblemFromFolderPath(string folder_path)
        {
            var ret_list = new List<Instance>();

            // folder_path直下に存在するファイル名一覧を取得
            string[] files = System.IO.Directory.GetFiles(folder_path, "*.txt", System.IO.SearchOption.AllDirectories);

            foreach ( var file in files )
            {
                StreamReader sr = new StreamReader(file, Encoding.GetEncoding("Shift_JIS"));

                try
                {
                    var field = new Field();
                    var stamp_list = new List<Stamp>();
                    var instance = new Instance();

                    // フィールドの読み取り
                    field.SetTargetField(sr.ReadLine());

                    // スタンプオブジェクトの読み取り
                    short count = 0;
                    while (true)
                    {
                        string buf_str = sr.ReadLine();
                        if (buf_str == "" || buf_str == null)
                        {
                            break;
                        }
                        Stamp tmp_stamp = new Stamp(count++, buf_str);
                        stamp_list.Add(tmp_stamp);
                    }

                    instance.SetOriginStampObject(stamp_list);
                    instance.SetField(field);
                    ret_list.Add(instance);
                }
                catch (Exception e)
                {
                    Console.WriteLine("エラー： " + file + " の初期化に失敗しました。");
                    continue;
                }
                finally
                {
                    sr.Close();
                }
            }

            return ret_list;
        }
    }
}
