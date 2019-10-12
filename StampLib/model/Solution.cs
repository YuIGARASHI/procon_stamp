using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampLib.model
{
    public class Solution
    {
        // 解を表す3-tupleのリスト
        private List<Tuple<Stamp, short, short>> stamp_answer_list;

        public Solution()
        {
            this.stamp_answer_list = new List<Tuple<Stamp, short, short>>();
        }

        public List<Tuple<Stamp, short, short>> GetStampAnswerList()
        {
            return this.stamp_answer_list;
        }

        /// <summary>
        /// 解に要素を追加する。
        /// </summary>
        /// <param name="stamp">スタンプのオブジェクト</param>
        /// <param name="x">x軸方向への平行移動距離</param>
        /// <param name="y">y軸方向への平行移動距離</param>
        public void AddStampAnswer(Stamp stamp, short x, short y)
        {
            this.stamp_answer_list.Add(new Tuple<Stamp, short, short>(stamp, x, y));
        }
    }
}
