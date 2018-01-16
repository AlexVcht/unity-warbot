using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class BougerRandomScout : BougerRandom
{

    public BougerRandomScout(long p_duree, Quaternion p_direction) : base(p_duree, p_direction) {
    }

    public override IEnumerator execute(Connaissances connaissances)
    {
        yield return scout.BougerRandom(duree, direction);
    }

    public override void mutate()
    {

    }
}