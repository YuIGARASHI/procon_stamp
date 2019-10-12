using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;

namespace StampLib.algorithm
{
    public abstract class Solver
    {
        abstract public Solution CalcSolution(Instance instance);
    }
}
