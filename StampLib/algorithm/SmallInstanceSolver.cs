using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.algorithm;
using StampLib.model;
using StampLib.util;
// using Google.OrTools.LinearSolver;
using Google.OrTools.Sat;

namespace StampLib.algorithm 
{
    public class SmallInstanceSolver : StampSolver {
        /// <summary>
        /// 小さいセルをターゲットフィールドの黒い箇所に配置する
        /// </summary>
        /// <param name="instance">インスタンスオブジェクト</param>
        /// <returns>ソリューションオブジェクト</returns>
        override
        public Solution CalcSolution(Instance instance)
        {
            Solution solution = new Solution();

            // CpModelに変数を追加
            CpModel model = new CpModel();
            Dictionary<Tuple<int, int>, IntVar> stamp_variables = new Dictionary<Tuple<int, int>, IntVar>();
            Dictionary<Tuple<int, int>, IntVar> field_variables = new Dictionary<Tuple<int, int>, IntVar>();
            // スタンプ変数

            CombinedStamp cs = instance.GetCombinedStampObjectList()[0];
            Field field = new Field();
            field = instance.GetField();

            for (int y = (cs.GetYSize() - 1) * (-1); y < field.GetYSize(); ++y)
            {
                for (int x = (cs.GetXSize() - 1) * (-1); x < field.GetXSize(); ++x)
                {
                    string name = "stamp_" + y.ToString() + "_" + x.ToString();
                    stamp_variables[new Tuple<int, int>(y, x)] = model.NewBoolVar(name);
                }
            }
            // フィールド変数
            //for (int y = 0; y < field.GetYSize(); ++y)
            //{
            //    for (int x = 0; x < field.GetXSize(); ++x)
            //    {
            //        string name = "field" + y.ToString() + "_" + x.ToString();
            //        field_variables[new Tuple<int, int>(y, x)] = model.NewBoolVar(name);
            //    }
            //}

            List<Tuple<short, short>> field_black_cell_coordinates = field.GetBlackCellCoordinates();
            foreach (var field_cell in field_black_cell_coordinates)
            {
                List<IntVar> var_take_xors = new List<IntVar>();
                List<Tuple<short, short>> stamp_black_cell_coordinates = cs.GetBlackCellCoordinate();
                foreach (var stamp_cell in stamp_black_cell_coordinates)
                {
                    int y_ind = field_cell.Item1 - stamp_cell.Item1;
                    int x_ind = field_cell.Item2 - stamp_cell.Item2;
                    var_take_xors.Add(stamp_variables[new Tuple<int, int>(y_ind, x_ind)]);
                }
                model.AddBoolXor(var_take_xors);
            }

            IntVar constant_true = model.NewBoolVar("constant_true");
            List<IntVar> constant_true_list = new List<IntVar>();
            constant_true_list.Add(constant_true);
            model.AddBoolAnd(constant_true_list);

            List<Tuple<short, short>> field_white_cell_coordinates = field.GetWhiteCellCoordinates();
            foreach (var field_cell in field_white_cell_coordinates)
            {
                List<IntVar> var_take_xors = new List<IntVar>();
                List<Tuple<short, short>> stamp_black_cell_coordinates = cs.GetBlackCellCoordinate();
                foreach (var stamp_cell in stamp_black_cell_coordinates)
                {
                    int y_ind = field_cell.Item1 - stamp_cell.Item1;
                    int x_ind = field_cell.Item2 - stamp_cell.Item2;
                    var_take_xors.Add(stamp_variables[new Tuple<int, int>(y_ind, x_ind)]);

                }

                //// target fieldの情報を制約に追加.
                //List<IntVar> field_variable = new List<IntVar>();
                //field_variable.Add(field_variables[new Tuple<int, int>(field_cell.Item1, field_cell.Item2)]);
                //model.AddBoolAnd(field_variable);

                var_take_xors.Add(constant_true);
                model.AddBoolXor(var_take_xors);
            }

            // 求解
            CpSolver solver = new CpSolver();
            //solver.StringParameters = "max_time_in_seconds:5.0";
            //Console.WriteLine("start!!\n");
            CpSolverStatus status = solver.Solve(model);

            if (status == CpSolverStatus.Feasible)
            {

                for (int y = (cs.GetYSize() - 1) * (-1); y < field.GetYSize(); ++y)
                {
                    for (int x = (cs.GetXSize() - 1) * (-1); x < field.GetXSize(); ++x)
                    {
                        Tuple<int, int> cur_pos = new Tuple<int, int>(y, x);
                        if (solver.Value(stamp_variables[cur_pos]) == 1)
                        {
                            solution.AddStampAnswer(cs, (short)x, (short)y);
                            //field.PressStamp(cs, (short)x, (short)y);

                        }
                        //Tuple<int, int> cur_pos = new Tuple<int, int>(y, x);
                        //if (solver.Value(stamp_variables[cur_pos]) == 1)
                        //{
                        //    field.PressStamp(cs, (short)x, (short)y);
                        //    Console.Write("1");
                        //}
                        //else
                        //{
                        //    Console.Write("0");
                        //}
                    }
                    //Console.WriteLine();
                }

                //Console.WriteLine("\nmy_field is :");
                //field.PrintMyself();
                //Console.WriteLine("\ntarget_field is :");
                //field.PrintTargetField();

            }

            return solution;

            //    // 解の検証
            //    Console.WriteLine("Pressing stamp is:");
            //if (status == CpSolverStatus.Feasible)
            //{
            //    for (int y = (stamp_size - 1) * (-1); y < field_size; ++y)
            //    {
            //        for (int x = (stamp_size - 1) * (-1); x < field_size; ++x)
            //        {
            //            Tuple<int, int> cur_pos = new Tuple<int, int>(y, x);
            //            if (solver.Value(stamp_variables[cur_pos]) == 1)
            //            {
            //                field.PressStamp(stamp, (short)x, (short)y);
            //                Console.Write("1");
            //            }
            //            else
            //            {
            //                Console.Write("0");
            //            }
            //        }
            //        Console.WriteLine();
            //    }

            //    Console.WriteLine("\nmy_field is :");
            //    field.PrintMyself();
            //    Console.WriteLine("\ntarget_field is :");
            //    field.PrintTargetField();
            //}
        }
    }
}