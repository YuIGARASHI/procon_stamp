using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon_stamp.model {
    class Solution {
        // 解を表す3-tupleのリスト
        private List<Tuple<Stamp, int, int>> stamp_answer_list;

        public Solution()
        {
            this.stamp_answer_list = new List<Tuple<Stamp, int, int>>();
        }

        // 解に要素を追加する
        public void AddStampAnswer(Stamp stamp_object, int x, int y)
        {
            this.stamp_answer_list.Add(new Tuple<Stamp, int, int>(stamp_object, x, y));
        }

        // 解を取得する
        public List<Tuple<Stamp, int ,int>> GetStampAnswerList()
        {
            return this.stamp_answer_list;
        }
    }
}
