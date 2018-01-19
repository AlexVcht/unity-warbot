using System.Collections;
using UnityEngine;

public class BougerRandomScout : BougerRandom
{

    public BougerRandomScout(long p_duree, Quaternion p_direction) : base(p_duree, p_direction) {
    }

    public static BougerRandomScout createRandom()
    {
        return new BougerRandomScout(getRandomDuree(), getRandomDirection());
    }

    public override IEnumerator execute(Connaissances connaissances)
    {
        Debug.Log("Scout BougerRandom");
        yield return scout.BougerRandom(duree, direction);
    }
}