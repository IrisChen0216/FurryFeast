using System;
using System.Collections.Generic;

namespace FurryFeast.Models
{
    public partial class GameQue
    {
        public GameQue()
        {
            GameQuesChoices = new HashSet<GameQuesChoice>();
        }

        public int QuesId { get; set; }
        public int PetTypesId { get; set; }
        public string QuesContent { get; set; } = null!;

        public virtual PetType PetTypes { get; set; } = null!;
        public virtual ICollection<GameQuesChoice> GameQuesChoices { get; set; }
    }
}
