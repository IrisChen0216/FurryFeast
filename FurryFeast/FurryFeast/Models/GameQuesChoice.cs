using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class GameQuesChoice
    {
        public GameQuesChoice()
        {
            GameOutcomes = new HashSet<GameOutcome>();
        }

        public int ChoicesId { get; set; }
        public int QuesId { get; set; }
        public string ChoicesContent { get; set; } = null!;

        public virtual GameQue Ques { get; set; } = null!;
        public virtual ICollection<GameOutcome> GameOutcomes { get; set; }
    }
}
