using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using procon_stamp.model;

namespace procon_stamp.util {
    class IO {
        // Stampクラスのオブジェクトを格納するリスト
        static List<Stamp> stamp_object_list;

        // 静的コンストラクタ
        static IO()
        {
            stamp_object_list = new List<Stamp>();
        }

        // 標準入力から文字列を読み取り、Fieldインスタンス・Stampインスタンスを生成する
        static void InputProblem()
        {
            // ターゲットフィールドの読み取り
            Field.SetTargetField(Console.ReadLine());

            // スタンプオブジェクトの読み取り
            int count = 0;
            while ( true )
            {
                string buf_str = Console.ReadLine();
                if (buf_str == "" || buf_str == null)
                {
                    break;
                }
                Stamp stamp_object  = new Stamp(count++, buf_str);
                stamp_object_list.Add(stamp_object);
            }

            return;
        }

        // 解の情報を受け取り標準出力に出力する
        static void OutputSolution(Solution solution, int field_x_size, int field_y_size)
        {
            var answer_list = new List<Tuple<int, int, int>>();
            foreach ( var pressing_info in solution.GetStampAnswerList )
            {
                Stamp combined_stamp = pressing_info[0];
                int slide_x = pressing_info[1];
                int slide_y = pressing_info[2];

                // スタンプを構成するorigin stampを平行移動したのちanswer_listに追加
                foreach ( var origin_stamp in combined_stamp.GetOriginStampList() )
                {
                    var after_stamp = new Tuple<int, int, int>(origin_stamp[0], 
                                                               origin_stamp[1] + slide_x, 
                                                               origin_stamp[2] + slide_y);
                    // スタンプがフィールド外にある場合には解に追加しない
                    // NOTE: ここでは第4象限にある場合のみをチェック。それ以外はチェックしていない
                    if ( after_stamp.Item2 > field_x_size || after_stamp.Item3 > field_y_size )
                    {
                        continue;
                    }
                    answer_list.Add(after_stamp);
                }
            }

            // スタンプを押す回数を出力
            int len_answer_list = answer_list.Count();
            Console.WriteLine(len_answer_list);

            // スタンプの押し方を出力
            for ( int i = 0; i < len_answer_list; ++i )
            {
                int stamp_number = answer_list[i].Item1;
                int slide_x = answer_list[i].Item2;
                int slide_y = answer_list[i].Item3;
                string end_line = "";
                if ( i == len_answer_list - 1)
                {
                    end_line = "\n";
                }
                Console.Write( stamp_number + ";" + slide_x + "," + slide_y + end_line );
            }

            return;
        }
    }
}
