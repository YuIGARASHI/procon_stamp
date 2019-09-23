using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon_stamp.model {
    class Instance {

        private List<Tuple<int, int, int>> origin_stamp_object_list;
        private List<Tuple<int, int, int>> combined_stamp_object_list;

        /// <summary>
        /// 引数のStampクラスのオブジェクトをstamp_object_listにセットする。
        /// </summary>
        /// <param name="stamp_object">スタンプクラスのオブジェクト</param>
        public void SetOriginStampObject(List<Tuple<int, int, int>> stamp_object)
        {
            this.origin_stamp_object_list = new List<Tuple<int, int, int>>(stamp_object);
        }

        /// <summary>
        /// できるだけ面積の小さい combined stamp を構築する。
        /// </summary>
        public void MakeCombinedStampList()
        {
            this.combined_stamp_object_list = new List<Tuple<int, int, int>>(this.origin_stamp_object_list);
        }


        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <returns>combined stamp</returns>
        public List<Tuple<int, int, int>> GetCombinedStampObjectList()
        {
            return this.combined_stamp_object_list;
        }
    }
}
