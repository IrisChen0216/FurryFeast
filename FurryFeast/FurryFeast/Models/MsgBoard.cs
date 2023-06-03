using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class MsgBoard
    {
        public MsgBoard()
        {
            EditedMsgRecords = new HashSet<EditedMsgRecord>();
        }

        public int MsgId { get; set; }
        public int MsgRecipesId { get; set; }
        public int UserId { get; set; }
        public string MsgContent { get; set; } = null!;
        public DateTime MsgDateTime { get; set; }
        public bool MsgActive { get; set; }

        public virtual Recipe MsgRecipes { get; set; } = null!;
        public virtual Member User { get; set; } = null!;
        public virtual ICollection<EditedMsgRecord> EditedMsgRecords { get; set; }
    }
}
