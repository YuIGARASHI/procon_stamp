using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using procon_stamp.model;

namespace procon_stamp.algorithm
{
    class RandomSolver : Solver
    {
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

            while ( sw.ElapsedMilliseconds < 9000 )
            {
                // ランダムなスタンプの配置を計算
                Tuple<Solution, Field> result = RandomSolver.MakeCandidateSolution(instance);
                Solution temp_solution = result.Item1;
                Field temp_field = result.Item2;
                // 目的関数値が改善すれば解に追加
                if ( temp_field.NumOfMatchesWithTargetField() > best_objective_value )
                {
                    current_best_solution = temp_solution;
                    best_objective_value = temp_field.NumOfMatchesWithTargetField();
                }                
            }
            
            return current_best_solution;
        }

        private static Tuple<Solution, Field> MakeCandidateSolution(Instance instance)
        {
            int rand_seed = 1000;
            var rand = new Random(rand_seed);
            var temp_field = new Field();
            var temp_solution = new Solution();

            int count = 0;
            const int MAX_ITER = 100;
            while (count < MAX_ITER)
            {
                // TODO: メンバ変数を直接参照している。アクセサを経由するように改修すべき。
                int parallel_translation_x = rand.Next(Field.field_x_size);
                int parallel_translation_y = rand.Next(Field.field_y_size);
                var combined_stamp_object_list = instance.GetCombinedStampObjectList();
                int stamp_object_idx = rand.Next(combined_stamp_object_list.Count() - 1);
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
