using System;
using UnityEngine;

public interface AlgoChoixCible
{
    Rigidbody compare(Connaissances connaissances, Transform tank, float closest, float insertedWhen);
}

public class ChoixCibleAlgorithmes
{
    public static AlgoChoixCible getRandomAlgo()
    {
        float r = UnityEngine.Random.Range(0, 2);
        if (r == 0)
        {
            return new Closest();
        }
        else if (r == 1)
        {
            return new Furthest();
        }
        throw new NotImplementedException();
    }

    public class Closest : AlgoChoixCible
    {
        public Rigidbody compare(Connaissances connaissances, Transform tank, float closest, float insertedWhen)
        {
            Rigidbody closestPos = null;
            foreach (Connaissances.Connaissance con in connaissances.connaissances)
            {
                if (closestPos == null || Vector3.Distance(con.getAgent().position, tank.position) < Vector3.Distance(closestPos.position, tank.position))
                    closestPos = con.getAgent();
            }
            return closestPos;
        }

        public override string ToString()
        {
            return "Closest";
        }
    }

    public class Furthest : AlgoChoixCible
    {
        public Rigidbody compare(Connaissances connaissances, Transform tank, float closest, float insertedWhen)
        {
            Rigidbody closestPos = null;
            foreach (Connaissances.Connaissance con in connaissances.connaissances)
            {
                if (closestPos == null || Vector3.Distance(con.getAgent().position, tank.position) > Vector3.Distance(closestPos.position, tank.position))
                    closestPos = con.getAgent();
            }
            return closestPos;
        }
        public override string ToString()
        {
            return "Furthest";
        }
    }

    public class Percentage : AlgoChoixCible
    {
        public Rigidbody compare(Connaissances connaissances, Transform tank, float closest, float insertedWhen)
        {
            //TODO
            return null;
        }
        public override string ToString()
        {
            return "Percentage";
        }
    }
}
