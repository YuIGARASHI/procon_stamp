using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using procon_stamp.algorithm;
using procon_stamp.model;
using procon_stamp.util;

namespace procon_stamp
{
    class Program
    {
        static void Main(string[] args)
        {
            // 問題の読み取り
            var io = new IO();
            io.InputProblem();
            var instance = new Instance();
            instance.SetOriginStampObject(io.GetStampObjectList());

            // ソルバーの生成 & 解の計算
            Solver solver = new RandomSolver();
            var solution = solver.CalcSolution(instance);

            // 解の出力
            io.OutputSolution(solution, Field.field_x_size, Field.field_y_size);
            
        }
    }
}
