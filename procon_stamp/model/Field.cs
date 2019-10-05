using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using procon_stamp.model;

namespace procon_stamp.model {
    class Field {
        // お手本のフィールドのx,y軸方向サイズ。
        public static int field_x_size;
        public static int field_y_size;

        // お手本の黒い箇所の座標を格納するリスト。
        public static List<Tuple<int, int>> black_cell_list_of_target_field;

        // 分割したお手本のフィールド情報（座標）を格納するリスト。
        public static List<Tuple<int, int>> divide_list;
        
        // 分割したお手本のフィールド情報（値[0,1]）を格納するリスト。
        public static List<Tuple<int, int>> divide_value_list;
        
        // 分割したお手本のフィールドごとの合計した値[0,1]を格納するリスト。
        public static List<Tuple<int, int>> divide_total_value_list;
      
        // ランダムの対象となる分割フィールドのインデックスを格納するリスト。 
        public static List<Tuple<int, int>> random_target_field;
    
        // 自分のフィールド情報を二次元配列で格納するリスト。
        private int[,] my_field;

        // お手本情報を二次元配列で格納するリスト。
        public static int[,] target_field;

        static Field()
        {
            field_x_size = 0;
            field_y_size = 0;
            black_cell_list_of_target_field = new List<Tuple<int, int>>();
            divide_list = new List<Tuple<int, int>>();
            divide_value_list = new List<Tuple<int, int>>();
            divide_total_value_list = new List<Tuple<int, int>>();
            random_target_field = new List<Tuple<int, int>>();
            target_field = new int[0, 0];
        }

        /// <summary>
        /// 引数なしコンストラクタ
        /// </summary>
        public Field()
        {
            // my_fieldを初期化
            this.my_field = new int[Field.field_y_size , Field.field_x_size];

            for (int y = 0; y < Field.field_y_size; y++)
            {
                for (int x = 0; x < Field.field_x_size; x++)
                {
                    this.my_field[y, x] = 0;
                }
            }
        }

        /// <summary>
        /// クラス変数（target_field、field_x_size、field_y_size）をセットする
        /// </summary>
        /// <param name="target_field_information">お手本情報</param>
        public static void SetTargetField(string target_field_information)
        {
            string target_field_str;
            string[] target_field_information_for_split = target_field_information.Split(';');
            field_x_size = int.Parse(target_field_information_for_split[0]);
            field_y_size = int.Parse(target_field_information_for_split[1]);
            target_field_str = target_field_information_for_split[2];

            // target_fieldを作成する
            Field.target_field = new int[Field.field_y_size, Field.field_x_size];
            int current_position = 0;
            for (int y = 0; y < field_y_size; y++)
            {
                for (int x = 0; x < field_x_size; x++)
                {
                    Field.target_field[y, x] = int.Parse(target_field_str[current_position].ToString());
                    current_position += 1;
                }
            }
        }

        /// <summary>
        /// target_fieldとmy_fieldとの一致数を計算する。
        /// </summary>
        /// <returns>一致数</returns>
        public int NumOfMatchesWithTargetField()
        {
            int match_count = 0;
            for (int y = 0; y < Field.field_y_size; y++)
            {
                for (int x = 0; x < Field.field_y_size; x++)
                {
                    if (Field.target_field[y,x].Equals(this.my_field[y,x]))
                    {
                        match_count++;
                    }
                }
            }                
            return match_count;
        }

        /// <summary>
        /// お手本の黒い箇所の座標を格納するリスト
        /// </summary>
        /// <returns></returns>
        public List<Tuple<int, int>> GetBlackCellCoordinateForTargetField()
        {
            if (black_cell_list_of_target_field.Count() == 0)
            {
                return Field.black_cell_list_of_target_field;
            }
            else
            {
                for(int y = 0; y < Field.field_y_size; y++)
                {
                    for (int x = 0; x < Field.field_x_size; x++)
                    {
                        if(Field.target_field[y, x] == 1)
                        {
                            Field.black_cell_list_of_target_field.Add(new Tuple<int, int>(y, x));
                        }
                    }
                }
                
            }
            return Field.black_cell_list_of_target_field;
        }


        /// <summary>
        /// my_fieldにスタンプを押す。
        /// </summary>
        /// <param name=""></param>
        public void PressStamp(Stamp stamp_object, int parallel_translation_x, int parallel_translation_y)
        {
            int candidate_press_x = 0;
            int candidate_press_y = 0;

            List<Tuple<int, int>> press_black_cell_coordinate_list = stamp_object.GetBlackCellCoordinate();

            foreach (Tuple<int, int> press_tuple in press_black_cell_coordinate_list)
            {
                candidate_press_x = press_tuple.Item2 + parallel_translation_x;
                candidate_press_y = press_tuple.Item1 + parallel_translation_y;

                if (candidate_press_x < 0 ||
                    candidate_press_y < 0 ||
                    candidate_press_x >= Field.field_x_size ||
                    candidate_press_y >= Field.field_y_size)
                {
                    continue;
                }

                if (this.my_field[candidate_press_y, candidate_press_x] == 0)
                {
                    this.my_field[candidate_press_y, candidate_press_x] = 1;
                }
                else if (this.my_field[candidate_press_y, candidate_press_x] == 1)
                {
                    this.my_field[candidate_press_y, candidate_press_x] = 0;
                }
                else
                {
                    Console.WriteLine("pass");
                }                
            }
        }
    }
}
