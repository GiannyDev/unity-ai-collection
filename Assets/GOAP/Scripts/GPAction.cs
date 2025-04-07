public class GPAction
{
    public string name;
    public int cost;
    public WorldState preConditions;
    public WorldState postConditions;

    public GPAction(string actionName)
    {
        name = actionName;
        cost = 1;
        preConditions = new WorldState();
        postConditions = new WorldState();
    }
}