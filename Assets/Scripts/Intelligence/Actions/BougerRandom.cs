using UnityEngine;

public class BougerRandom : Action
{
    private Transform direction;

    public BougerRandom(long p_duree, Transform p_direction) : base(p_duree) {
        direction = p_direction;
    }

    public override void execute(Connaissances connaissances, int x, int y)
    {
    }

    public override void mutate()
    {

    }

    public Transform getDirection()
    {
        return direction;
    }

    public void setDirection(Transform p_direction)
    {
        direction = p_direction;
    }
}