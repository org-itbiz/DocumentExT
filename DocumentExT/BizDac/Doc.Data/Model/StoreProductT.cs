using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Doc.Data.Model
{
    [Table("STORE_PRODUCT")]
    public partial class StoreProductT
    {
        [Key, Column("NO")]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public long NO { get; set; }
        public string VAR_NO { get; set; }
        public string NAME { get; set; }
        public string FILE_NAME { get; set; }
        public string FILE_RENAME { get; set; }
        public string FILE_EXT { get; set; }
        public string MIME_TYPE { get; set; }
        public double FILE_SIZE { get; set; }
        public Nullable<double> MATERIAL_VOLUME { get; set; }
        public Nullable<double> OBJECT_VOLUME { get; set; }
        public Nullable<double> SIZE_X { get; set; }
        public Nullable<double> SIZE_Y { get; set; }
        public Nullable<double> SIZE_Z { get; set; }
        public Nullable<double> SCALE { get; set; }
        public string SHORT_LINK { get; set; }
        public string VIDEO_URL { get; set; }
        public Nullable<int> CATEGORY_NO { get; set; }
        public string CONTENTS { get; set; }
        public string DESCRIPTION { get; set; }
        public Nullable<int> PART_CNT { get; set; }
        public string CUSTERMIZE_YN { get; set; }
        public string SELL_YN { get; set; }
        public string TAG_NAME { get; set; }
        public int CERTIFICATE_YN { get; set; }
        public string VISIBILITY_YN { get; set; }
        public string USE_YN { get; set; }
        public int MEMBER_NO { get; set; }
        public Nullable<double> TXT_SIZE_X { get; set; }
        public Nullable<double> TXT_SIZE_Y { get; set; }
        public Nullable<int> DETAIL_TYPE { get; set; }
        public Nullable<int> DETAIL_DEPTH { get; set; }
        public string TXT_LOC { get; set; }
        public System.DateTime REG_DT { get; set; }
        public string REG_ID { get; set; }
        public Nullable<System.DateTime> UPD_DT { get; set; }
        public string UPD_ID { get; set; }
    }


    public class StoreProductExT
    {
        public string TEST { get; set; }
    } 
}