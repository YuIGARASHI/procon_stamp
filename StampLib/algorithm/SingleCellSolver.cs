using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;

namespace StampLib.algorithm
{
    public class SingleCellSolver : StampSolver
    {
        override
        public Solution CalcSolution(Instance instance)
        {
            Solution solution = null;

            // 黒いセルが1つのスタンプを取得
            CombinedStamp single_cell_stamp = null;
            foreach (var combined_stamp in instance.GetCombinedStampObjectList())
            {
                if (combined_stamp.GetBlackCellCoordinate().Count() == 1)
                {
                    single_cell_stamp = combined_stamp;
                    break;
                }
            }
            short single_cell_y = single_cell_stamp.GetBlackCellCoordinate()[0].Item1;
            short single_cell_x = single_cell_stamp.GetBlackCellCoordinate()[0].Item2;

            // ターゲットフィールドの黒い場所にあてがうように黒いセルを配置する
            Field field = instance.GetField();
            foreach (var black_cell_coordinate in field.GetBlackCellCoordinates() )
            {
                short target_cell_y = black_cell_coordinate.Item1;
                short target_cell_x = black_cell_coordinate.Item2;

                solution.AddStampAnswer(single_cell_stamp, 
                                      (short)(target_cell_x - single_cell_x), 
                                      (short)(target_cell_y - single_cell_y));

            }

            return solution;
        }
    }
}
