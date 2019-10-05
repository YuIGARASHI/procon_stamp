using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon_stamp.model {
    class Stamp {

        // スタンプのx、y軸方向サイズ。
        private int stamp_x_size;
        private int stamp_y_size;

        // スタンプの絵の定義。
        private string definition_of_stamp_picture;

        // スタンプの黒いセルの座標を格納する配列。
        private List<Tuple<int, int>> black_cell_coordinate_list = new List<Tuple<int, int>>();

        // スタンプを構成するオリジナルスタンプの情報を保持する3-tupeの配列。
        // 3-tupleのレイアウトは(index, x軸方向への平行移動距離, y軸方向の平行移動距離)。
        private List<Tuple<int, int, int>> indices = new List<Tuple<int, int, int>>();

        // スタンプのオブジェクトを格納するリスト
        private List<Tuple<int, int, int>> origin_stamp_list = new List<Tuple<int, int, int>>();

        /// <summary>
        /// 引数ありコンストラクタ
        /// </summary>
        /// <param name="idx">スタンプのインデックス</param>
        /// <param name="input_str">スタンプの定義（x軸方向サイズ；y 軸方向サイズ；絵の定義）</param>
        public Stamp(int idx, string input_str)
        {
            this.origin_stamp_list.Add(new Tuple<int, int, int>(idx, 0, 0));
            string[] input_stamp_information = input_str.Split(';');
            this.stamp_x_size = int.Parse(input_stamp_information[0]);
            this.stamp_y_size = int.Parse(input_stamp_information[1]);
            this.definition_of_stamp_picture = input_stamp_information[2];

            // black_cell_coordinate_listの計算
            int current_position = 0;
            for (int y = 0; y < stamp_y_size; y++)
            {
                for (int x = 0; x < stamp_x_size; x++)
                {
                    if (definition_of_stamp_picture[current_position] == '1')
                    {
                        this.black_cell_coordinate_list.Add(new Tuple<int, int>(y, x));
                    }
                    current_position++;
                }
            }
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns>フィールドの黒いセルの座標リスト</returns>
        public List<Tuple<int, int>> GetBlackCellCoordinate()
        {
            return this.black_cell_coordinate_list;
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns>スタンプクラスのオブジェクトリスト</returns>
        public List<Tuple<int,int,int>> GetOriginStampList()
        {
            return this.origin_stamp_list;
        }

    }
}
