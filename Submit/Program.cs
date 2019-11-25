using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;
using StampLib.algorithm;
using StampLib.util;

namespace Submit
{
    class Program
    {
        static void Main(string[] args)
        {
            // 問題の読み取り
            Instance instance = IO.InputProblemFromConsole();

            // original stampを基にしたcombined stampの生成
            instance.MakeCombinedStampList();

            // ソルバーの生成
            StampSolver solver = null;
            if (instance.HasSingleCellStamp())
            {
                solver = new SingleCellSolver();
            }
            else
            {
                solver = new RandomStampSolver();
            }

            // 求解
            var combined_solution = solver.CalcSolution(instance);

            // combined_solutionからoriginal solutionへの変換
            var solution = instance.CombinedSolutionToOriginalSolution(combined_solution);

            // 解の出力
            IO.OutputSolutionToConsole(solution);
        }
    }
}
