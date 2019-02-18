using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Settings
{
    public class UpgradeSettings : MonoBehaviour
    {
        private List<HeroDetails> herosList;        // All avialable heros with levels and costs
        private List<HeroDetails> playerHerosList;        // All avialable heros with levels and costs

        void Start()
        {
            herosList = new List<HeroDetails>
            {
                new HeroDetails(Hero.Daochan, 0, 2, 100),
                new HeroDetails(Hero.Zhangjiao, 0, 2, 100),
                new HeroDetails(Hero.Zhouyu, 0, 2, 100),
                new HeroDetails(Hero.Zhugeliang, 0, 2, 100)
            };

            playerHerosList = new List<HeroDetails>()
            {
                new HeroDetails(Hero.Daochan, 0, 2, 100),
            };
        }

        public void UpgradeHeroNextLevel(Hero hero, int level)
        {
            // check if hero with specified level exist for upgrade
            var foundHero = herosList.FirstOrDefault(heroDetails => heroDetails.Hero == hero && heroDetails.Level == level + 1);
            if (foundHero != null)
            {
                // check if player has hero for upgrade and upgrade it
                var foundPlayerHero = playerHerosList.FirstOrDefault(heroDetails => heroDetails.Hero == hero && heroDetails.Level == level);
                if (foundPlayerHero != null)
                {
                    foundPlayerHero.Level += 1;
                    // TODO: reduce money from player (i.e. player.money -= foundHero.Cost
                }

            }
        }

        public HeroDetails GetplayerHeroDetails(Hero hero)
        {
            var foundPlayerHero = playerHerosList.FirstOrDefault(heroDetails => heroDetails.Hero == hero);
            if (foundPlayerHero != null)
                return foundPlayerHero;

            return null;
        }
    }

    public class HeroDetails
    {
        public Hero Hero { get; set; }
        public int Level { get; set; }        
        public int Damage { get; set; }
        public int Cost { get; set; }

        public HeroDetails(Hero hero, int level, int damage, int cost)
        {
            Hero = hero;
            Cost = cost;
            Damage = damage;
            Level = level;
        }
    }
}
