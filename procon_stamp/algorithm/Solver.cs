using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using procon_stamp.model;

namespace procon_stamp.algorithm {
    abstract class Solver {
        abstract public Solution CalcSolution(Instance instance);
    }
}
