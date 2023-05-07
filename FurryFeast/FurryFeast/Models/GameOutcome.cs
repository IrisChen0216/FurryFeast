using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class GameOutcome
    {
        public int AnswerId { get; set; }
        public int ChoicesId { get; set; }
        public string AnswerResult { get; set; } = null!;

        public virtual GameQuesChoice Choices { get; set; } = null!;
    }
}
