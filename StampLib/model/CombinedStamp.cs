using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampLib.model
{
    public class CombinedStamp : Stamp
    {
        // 自身を構成するオリジナルスタンプのリスト
        // タプルの構成は(オリジナルスタンプのidx, x方向へのスライド距離, y方向へのスライド距離)
        private List<Tuple<short, short, short>> origin_stamp_config_list;

        public CombinedStamp()
        {
            this.origin_stamp_config_list = new List<Tuple<short, short, short>>();
        }

        public List<Tuple<short, short, short>> GetOriginStampConfigList()
        {
            return this.origin_stamp_config_list;
        }

        /// <summary>
        /// オリジナルスタンプを追加する。このとき、親クラスのメンバ変数も更新される。
        /// ただし、x_sizeとy_sizeは、黒セルの入っているセルのうち最も外側にあるものの値とする。
        /// 　
        /// e.g.) black_cell_coordinates が次のような場合、x_size=7, x_size=3になる。
        /// 　...#....
        /// 　.#..#..#
        /// 　...#....
        /// 　.......
        /// </summary>
        /// <param name="instance">問題のインスタンス</param>
        /// <param name="origin_stamp">追加するオリジナルスタンプのインデクス</param>
        /// <param name="slide_x">x方向へのスライド距離</param>
        /// <param name="slide_y">y方向へのスライド距離</param>
        public void AddOriginalStamp(Instance instance, short origin_stamp_idx, short slide_x, short slide_y)
        {
            // CombinedStampのメンバ変数の更新
            this.origin_stamp_config_list.Add(new Tuple<short, short, short>(origin_stamp_idx, slide_x, slide_y));

            // 親クラス（Stamp）のメンバ変数の更新
            // black_cell_coordinate_listの更新
            Stamp add_stamp = (instance.GetOriginalStampObjectList())[origin_stamp_idx];
            List<Tuple<short, short>> add_stamp_black_cell_list = add_stamp.GetBlackCellCoordinate();
            foreach (var black_cell in add_stamp_black_cell_list)
            {
                short y = (short)(black_cell.Item1 + slide_y);
                short x = (short)(black_cell.Item2 + slide_x);
                Tuple<short, short> reverse_cell = new Tuple<short, short>(y, x);

                if (this.black_cell_coordinates.Contains(reverse_cell))
                {
                    this.black_cell_coordinates.Remove(reverse_cell);
                }
                else
                {
                    this.black_cell_coordinates.Add(reverse_cell);
                }
            }
            // size_y, size_xの更新
            short black_cell_max_y = 0; 
            short black_cell_max_x = 0;
            foreach (var cell in this.black_cell_coordinates)
            {
                black_cell_max_y = Math.Max(cell.Item1, black_cell_max_y);
                black_cell_max_x = Math.Max(cell.Item2, black_cell_max_x);
            }
            this.y_size = black_cell_max_y;
            this.x_size = black_cell_max_x;
        }
    }
}
