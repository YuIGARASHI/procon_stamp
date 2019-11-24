using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StampLib.model;

namespace StampLib.algorithm {

    public class FieldDivider {

        private short divided_field_counter;
        private short divided_field_x_division;
        private short divided_field_y_division;
        private short divided_field_count;
        private List<Tuple<Tuple<short, short>, Tuple<short, short>>> start_end_position;
        private Field field;

        // コンストラクタ
        public FieldDivider(short divided_field_y_division, short divided_field_x_division, Field field)
        {
            // 分割したフィールドの場所を示すポインタ
            this.divided_field_counter = -1;

            // ｘ、ｙ軸分割単位
            this.divided_field_y_division = divided_field_y_division;
            this.divided_field_x_division = divided_field_x_division;

            this.field = field;

            this.divided_field_count = (short)(this.divided_field_y_division * this.divided_field_x_division);

            // 初期化
            this.start_end_position = new List<Tuple<Tuple<short, short>, Tuple<short, short>>>();

        }

        public bool HasNext()
        {
            if (this.divided_field_counter < this.divided_field_count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 次の分割された開始点、終了点を返す
        public Tuple<Tuple<short, short>, Tuple<short, short>> GetNext()
        {
            this.divided_field_counter += 1;
            return start_end_position[this.divided_field_counter];
        }

        public void CalcStartEndPosition(short Y_Size, short X_Size)
        {
            
            short start_point_y = 0;
            short start_point_x = 0;
            short end_point_y = 0;
            short end_point_x = 0;

            short quotient_y = (short)(this.field.GetYSize() / this.divided_field_y_division);
            short remainder_y = (short)(this.field.GetYSize() % this.divided_field_y_division);
            short quotient_x = (short)(this.field.GetXSize() / this.divided_field_x_division);
            short remainder_x = (short)(this.field.GetXSize() % this.divided_field_x_division);

            for (short y = 0; y < this.divided_field_y_division; y++)
            {
                for (short x = 0; x < this.divided_field_x_division; x++)
                {
                    // 初回の処理（余り考慮する）
                    if (y == 0 && x == 0)
                    {
                        start_point_y = 0;
                        start_point_x = 0;
                        end_point_y = (short)((quotient_y + remainder_y) - 1);
                        end_point_x = (short)((quotient_x + remainder_x) - 1);
                    }
                    else if (y != 0 && x == 0)
                    {
                        start_point_y = (short)(quotient_y * y + remainder_y);
                        start_point_x = 0;
                        end_point_y = (short)(quotient_y * (y + 1) + remainder_y - 1);
                        end_point_x = (short)((quotient_x + remainder_x) - 1);
                    }
                    else if (y == 0 && x != 0)
                    {
                        start_point_y = 0;
                        start_point_x = (short)(quotient_x * x + remainder_x);
                        end_point_y = (short)((quotient_y + remainder_y) - 1);
                        end_point_x = (short)(quotient_x * (x + 1) + remainder_x - 1);
                    }
                    else
                    {
                        start_point_y = (short)(quotient_y * y + remainder_y);
                        start_point_x = (short)(quotient_x * x + remainder_x);
                        end_point_y = (short)(quotient_y * (y + 1) + remainder_y - 1);
                        end_point_x = (short)(quotient_x * (x + 1) + remainder_x - 1);
                    }

                    this.start_end_position.Add(new Tuple<Tuple<short, short>, Tuple<short, short>>(new Tuple<short, short>(start_point_y, start_point_x), new Tuple<short, short>(end_point_y, end_point_x)));
                }
            }
        }

        public List<Tuple<Tuple<short, short>, Tuple<short, short>>> GetStartEndPosition()
        {
            return this.start_end_position;
        }
    }
}
