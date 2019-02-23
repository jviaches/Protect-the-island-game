using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.Settings
{
    public class UpgradeSettings : MonoBehaviour
    {
        public List<HeroDetails> HerosList;        // All avialable heros with levels and costs
        public List<HeroDetails> PlayerHerosList;        // All avialable heros with levels and costs

        void Start()
        {
            HerosList = new List<HeroDetails>
            {
                new HeroDetails(Hero.Daochan, 1, 2, 100, "Master of .."),
                new HeroDetails(Hero.Zhangjiao, 1, 4, 100, "Master of .."),
                new HeroDetails(Hero.Zhouyu, 1, 6, 100, "Master of .."),
                new HeroDetails(Hero.Zhugeliang, 1, 8, 100, "Master of ..")
            };

            PlayerHerosList = new List<HeroDetails>()  { HerosList[0] };
        }

        public void UpgradeHeroNextLevel(Hero hero)
        {
            print("Upgrade hero=" +hero+" invoked");
            // check if hero with specified level exist for upgrade
            var foundHero = HerosList.FirstOrDefault(heroDetails => heroDetails.Hero == hero);
            if (foundHero == null)
                return;

            // check if player has hero for upgrade and upgrade it
            var foundPlayerHero = PlayerHerosList.FirstOrDefault(heroDetails => heroDetails.Hero == hero);
            if (foundPlayerHero != null)
                foundPlayerHero.Level += 1;
            else
                PlayerHerosList.Add(foundHero);

            // TODO: reduce money from player (i.e. player.money -= foundHero.Cost

        }

        public HeroDetails GetplayerHeroDetails(Hero hero)
        {
            var foundPlayerHero = PlayerHerosList.FirstOrDefault(heroDetails => heroDetails.Hero == hero);
            if (foundPlayerHero != null)
                return foundPlayerHero;

            return null;
        }
        
    }

    [Serializable]
    public class HeroDetails
    {
        public Hero Hero { get; set; }
        public int Level { get; set; }        
        public int Damage { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }

        public HeroDetails(Hero hero, int level, int damage, int cost, string description)
        {
            Hero = hero;
            Cost = cost;
            Damage = damage;
            Level = level;
            Description = description;
        }
    }
}
