using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindBot.Game.AI.Decks
{
    [Deck("SixSamurai", "AI_OhmSixSamurai")]
    public class OhmSixSamuraiExecutor : DefaultExecutor
    {
        public OhmSixSamuraiExecutor(GameAI ai, Duel duel) 
            : base(ai, duel)
        {

        }
    }
}
