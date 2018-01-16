using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class BougerRandomTank : BougerRandom
{

    public BougerRandomTank(long p_duree, Quaternion p_direction) : base(p_duree, p_direction) {
    }

    public override IEnumerator execute(Connaissances connaissances)
    {
        yield return tank.BougerRandom(duree, direction);
    }

    public override void mutate()
    {

    }
}