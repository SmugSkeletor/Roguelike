using Roguelike.Behaviors;
using Roguelike.Systems;

namespace Roguelike.Core
{
    public abstract class Monster : Actor
    {
        public int? TurnsAlerted { get; set; }

        public virtual void PerformAction(Commands commands)
        {
            var ai = new Basic();
            ai.Act(this, commands);
        }

        //public abstract void SetAttributes();

    }
}