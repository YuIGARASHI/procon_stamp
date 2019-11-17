using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StampLib.model
{
    public class Instance
    {

        private List<Stamp> origin_stamp_object_list;
        private List<CombinedStamp> combined_stamp_object_list;
        private Field field;

        public Instance()
        {
            this.origin_stamp_object_list = new List<Stamp>();
            this.combined_stamp_object_list = new List<CombinedStamp>();
            this.field = new Field();
        }

        public void SetOriginStampObject(List<Stamp> stamp_object)
        {
            this.origin_stamp_object_list = stamp_object;
        }

        public List<CombinedStamp> GetCombinedStampObjectList()
        {
            return this.combined_stamp_object_list;
        }

        public List<Stamp> GetOriginalStampObjectList()
        {
            return this.origin_stamp_object_list;
        }

        public void SetField( Field field )
        {
            this.field = field;
        }

        public Field GetField()
        {
            return this.field;
        }

        /// <summary>
        /// できるだけ面積の小さい combined stamp を構築する。
        /// </summary>
        public void MakeCombinedStampList()
        {
            // とりあえず仮実装
            foreach(var origin_stamp in this.origin_stamp_object_list)
            {
                CombinedStamp cs = new CombinedStamp();
                cs.AddOriginalStamp(this, origin_stamp.GetOriginStampIndex(), 0, 0);
                this.combined_stamp_object_list.Add(cs);
            }
        }

        /// <summary>
        /// Combinedスタンプから構成される解をOriginalスタンプの解に変換する
        /// </summary>
        /// <param name="combined_solution">Combinedスタンプから成る解</param>
        /// <returns>Originalスタンプから構成される解</returns>
        public Solution CombinedSolutionToOriginalSolution(Solution combined_solution)
        {
            // combined_stampをオリジナルスタンプに分解してtmp_origin_solutionに追加
            Solution tmp_origin_solution = new Solution();
            {
                List<Tuple<Stamp, short, short>> combined_answer_list = combined_solution.GetStampAnswerList();
                foreach (var combined_answer in combined_answer_list)
                {
                    CombinedStamp combined_stamp = (CombinedStamp)combined_answer.Item1;
                    List<Tuple<short, short, short>> origin_stamp_config_list = combined_stamp.GetOriginStampConfigList();
                    short combined_stamp_slide_x = combined_answer.Item2;
                    short combined_stamp_slide_y = combined_answer.Item3;

                    foreach (var origin_stamp_config in origin_stamp_config_list)
                    {
                        short origin_stamp_idx = origin_stamp_config.Item1;
                        short origin_stamp_slide_x = origin_stamp_config.Item2;
                        short origin_stamp_slide_y = origin_stamp_config.Item3;
                        Stamp origin_stamp_object = this.origin_stamp_object_list[origin_stamp_idx];
                        tmp_origin_solution.AddStampAnswer(origin_stamp_object,
                                                           (short)(origin_stamp_slide_x + combined_stamp_slide_x),
                                                           (short)(origin_stamp_slide_y + combined_stamp_slide_y));
                    }
                }
            }

            // 枠外にはみ出たオリジナルスタンプ以外をorigin_solutionに追加
            Solution origin_solution = new Solution();
            {
                List<Tuple<Stamp, short, short>> origin_stamp_answer_list = tmp_origin_solution.GetStampAnswerList();

                foreach (var origin_stamp_answer in origin_stamp_answer_list)
                {
                    Stamp stamp = origin_stamp_answer.Item1;
                    short slide_x = origin_stamp_answer.Item2;
                    short slide_y = origin_stamp_answer.Item3;

                    // x方向チェック
                    if (stamp.GetXSize() + slide_x <= 0 || slide_x >= field.GetXSize())
                    {
                        continue;
                    }

                    // y方向チェック
                    if (stamp.GetYSize() + slide_y <= 0 || slide_y >= field.GetYSize())
                    {
                        continue;
                    }

                    origin_solution.AddStampAnswer(stamp, slide_x, slide_y);
                }
            }

            return origin_solution;
        }
    }
}
