using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class EditedMsgRecord
    {
        public int EditedMsgId { get; set; }
        public int MsgId { get; set; }
        public string EditedText { get; set; } = null!;
        public DateTime EditedTime { get; set; }

        public virtual MsgBoard Msg { get; set; } = null!;
    }
}
