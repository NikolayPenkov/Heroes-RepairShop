using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Heroes.Models.Heroes;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            var knights = new List<Knight>();
            var barbarians = new List<Barbarian>();
            foreach (var player in players)
            {
                if (player.IsAlive)
                {
                    if (player is Knight knight)
                    {
                        knights.Add(knight);
                    }
                    else if (player is Barbarian barbarian)
                    {
                        barbarians.Add(barbarian);
                    }
                    else
                    {
                        throw new ArgumentException("Invalid player type.");
                    }
                }
            }
            var continueBattle = true;
            while (continueBattle)
            {
                var allKnightsAreDead = true;
                var allBarbarianssAreDead = true;

                var aliveKnights = 0;
                var aliveBarbarians = 0;
                foreach (var knight in knights)
                {
                    if (knight.IsAlive)
                    {
                        allKnightsAreDead = false;
                        aliveKnights++;
                    }
                    foreach (var barbarian in barbarians)
                    {
                        var weaponDamage = knight.Weapon.DoDamage();
                        barbarian.TakeDamage(weaponDamage);
                    }
                } 
                foreach (var barbarian in barbarians)
                {
                    if (barbarian.IsAlive)
                    {
                        allBarbarianssAreDead = false;
                        aliveBarbarians++;
                    }
                    foreach (var knight in knights)
                    {
                        var weaponDamage = barbarian.Weapon.DoDamage();
                        knight.TakeDamage(weaponDamage);
                    }
                }

                if (allBarbarianssAreDead)
                {
                    var deathBarbarians = barbarians.Count - aliveBarbarians;
                    return $"The knights took {deathBarbarians} casualties but won the battle.";
                }
                if (allKnightsAreDead)
                {
                    var deathKnights = knights.Count - aliveKnights;
                    return $"The barbarians took {deathKnights} casualties but won the battle.";
                }

            }
            throw new InvalidOperationException("The battle map has a bug.");
            //var knights = players.OfType<Knight>().Where(kn => kn.IsAlive).ToList();

            //var barbarians = players.OfType<Barbarian>().Where(kn => kn.IsAlive).ToList();
        }
    }
}
