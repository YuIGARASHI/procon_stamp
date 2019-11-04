using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;

namespace StampLib.algorithm {
    public class SingleBlackCellSolver : Solver {

        /// <summary>
        /// 加工されたスタンプ（黒いセルが1つ）を用いて、解を生成する。
        /// </summary>
        /// <param name="instance">インスタンスオブジェクト</param>
        /// <returns>ソリューションオブジェクト</returns>
        override
        public Solution CalcSolution(Instance instance)
        {

            // 暫定解
            Solution current_best_solution = new Solution();

            Stamp single_black_cell_stamp_object = new Stamp();
            // ①combinedlistの中から、黒いセルが一つのものを探す。
            foreach (Stamp stamp_object in instance.GetCombinedStampObjectList())
            {
                if (stamp_object.GetBlackCellCoordinate().Count() == 1)
                {
                    single_black_cell_stamp_object = stamp_object;
                }
            }
            
            // ③for分で②で取得した座標に対して、①を配置
            current_best_solution = SingleBlackCellSolver.MakeCandidateSolution(instance, single_black_cell_stamp_object);


            // ④念のため、お手本との一致数を確認
            int match_count = 0;
            //match_count = field.NumOfMatchesWithTargetField();
            Console.WriteLine(match_count);

            return current_best_solution;

        }


        /// <summary>
        /// target_fieldにスタンプを押した解を生成する。
        /// </summary>
        /// <param name="instance">インスタンスオブジェクト</param>
        /// <returns>生成した解, 解によって生成されたフィールド</returns>
        
        private static Solution MakeCandidateSolution(Instance instance, Stamp single_black_cell_stamp_object)
        {
            var temp_field = new Field();
            var temp_solution = new Solution();
            List<Tuple<short, short>> temp_stamp_x_y = new List<Tuple<short, short>>();
            temp_stamp_x_y = single_black_cell_stamp_object.GetBlackCellCoordinate();

            //foreach (Tuple<short, short> target_field_black_cell_coordinate in temp_field.GetBlackCellCoordinates())
            //{
            //    temp_solution.AddStampAnswer(single_black_cell_stamp_object,
            //                                 target_field_black_cell_coordinate.Item1 - temp_stamp_x_y[0].Item1,
            //                                 target_field_black_cell_coordinate.Item2 - temp_stamp_x_y[0].Item2);
            //    temp_field.PressStamp(single_black_cell_stamp_object,
            //                          target_field_black_cell_coordinate.Item1,
            //                          temp_stamp_x_y[0].Item2);
            //}

            return temp_solution;
        }



















    }
}
