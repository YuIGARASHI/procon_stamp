using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampLib.model
{
    public class Field
    {
        // お手本のフィールドのx,y軸方向サイズ
        public static short x_size;
        public static short y_size;

        // お手本の黒いセルの座標を格納するリスト
        public List<Tuple<short, short>> black_cell_coordinates;

        // お手本の白いセルの座標を格納するリスト
        private List<Tuple<short, short>> white_cell_coordinates;

        // 自分のフィールド情報を二次元配列で格納するリスト
        private bool[,] my_field;

        static Field()
        {
            Field.x_size = 0;
            Field.y_size = 0;
        }

        public Field()
        {
            this.my_field = new bool[Field.y_size, Field.x_size];
            this.black_cell_coordinates = new List<Tuple<short, short>>();
            this.white_cell_coordinates = new List<Tuple<short, short>>();

            for (short y = 0; y < Field.y_size; y++)
            {
                for (short x = 0; x < Field.x_size; x++)
                {
                    this.my_field[y, x] = false;
                }
            }
        }

        public List<Tuple<short, short>> GetBlackCellCoordinates()
        {
            return this.black_cell_coordinates;
        }

        /// <summary>
        /// クラス変数（target_field、x_size、y_size）をセットする。
        /// </summary>
        /// <param name="target_field_info">お手本の情報を表す文字列</param>
        public void SetTargetField(string target_field_info)
        {
            string[] target_field_info_array = target_field_info.Split(';');
            Field.x_size = short.Parse(target_field_info_array[0]);
            Field.y_size = short.Parse(target_field_info_array[1]);

            string black_cell_coordinates_str = target_field_info_array[2];
            short ind = 0;
            for (short y_ind = 0; y_ind < Field.y_size; ++y_ind )
            {
                for (short x_ind = 0; x_ind < Field.x_size; ++x_ind )
                {
                    if ( black_cell_coordinates_str[ind++] == '1' )
                    {
                        this.black_cell_coordinates.Add(new Tuple<short, short>(y_ind, x_ind));
                    } else
                    {
                        this.white_cell_coordinates.Add(new Tuple<short, short>(y_ind, x_ind));
                    }
                }
            }
        }

        /// <summary>
        /// target_fieldとmy_fieldとの一致数を計算する。
        /// </summary>
        /// <returns>一致数</returns>
        public short NumOfMatchesWithTargetField()
        {
            short match_count = 0;

            // 黒いセルの一致数をカウント
            foreach (var cell in this.black_cell_coordinates)
            {
                if ( this.my_field[cell.Item1, cell.Item2] )
                {
                    match_count++;
                }
            }

            // 白いセルの一致数をカウント
            foreach (var cell in this.white_cell_coordinates)
            {
                if ( !this.my_field[cell.Item1, cell.Item2] )
                {
                    match_count++;
                }
            }
            return match_count;
        }

        /// <summary>
        /// stampを平行移動したのち、myfieldに押す。
        /// </summary>
        /// <param name="stamp">スタンプのオブジェクト</param>
        /// <param name="parallel_translation_x">x軸方向への平行移動距離</param>
        /// <param name="parallel_translation_y">y軸方向への平行移動距離</param>
        public void PressStamp(Stamp stamp,
                               short parallel_translation_x,
                               short parallel_translation_y)
        {
            foreach (var cell in stamp.GetBlackCellCoordinate())
            {
                short y = (short)(parallel_translation_y + cell.Item1);
                short x = (short)(parallel_translation_x + cell.Item2);

                // スタンプを押す場所が my field の外なら continue
                if (y < 0 || y >= Field.y_size || x < 0 || x >= Field.x_size)
                {
                    continue;
                }

                this.my_field[y, x] = !my_field[y, x];
            }
        }

        /// <summary>
        /// 現在のフィールドを標準出力に描画する。
        /// </summary>
        public void Print()
        {
            for (short y_ind = 0; y_ind < Field.y_size; ++y_ind)
            {
                for (short x_ind = 0; x_ind < Field.x_size; ++x_ind)
                {
                    if (this.my_field[y_ind, x_ind])
                    {
                        Console.Write(" *");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
