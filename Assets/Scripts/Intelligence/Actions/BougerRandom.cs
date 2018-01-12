public class BougerRandom : Action
{
    private int direction;

    public BougerRandom(long p_duree, int p_direction) : base(p_duree) {
        direction = p_direction;
    }

    public override void execute()
    {
        throw new System.NotImplementedException();
    }

    public override void mutate()
    {
        throw new System.NotImplementedException();
    }

    public int getDirection()
    {
        return direction;
    }

    public void setDirection(int p_direction)
    {
        direction = p_direction;
    }
}