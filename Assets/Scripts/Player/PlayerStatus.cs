using System.Collections.Generic;

public class PlayerStatus
{
    public string Name;
    public Race Race;
    public Gender Gender;
    public int Age;
    public Rank Rank;
    public Job Job;
    public int Level;

    public int HP;
    public int MP;
    public int STR;
    public int AGI;

    public int CurrentHP;
    public int CurrentMP;

    public List<Skill> Skills = new();
    public List<Equipment> Equipments = new();
    public List<Item> Items = new();
}

public enum Gender
{
    Male,
    Female
}
