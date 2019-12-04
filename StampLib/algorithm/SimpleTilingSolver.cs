using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;

namespace StampLib.algorithm
{
    public class SimpleTilingSolver : StampSolver
    {
        override
        public Solution CalcSolution(Instance instance)
        {
            Solution ret_solution = new Solution();

            Field field = instance.GetField();
            field.SetTargetFieldToMyField();

            Stamp stamp = instance.GetOriginalStampObjectList()[0];
            Tuple<short, short> first_black_cell_coord = stamp.GetFirstBlackCellCoord();

            for (short y = 0; y < field.GetYSize(); ++y)
            {
                for (short x = 0; x < field.GetXSize(); ++x)
                {
                    if ( field.GetMyField(y, x) == false )
                    {
                        continue;
                    }

                    short stamp_slide_y = (short)(y - first_black_cell_coord.Item1);
                    short stamp_slide_x = (short)(x - first_black_cell_coord.Item2);
                    ret_solution.AddStampAnswer(stamp, stamp_slide_x, stamp_slide_y);

                    field.PressStamp(stamp, stamp_slide_x, stamp_slide_y);
                }
            }

            return ret_solution;
        }
    }
}
