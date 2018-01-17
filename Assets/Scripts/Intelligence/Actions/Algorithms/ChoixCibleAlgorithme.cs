using System;
using UnityEngine;

public interface AlgoChoixCible
{
    Transform compare(Connaissances connaissances, Transform tank);
}

public class ChoixCibleAlgorithmes
{
    public static AlgoChoixCible getRandomAlgo()
    {
        float r = UnityEngine.Random.Range(0, 2);
        if (r < 1)
        {
            return new Closest();
        }
        else if (r < 2)
        {
            return new Furthest();
        }
        throw new NotImplementedException();
    }

    public class Closest : AlgoChoixCible
    {
        public Transform compare(Connaissances connaissances, Transform tank)
        {
            Transform closest = null;
            foreach (Connaissances.Connaissance con in connaissances.connaissances)
            {
                if (closest == null || Vector3.Distance(con.getAgent().position, tank.position) < Vector3.Distance(closest.position, tank.position))
                    closest = con.getAgent().transform;
            }
            return closest;
        }
    }

    public class Furthest : AlgoChoixCible
    {
        public Transform compare(Connaissances connaissances, Transform tank)
        {
            Transform closest = null;
            foreach (Connaissances.Connaissance con in connaissances.connaissances)
            {
                if (closest == null || Vector3.Distance(con.getAgent().position, tank.position) > Vector3.Distance(closest.position, tank.position))
                    closest = con.getAgent().transform;
            }
            return closest;
        }
    }
}
