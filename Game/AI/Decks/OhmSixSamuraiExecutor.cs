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
        public class CardId
        {
            // Monsters
            public const int GreatShogunShien = 63176202;
            public const int EnishiShienChancellor = 38280762;
            public const int GrandmasterOfTheSixSamurai = 83039729;
            public const int Kizan = 49721904;
            public const int Kageki = 2511717;
            public const int Irou = 27782503;
            public const int Zanji = 95519486;
            public const int Yaichi = 64398890;
            public const int Squire = 33883834;
            public const int Mizuho = 74094021;
            public const int Shinai = 48505422;

            // Spells
            public const int ReinforcementOfTheArmy = 32807846;
            public const int GracefulCharity = 79571449;
            public const int SixSamuraiUnited = 72345736;
            public const int ShienSmokeSignal = 54031490;
            public const int PotOfAvanice = 67169062;
            public const int LinkBurst = 73287067;
            public const int CardTrader = 48712195;
            public const int AsceticismOfTheSixSamurai = 27821104;
            public const int GatewayOfTheSix = 27970830;
            public const int TempleOfTheSix = 53819808;
            public const int ShienDojo = 47436247;

            // Traps
            public const int JarOfGreed = 83968380;
            public const int JarOfAvarice = 98954106;

            // -Extra
            public const int Rihab = 33964637;  // Fusion
            public const int ShiEn = 29981921; // Synchro
            public const int Shien = 1828513; // Exceed
            public const int GreatGeneral = 74752631; // Link
        }
    }
}
