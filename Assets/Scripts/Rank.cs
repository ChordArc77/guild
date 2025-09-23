using System;

public enum Rank
{
    S,
    A,
    B,
    C,
    D,
    E,
    F
}

public static class RankExtensions
{
    public static string GetText(this Rank rank)
    {
        return rank switch
        {
            Rank.S => "S",
            Rank.A => "A",
            Rank.B => "B",
            Rank.C => "C",
            Rank.D => "D",
            Rank.E => "E",
            Rank.F => "F",
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }
}