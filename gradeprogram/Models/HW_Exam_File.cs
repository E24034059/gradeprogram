//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace gradeprogram.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HW_Exam_File
    {
        public int Course_ID { get; set; }
        public string HW_Exam_Number { get; set; }
        public string HW_Exam_FilePath { get; set; }
    
        public virtual Class Class { get; set; }
    }
}
