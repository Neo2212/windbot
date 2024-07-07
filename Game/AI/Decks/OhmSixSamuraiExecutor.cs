using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WindBot.Game.AI.Decks
{
    [Deck("SixSamurai", "AI_OhmSixSamurai")]
    public class OhmSixSamuraiExecutor : DefaultExecutor
    {
        private const int _oneStar = 1;
        private const int _fourStar = 4;
        private const int _fiveStar = 5;
        private int _bushidoCounter = default;

        private readonly List<int> _fourthStarSamurai = new List<int>() {
            CardId.Kizan,
            CardId.Kageki,
            CardId.Irou,
            CardId.Zanji,
            CardId.Yaichi,
            CardId.Mizuho,
            CardId.Shinai
        };

        private readonly List<int> _searchSpell = new List<int>() {
            CardId.ReinforcementOfTheArmy,
            CardId.ShienSmokeSignal
        };

        #region CONSTRUCTOR

        public OhmSixSamuraiExecutor(GameAI ai, Duel duel)
            : base(ai, duel)
        {
            // Traps Activate
            AddExecutor(ExecutorType.Activate, CardId.JarOfGreed);

            // Draw Speel Activate
            AddExecutor(ExecutorType.Activate, CardId.PotOfGreed);
            AddExecutor(ExecutorType.Activate, CardId.GracefulCharity);

            // Six Samurai Spell Search
            AddExecutor(ExecutorType.Activate, CardId.ReinforcementOfTheArmy, ReinforcementOfTheArmyEffect);
            AddExecutor(ExecutorType.Activate, CardId.ShienSmokeSignal, ShienSmokeSignalEffect);

            // Destroy Spell
            AddExecutor(ExecutorType.Activate, CardId.LinkBurst, LinkBurstEffect);

            // Core Spell
            AddExecutor(ExecutorType.Activate, CardId.TempleOfTheSix, TempleOfTheSixEffect);
            AddExecutor(ExecutorType.Activate, CardId.ShienDojo, ShienDojoEffect);
            AddExecutor(ExecutorType.Activate, CardId.GatewayOfTheSix, GatewayOfTheSixEffect);

            // Summon ForthStarSamuri
            AddExecutor(ExecutorType.Summon, CardId.Kageki, KagekiNormalSummon);
            AddExecutor(ExecutorType.MonsterSet, CardId.Kageki, KagekiSummonSet);
            AddExecutor(ExecutorType.SummonOrSet, CardId.Kizan, HasKagekiInHand);
            AddExecutor(ExecutorType.SummonOrSet, CardId.Mizuho, HasKagekiInHand);
            AddExecutor(ExecutorType.SummonOrSet, CardId.Shinai, HasKagekiInHand);
            AddExecutor(ExecutorType.SpSummon, CardId.Kizan);
            AddExecutor(ExecutorType.SpSummon, CardId.Mizuho);
            AddExecutor(ExecutorType.SpSummon, CardId.Shinai);
        }

        #endregion CONSTRUCTOR

        #region CARD_MAPPING

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

            public const int PotOfGreed = 55144522;
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

        #endregion CARD_MAPPING

        #region CORE_SPELL

        private bool GatewayOfTheSixEffect()
        {
            return false;
        }

        private bool ShienDojoEffect()
        {
            return false;
        }

        private bool TempleOfTheSixEffect()
        {
            return false;
        }

        private bool ReinforcementOfTheArmyEffect()
        {
            if (!Bot.HasInHand(CardId.ShienSmokeSignal))
            {
                if (!Bot.HasInHand(CardId.Kageki))
                    AI.SelectCard(CardId.Kageki);
                AI.SelectCard(CardId.Kizan,
                              CardId.Mizuho,
                              CardId.Shinai,
                              CardId.Squire,
                              CardId.Yaichi,
                              CardId.Irou,
                              CardId.Zanji);
                return true;
            }
            return false;
        }

        private bool ShienSmokeSignalEffect()
        {
            if (!Bot.HasInHand(CardId.Kageki))
            {
                AI.SelectCard(CardId.Kageki,
                              CardId.Shinai,
                              CardId.Mizuho,
                              CardId.Yaichi);
                return true;
            }
            return false;
        }

        #endregion CORE_SPELL


        #region SUMMON_LOGIC
        private bool HasKagekiInHand()
        {
            if (Bot.HasInHand(CardId.Kageki) 
                || Bot.HasInHand(_searchSpell))
                return false;
            return true;
        }

        private bool KagekiNormalSummon()
        {
            if (Bot.HasInHand(_fourthStarSamurai))
                return true;
            return false;
        }

        private bool KagekiSummonSet()
        {
            if (Enemy.GetMonsters().Count() > 0 && CompareEnemyAtkToLifePoint())
                return true;
            return false;
        }

        #endregion SUMMON_LOGIC

        #region EXTRA_MONSTER_SUMMON_LOGIC

        public override IList<ClientCard> OnSelectSynchroMaterial(IList<ClientCard> cards, int sum, int min, int max)
        {
            Logger.DebugWriteLine("OnSelectSynchroMaterial " + cards.Count + " " + sum + " " + min + " " + max);
            if (sum != _fiveStar)
                return null;

            if (Bot.MonsterZone.Any(monster => monster.Level.Equals(_oneStar))
                && Bot.MonsterZone.Any(monster => monster.Level.Equals(_fourStar))
                && Bot.HasInExtra(CardId.GreatGeneral))
            {
                return Bot.MonsterZone
                        .Where(monster => monster.Level == _fourStar || monster.Level == _oneStar)
                        .ToList();
            };

            return null;
        }

        #endregion EXTRA_MONSTER_SUMMON_LOGIC

        #region DESTROY_ENEMY_CARD_LOGIC

        private bool LinkBurstEffect()
        {
            return 1 < Bot.GetMonsterCount() && Bot.GetMonstersExtraZoneCount() > 0;
        }

        #endregion DESTROY_ENEMY_CARD_LOGIC

        #region EXTENSION_METHOD

        /// <summary>
        /// Compare all ATK of enemy monsters in field
        /// </summary>
        /// <returns>true when all attack is more than bot life point,
        /// otherwise return false</returns>
        private bool CompareEnemyAtkToLifePoint()
            => Enemy.GetMonsters().Sum(monster => monster.Attack) >= Bot.LifePoints;

        /// <summary>
        /// Check the monster that contain "Six Samurai" name
        /// </summary>
        /// <param name="name">monster name</param>
        /// <returns>true when match the "Six Samurai" name,
        /// otherwise return false</returns>
        private bool IsMatchSixSamuraiName(string name)
            => Regex.IsMatch(name, @"(Six Samurai)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Check the monster that contain "Six Samurai" name
        /// </summary>
        /// <param name="name">monster name</param>
        /// <returns>true when match the "Shien" name,
        /// otherwise return false</returns>
        private bool IsMatchShienName(string name)
            => Regex.IsMatch(name, @"(Shien)", RegexOptions.IgnoreCase);

        /// <summary>
        /// Check the monster that contain "Six Samurai" name
        /// </summary>
        /// <param name="name">monster name</param>
        /// <returns>true when match the "Six Samurai" or "Shien",
        /// otherwise return false</returns>
        private bool IsMatchBothSamuraiAndShien(string name)
            => Regex.IsMatch(name, @"(Six Samurai|Shien)", RegexOptions.IgnoreCase);

        #endregion EXTENSION_METHOD
    }
}