using System.Collections;
using UnityEngine;

public class BougerRandomTank : BougerRandom
{

    public BougerRandomTank(float p_duree, Quaternion p_direction) : base(p_duree, p_direction) {
    }

    public static BougerRandomTank createRandom()
    {
        return new BougerRandomTank(getRandomDuree(), getRandomDirection());
    }

    public override IEnumerator execute(Connaissances connaissances)
    {
        Debug.Log("Tank BougerRandom : " + duree);
        yield return tank.BougerRandom(duree, direction);
    }

}