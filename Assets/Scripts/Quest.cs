public class Quest
{
    public string Name;
    public Rank Rank;
    public QuestType Type;
    public QuestReward Reward;
    public string Content;
    public string Giver;
}

public enum QuestType
{
    Hunt,
    Gather
}

public class QuestReward
{
    public QuestRewardType Type;
    public int Amount;
    public int ItemID;
}

public enum QuestRewardType
{
    Money,
    Item
}
