using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roguelike.MonsterFactories
{

    public enum FactoryType
    {
        KOBOLD,
        ORC,
        GOBLIN,
        BEHOLDER,
        GOBLIN_SHAMAN
    }


    public static class MonsterFactoryStore
    {
        private static Dictionary<FactoryType,MonsterFactory> factoryDictionary;

        static MonsterFactoryStore()
        {
            factoryDictionary = new Dictionary<FactoryType, MonsterFactory>();
            factoryDictionary.Add(FactoryType.KOBOLD, new KoboldFactory());
            factoryDictionary.Add(FactoryType.ORC, new OrcFactory());
            factoryDictionary.Add(FactoryType.GOBLIN, new GoblinFactory());
            factoryDictionary.Add(FactoryType.BEHOLDER, new BeholderFactory());
            factoryDictionary.Add(FactoryType.GOBLIN_SHAMAN, new GoblinShamanFactory());
        }

        public static MonsterFactory getFactory(FactoryType type)
        {
            if (factoryDictionary.ContainsKey(type))
                return factoryDictionary[type];
            else return new KoboldFactory();              //w zasadzie nie potrzebne to sprawdzanie ale dla pewności jest
        }


    }
}
