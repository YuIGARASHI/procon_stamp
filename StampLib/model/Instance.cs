using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.OrTools.Sat;

namespace StampLib.model
{
    public class Instance
    {

        private List<Stamp> origin_stamp_object_list;
        private List<CombinedStamp> combined_stamp_object_list;
        private Field field;
        const int JUDGE_SIZE = 10000;

        public Instance()
        {
            this.origin_stamp_object_list = new List<Stamp>();
            this.combined_stamp_object_list = new List<CombinedStamp>();
            this.field = new Field();
        }

        public void SetOriginStampObject(List<Stamp> stamp_object)
        {
            this.origin_stamp_object_list = stamp_object;
        }

        public List<CombinedStamp> GetCombinedStampObjectList()
        {
            return this.combined_stamp_object_list;
        }

        public List<Stamp> GetOriginalStampObjectList()
        {
            return this.origin_stamp_object_list;
        }

        public void SetField( Field field )
        {
            this.field = field;
        }

        public Field GetField()
        {
            return this.field;
        }

        /// <summary>
        /// できるだけ面積の小さい combined stamp を構築する。
        /// </summary>
        public void MakeCombinedStampList()
        {

            int max_count = 640 * 480;
            CombinedStamp candidate_combined_stamp = new CombinedStamp();
            
            // combined_stamp_listに追加
            foreach (var origin_stamp in this.origin_stamp_object_list)
            {
                CombinedStamp cs = new CombinedStamp();
                cs.AddStamp(this, origin_stamp, 0, 0);
                cs.AddStamp(this, origin_stamp, 0, 1);
                this.combined_stamp_object_list.Add(cs);
            }


            // combined_stamp_listの中から黒いセルが一番少ないスタンプを抽出する
            foreach (var combined_stamp in this.combined_stamp_object_list)
            {
                if (max_count > combined_stamp.GetBlackCellCoordinate().Count())
                {
                    max_count = combined_stamp.GetBlackCellCoordinate().Count();
                    candidate_combined_stamp = combined_stamp;
                }
            }

            this.combined_stamp_object_list.Clear();
            this.combined_stamp_object_list.Add(candidate_combined_stamp);
            return;
            

            //#region OrTools
            //Field single_cell_field = new Field();
            //short parameter_field_size = 80;
            //short M = parameter_field_size;
            //short N = parameter_field_size;
            //// ここで、single_cell_fieldの各種メンバ変数を初期化
            //single_cell_field.SetYSize(M);
            //single_cell_field.SetXSize(N);
            //for (short y = 0; y < M; y++)
            //{
            //    for (short x = 0; x < N; x++)
            //    {
            //        if ((y == (short)(M/2)) && (x == (short)(N/2)))
            //        {
            //            single_cell_field.AddBlackCellCoordinates(y, x);
            //        }
            //        else
            //        {
            //            single_cell_field.AddWhiteCellCoordinates(y, x);
            //        }
            //    }
            //}

            //// Creates the model.
            //CpModel model = new CpModel();
            //Dictionary<Tuple<int, int>, IntVar> stamp_variables = new Dictionary<Tuple<int, int>, IntVar>();
            //Dictionary<Tuple<int, int>, IntVar> field_variables = new Dictionary<Tuple<int, int>, IntVar>();
            //// スタンプ変数
            //for (int y = (candidate_combined_stamp.GetYSize() - 1) * (-1); y < single_cell_field.GetYSize(); ++y)
            //{
            //    for (int x = (candidate_combined_stamp.GetXSize() - 1) * (-1); x < single_cell_field.GetXSize(); ++x)
            //    {
            //        string name = "stamp_" + y.ToString() + "_" + x.ToString();
            //        stamp_variables[new Tuple<int, int>(y, x)] = model.NewBoolVar(name);
            //    }
            //}
            //// フィールド変数
            //for (int y = 0; y < single_cell_field.GetYSize(); ++y)
            //{
            //    for (int x = 0; x < single_cell_field.GetXSize(); ++x)
            //    {
            //        string name = "field" + y.ToString() + "_" + x.ToString();
            //        field_variables[new Tuple<int, int>(y, x)] = model.NewBoolVar(name);
            //    }
            //}


            //List<Tuple<short, short>> field_black_cell_coordinates = single_cell_field.GetBlackCellCoordinates();
            //foreach (var field_cell in field_black_cell_coordinates)
            //{
            //    List<IntVar> var_take_xors = new List<IntVar>();
            //    List<Tuple<short, short>> stamp_black_cell_coordinates = candidate_combined_stamp.GetBlackCellCoordinate();
            //    foreach (var stamp_cell in stamp_black_cell_coordinates)
            //    {
            //        int y_ind = field_cell.Item1 - stamp_cell.Item1;
            //        int x_ind = field_cell.Item2 - stamp_cell.Item2;
            //        var_take_xors.Add(stamp_variables[new Tuple<int, int>(y_ind, x_ind)]);
            //    }
            //    model.AddBoolXor(var_take_xors);
            //}

            //List<Tuple<short, short>> field_white_cell_coordinates = single_cell_field.GetWhiteCellCoordinates();
            //foreach (var field_cell in field_white_cell_coordinates)
            //{
            //    List<IntVar> var_take_xors = new List<IntVar>();
            //    List<Tuple<short, short>> stamp_black_cell_coordinates = candidate_combined_stamp.GetBlackCellCoordinate();
            //    foreach (var stamp_cell in stamp_black_cell_coordinates)
            //    {
            //        int y_ind = field_cell.Item1 - stamp_cell.Item1;
            //        int x_ind = field_cell.Item2 - stamp_cell.Item2;
            //        var_take_xors.Add(stamp_variables[new Tuple<int, int>(y_ind, x_ind)]);

            //    }

            //    // target fieldの情報を制約に追加.
            //    List<IntVar> field_variable = new List<IntVar>();
            //    field_variable.Add(field_variables[new Tuple<int, int>(field_cell.Item1, field_cell.Item2)]);
            //    model.AddBoolAnd(field_variable);

            //    var_take_xors.Add(field_variables[new Tuple<int, int>(field_cell.Item1, field_cell.Item2)]);
            //    model.AddBoolXor(var_take_xors);
            //}

            //// 求解
            //CpSolver solver = new CpSolver();
            //solver.StringParameters = "max_time_in_seconds:5.0";
            ////Console.WriteLine("start!!\n");
            //CpSolverStatus status = solver.Solve(model);

            //// --

            //// 解の検証
            ////Console.WriteLine("Pressing stamp is:");
            //Field dummy_field = new Field();
            //// ここで、single_cell_fieldの各種メンバ変数を初期化
            //dummy_field.SetYSize(M);
            //dummy_field.SetXSize(N);
            //dummy_field.InitMyField();

            //if (status == CpSolverStatus.Feasible)
            //{
            //    for (int y = (candidate_combined_stamp.GetYSize() - 1) * (-1); y < dummy_field.GetYSize(); ++y)
            //    {
            //        for (int x = (candidate_combined_stamp.GetXSize() - 1) * (-1); x < dummy_field.GetXSize(); ++x)
            //        {
            //            Tuple<int, int> cur_pos = new Tuple<int, int>(y, x);
            //            if (solver.Value(stamp_variables[cur_pos]) == 1)
            //            {
          

            //                dummy_field.PressStamp(candidate_combined_stamp, (short)x, (short)y);
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
            //    dummy_field.PrintMyself();
            //    Console.WriteLine("\ntarget_field is :");
            //    single_cell_field.PrintTargetField();
            //}

            //#endregion
            //foreach (var combined_stamp_object in this.combined_stamp_object_list)
            //{
            //    combined_stamp_object.Print();
            //}
        }

        /// <summary>
        /// Combinedスタンプから構成される解をOriginalスタンプの解に変換する
        /// </summary>
        /// <param name="combined_solution">Combinedスタンプから成る解</param>
        /// <returns>Originalスタンプから構成される解</returns>
        public Solution CombinedSolutionToOriginalSolution(Solution combined_solution)
        {
            // combined_stampをオリジナルスタンプに分解してtmp_origin_solutionに追加
            Solution tmp_origin_solution = new Solution();
            {
                List<Tuple<Stamp, short, short>> combined_answer_list = combined_solution.GetStampAnswerList();
                foreach (var combined_answer in combined_answer_list)
                {
                    CombinedStamp combined_stamp = (CombinedStamp)combined_answer.Item1;
                    List<Tuple<short, short, short>> origin_stamp_config_list = combined_stamp.GetOriginStampConfigList();
                    short combined_stamp_slide_x = combined_answer.Item2;
                    short combined_stamp_slide_y = combined_answer.Item3;

                    foreach (var origin_stamp_config in origin_stamp_config_list)
                    {
                        short origin_stamp_idx = origin_stamp_config.Item1;
                        short origin_stamp_slide_x = origin_stamp_config.Item2;
                        short origin_stamp_slide_y = origin_stamp_config.Item3;
                        Stamp origin_stamp_object = this.origin_stamp_object_list[origin_stamp_idx];
                        tmp_origin_solution.AddStampAnswer(origin_stamp_object,
                                                           (short)(origin_stamp_slide_x + combined_stamp_slide_x),
                                                           (short)(origin_stamp_slide_y + combined_stamp_slide_y));
                    }
                }
            }

            // 枠外にはみ出たオリジナルスタンプ以外をorigin_solutionに追加
            Solution origin_solution = new Solution();
            {
                List<Tuple<Stamp, short, short>> origin_stamp_answer_list = tmp_origin_solution.GetStampAnswerList();

                foreach (var origin_stamp_answer in origin_stamp_answer_list)
                {
                    Stamp stamp = origin_stamp_answer.Item1;
                    short slide_x = origin_stamp_answer.Item2;
                    short slide_y = origin_stamp_answer.Item3;

                    // x方向チェック
                    if (stamp.GetXSize() + slide_x <= 0 || slide_x >= field.GetXSize())
                    {
                        continue;
                    }

                    // y方向チェック
                    if (stamp.GetYSize() + slide_y <= 0 || slide_y >= field.GetYSize())
                    {
                        continue;
                    }

                    origin_solution.AddStampAnswer(stamp, slide_x, slide_y);
                }
            }

            return origin_solution;
        }

        /// <summary>
        /// 黒いセルが1つだけのスタンプを保持しているかどうかを返す
        /// </summary>
        /// <returns>保持している場合true</returns>
        public bool HasSingleCellStamp()
        {
            foreach (var combined_stamp in this.combined_stamp_object_list)
            {
                if (combined_stamp.GetBlackCellCoordinate().Count() == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public int SelectAlgorithm()
        {
            int target_field_size = this.field.GetYSize() * this.field.GetXSize();

            if (target_field_size <= JUDGE_SIZE)
            {
                return 0;
            }

            //if (hogehoge)
            //{
            //    return    
            //}

            return 100;

        }

    }
}
