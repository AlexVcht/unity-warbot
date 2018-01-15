using UnityEngine;

public class BougerRandom : Action
{
    private Quaternion direction;

    public BougerRandom(long p_duree, Quaternion p_direction) : base(p_duree) {
        direction = p_direction;
    }

    public override void execute(GameObject obj, Connaissances connaissances)
    {

    }

    public override void mutate()
    {

    }

    public Quaternion getDirection()
    {
        return direction;
    }

    public void setDirection(Quaternion p_direction)
    {
        direction = p_direction;
    }
}