using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampLib.model
{
    public class Stamp
    {
        // スタンプのx軸, y軸方向のサイズ
        protected short x_size;
        protected short y_size;

        // スタンプの黒いセルの座標を格納する配列。
        protected List<Tuple<short, short>> black_cell_coordinates;

        // 入力問題中の自身のインデクス。combined_stampの場合は-1(無効値)になる
        // NOTE:本来であればこの変数はStampクラスに入っているべきではない。
        //      CombinedStampはStampを継承しているにもかかわらず、この変数の存在により is-a 関係が崩れてしまっているため。
        //      時間の関係で今回はこのようなクラス構成になってしまっているが、べき論でいえば
        //      Stampを継承するOriginalStampクラスをつくってそこにこの変数をもたせるのがよさそう。
        private short origin_stamp_index;

        /// <summary>
        /// 引数なしコンストラクタ。CombinedStampを生成する際にのみ用いる。
        /// </summary>
        public Stamp()
        {
            this.black_cell_coordinates = new List<Tuple<short, short>>();
            this.x_size = 0;
            this.y_size = 0;
            this.origin_stamp_index = -1;
        }

        /// <summary>
        /// 引数ありコンストラクタ。OriginalStampを生成する際にのみ用いる。
        /// </summary>
        /// <param name="idx">スタンプのインデックス</param>
        /// <param name="input_str">スタンプの定義（x軸方向サイズ；y 軸方向サイズ；絵の定義）</param>
        public Stamp(short idx, string input_str)
        {
            this.black_cell_coordinates = new List<Tuple<short, short>>();

            string[] input_stamp_information = input_str.Split(';');
            this.x_size = short.Parse(input_stamp_information[0]);
            this.y_size = short.Parse(input_stamp_information[1]);
            this.origin_stamp_index = idx;

            // black_cell_coordinatesの計算
            string stamp_info_str = input_stamp_information[2];
            short current_position = 0;
            for (short y = 0; y < y_size; y++)
            {
                for (short x = 0; x < x_size; x++)
                {
                    if (stamp_info_str[current_position] == '1')
                    {
                        this.black_cell_coordinates.Add(new Tuple<short, short>(y, x));
                    }
                    current_position++;
                }
            }
        }

        public List<Tuple<short, short>> GetBlackCellCoordinate()
        {
            return this.black_cell_coordinates;
        }

        public short GetXSize()
        {
            return this.x_size;
        }

        public short GetYSize()
        {
            return this.y_size;
        }

        public short GetOriginStampIndex()
        {
            return this.origin_stamp_index;
        }

        /// <summary>
        /// スタンプを標準出力に描画する。
        /// </summary>
        public void Print()
        {
            for (short y_ind = 0; y_ind < this.y_size; ++y_ind)
            {
                for (short x_ind = 0; x_ind < this.x_size; ++x_ind)
                {
                    var candidate_cell = new Tuple<short, short>(y_ind, x_ind);
                    if (this.black_cell_coordinates.Contains(candidate_cell))
                    {
                        Console.Write(" *");
                    }
                    else
                    {
                        Console.Write(" -");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
