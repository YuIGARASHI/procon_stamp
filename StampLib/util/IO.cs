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
            var stamp_answer_list = solution.GetStampAnswerList();
            foreach (var stamp_answer in stamp_answer_list)
            {
                Stamp stamp = stamp_answer.Item1;
                short slide_x = stamp_answer.Item2;
                short slide_y = stamp_answer.Item3;
                answer_list.Add(new Tuple<short, short, short>(stamp.GetOriginStampIndex(), slide_x, slide_y));
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
