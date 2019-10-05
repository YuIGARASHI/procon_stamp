using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace procon_stamp.model {
    class Instance {

        private List<Stamp> origin_stamp_object_list;
        private List<Stamp> combined_stamp_object_list;

        pubilc pubilc()
        {
            origin_stamp_object_list = new List<Stamp>();
            combined_stamp_object_list = new List<Stamp>();
        }

        /// <summary>
        /// 引数のStampクラスのオブジェクトをstamp_object_listにセットする。
        /// </summary>
        /// <param name="stamp_object">スタンプクラスのオブジェクト</param>
        public void SetOriginStampObject(List<Stamp> stamp_object)
        {
            this.origin_stamp_object_list = stamp_object;
        }

        /// <summary>
        /// できるだけ面積の小さい combined stamp を構築する。
        /// </summary>
        public void MakeCombinedStampList()
        {
            // ひとまずoriginal stamp listをそのまま使う
            // NOTE: deepcopyしていないが大丈夫か...？
            this.combined_stamp_object_list = this.origin_stamp_object_list;
        }

        public List<Stamp> GetCombinedStampObjectList()
        {
            return this.combined_stamp_object_list;
        }
    }
}
