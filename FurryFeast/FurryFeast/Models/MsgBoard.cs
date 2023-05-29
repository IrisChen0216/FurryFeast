﻿using System;
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
        public string UserId { get; set; } = null!;
        public string MsgContent { get; set; } = null!;
        public DateTime MsgDateTime { get; set; }
        public string MsgActive { get; set; } = null!;

        public virtual Recipe MsgRecipes { get; set; } = null!;
        public virtual ICollection<EditedMsgRecord> EditedMsgRecords { get; set; }
    }
}
