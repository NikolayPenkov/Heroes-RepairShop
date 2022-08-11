using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly Dictionary<string, IHero> heroes;

        public HeroRepository()
        {
            this.heroes = new Dictionary<string, IHero>();
        }
        public IReadOnlyCollection<IHero> Models => this.heroes.Values;

        public void Add(IHero model) => this.heroes.Add(model.Name, model);

        public IHero FindByName(string name)
        {
            if (this.heroes.ContainsKey(name))
            {
                return this.heroes[name];
            }
            return null;
        }

        public bool Remove(IHero model)
        {
            if (this.heroes.ContainsKey(model.Name))
            {
                return true;
            }
            return false;
        }
    }
}
