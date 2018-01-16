using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public abstract class BougerRandom : Action
{
    protected Quaternion direction;

    public BougerRandom(long p_duree, Quaternion p_direction) : base(p_duree) {
        direction = p_direction;
    }

    public abstract override IEnumerator execute(Connaissances connaissances);

    public abstract override void mutate();

    public Quaternion getDirection()
    {
        return direction;
    }

    public void setDirection(Quaternion p_direction)
    {
        direction = p_direction;
    }
}