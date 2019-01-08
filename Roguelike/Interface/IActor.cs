namespace Roguelike.Interfaces
{
    public interface IActor
    {
        int Level { get; set; }
        int Attack { get; set; }
        int AtkChance { get; set; }
        int FOVValue { get; set; }
        int Defense { get; set; }
        int DefChance { get; set; }
        int Gold { get; set; }
        int Gems { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        string Name { get; set; }
        int Speed { get; set; }
        int ExpValue { get; set; }
        int Experience { get; set; }
        int ExpToLevel { get; set; }
    }
}