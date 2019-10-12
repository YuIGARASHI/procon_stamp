﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampLib.model
{
    public class Instance
    {

        private List<Stamp> origin_stamp_object_list;
        private List<Stamp> combined_stamp_object_list;

        public Instance()
        {
            origin_stamp_object_list = new List<Stamp>();
            combined_stamp_object_list = new List<Stamp>();
        }

        public void SetOriginStampObject(List<Stamp> stamp_object)
        {
            this.origin_stamp_object_list = stamp_object;
        }

        public List<Stamp> GetCombinedStampObjectList()
        {
            return this.combined_stamp_object_list;
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
    }
}