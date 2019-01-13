using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using Roguelike.Core;

namespace Roguelike.Pickups
{
    public enum PickupType{
        GOLD_COIN,
        DEF_POTION,
        EXP_POTION,
        HP_POTION,
        STR_POTION
    }
    public sealed class PickupFactory
    {
        private static PickupFactory instance = null;
        private Dictionary<PickupType, Pickup> pickups;
        private PickupFactory()
        {
            pickups = new Dictionary<PickupType, Pickup>();
        }
        public static PickupFactory GetInstance()
        {
            if (instance == null)
                instance = new PickupFactory();
            return instance;
        }
        public Pickup getPickup(PickupType type)
        {
            Pickup pickup;
            if(pickups.TryGetValue(type, out Pickup value))
            {
                return value;
            }
            else
            {
                switch(type)
                {
                    case PickupType.GOLD_COIN:
                        { 
                            pickup = new GoldCoin();
                            pickups.Add(type,pickup);
                            return pickup;
                        }
                    case PickupType.DEF_POTION:
                        { 
                            pickup = new DefPotion();
                            pickups.Add(type, pickup);
                            return pickup;
                        }
                    case PickupType.EXP_POTION:
                        {
                            pickup = new ExpPotion();
                            pickups.Add(type, pickup);
                            return pickup;
                        }
                    case PickupType.HP_POTION:
                        { 
                            pickup = new HpPotion();
                            pickups.Add(type, pickup);
                            return pickup;
                        }
                    case PickupType.STR_POTION:
                        { 
                            pickup = new StrPotion();
                            pickups.Add(type, pickup);
                            return pickup;
                        }
                    default:
                        return null;
                }
            }
        }


    }
}
