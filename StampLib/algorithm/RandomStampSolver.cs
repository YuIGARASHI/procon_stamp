using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;

namespace StampLib.algorithm
{
    public class RandomStampSolver : StampSolver
    {
        /// <summary>
        /// ランダムにスタンプを配置し解を生成する。
        /// </summary>
        /// <param name="instance">インスタンスオブジェクト</param>
        /// <returns>ソリューションオブジェクト</returns>
        override
        public Solution CalcSolution(Instance instance)
        {

            // 暫定解
            Solution current_best_solution = new Solution();

            // 暫定解の目的関数値
            int best_objective_value = 0;

            // 9.5秒以内で最適解を計算する
            var sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            short rand_seed = 1000;
            var rand = new Random(rand_seed);

            while (sw.ElapsedMilliseconds < 9500)
            {
                // ランダムなスタンプの配置を計算
                Tuple<Solution, Field> result = RandomStampSolver.MakeCandidateSolution(instance, rand);
                Solution temp_solution = result.Item1;
                Field temp_field = result.Item2;
                
                // 目的関数値が改善すれば解に追加
                if (temp_field.NumOfMatchesWithTargetField() > best_objective_value)
                {
                    current_best_solution = temp_solution;
                    best_objective_value = temp_field.NumOfMatchesWithTargetField();
                }
            }

            return current_best_solution;
        }

        /// <summary>
        /// スタンプを100回ランダムに押した解を生成する。
        /// </summary>
        /// <param name="instance">インスタンスオブジェクト</param>
        /// <returns>生成した解, 解によって生成されたフィールド</returns>
        private static Tuple<Solution, Field> MakeCandidateSolution(Instance instance, Random rand)
        {
            var temp_field = new Field(instance.GetField());
            var temp_solution = new Solution();

            short count = 0;
            const short MAX_ITER = 100;
            while (count < MAX_ITER)
            {
                short parallel_translation_x = (short)rand.Next(instance.GetField().GetXSize());
                short parallel_translation_y = (short)rand.Next(instance.GetField().GetYSize());
                var combined_stamp_object_list = instance.GetCombinedStampObjectList();
                short stamp_object_idx = (short)rand.Next(combined_stamp_object_list.Count() - 1);
                temp_solution.AddStampAnswer(combined_stamp_object_list[stamp_object_idx],
                                             parallel_translation_x,
                                             parallel_translation_y);
                temp_field.PressStamp(combined_stamp_object_list[stamp_object_idx],
                                      parallel_translation_x,
                                      parallel_translation_y);
                count++;
            }
            return new Tuple<Solution, Field>(temp_solution, temp_field);
        }
    }
}
