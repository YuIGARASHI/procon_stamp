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
            instance.MakeCombinedStampList();

            // ソルバーの生成 & 解の計算
            StampSolver solver = new RandomStampSolver();
            var solution = solver.CalcSolution(instance);

            // 解の出力
            IO.OutputSolutionToConsole(solution);
        }
    }
}
