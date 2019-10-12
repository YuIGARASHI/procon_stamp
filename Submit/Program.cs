﻿using System;
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
            var io = new IO();
            io.InputProblem();
            var instance = new Instance();
            instance.SetOriginStampObject(io.GetStampObjectList());
            instance.MakeCombinedStampList();

            // ソルバーの生成 & 解の計算
            Solver solver = new RandomSolver();
            var solution = solver.CalcSolution(instance);

            // 解の出力
            io.OutputSolution(solution);
        }
    }
}
