using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon_stamp.algorithm {
    class FieldDivider {


        private short divided_field_counter;

        // コンストラクタ
        public FieldDivider()
        {
            divided_field_counter = 0;

            // ここで、分割された配列生成

            // 分割したときに、その分割したフィールドに黒いセルが一つもない場合、分割処理をスキップする





        }

        // divided_field_counterをインクリメントしていき、分割単位のMAXに達した場合、FALSE返す
        public bool HasNext()
        {
            return false;
        }

        // 次の分割された開始点、終了点を返す
        public Tuple<Tuple<short, short>, Tuple<short, short>> GetNext()
        {
            return new Tuple<Tuple<short, short>, Tuple<short, short>>(new Tuple<short, short>(0, 0), new Tuple<short, short>(0, 0));
        }

    }
}
