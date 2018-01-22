using System.Collections;
using UnityEngine;

public class MoveToTarget : Action
{
    private float closest;
    private float insertedWhen;
    private AlgoChoixCible choixCible;

    public MoveToTarget(float p_closest, float p_insertedWhen, AlgoChoixCible p_choixCible) :base(0)
    {
        insertedWhen = p_insertedWhen;
        closest = p_closest;
        choixCible = p_choixCible;
    }

    public static MoveToTarget createRandom()
    {
        return new MoveToTarget(getRandomFrequence(), getRandomFrequence(), ChoixCibleAlgorithmes.getRandomAlgo());
    }

    public override IEnumerator execute(Connaissances connaissances)
    {
        Debug.Log((tank == null ? "Scout" : "Tank") + " MoveToTarget");
        Transform t = choixCible.compare(connaissances, tank.transform, closest, insertedWhen);
        if (t)
        {
            Debug.Log("Target found : " + t);
            yield return tank.moveToTarget(t);
        }
        else
            yield return null;
    }

    public override void mutate(float iMutation)
    {
        float r = UnityEngine.Random.Range(0, 1);
        if (r < iMutation)
        {
            closest = getRandomFrequence();
        }
        r = UnityEngine.Random.Range(0, 1);
        if (r < iMutation)
        {
            duree = (long)(duree * getRandomFrequence());
        }
        r = UnityEngine.Random.Range(0, 1);
        if(r < iMutation)
        {
            choixCible = ChoixCibleAlgorithmes.getRandomAlgo();
        }
    }

    public override string ToString()
    {
        return "MoveToTarget(" + closest + ";" + insertedWhen+ ";"+choixCible+")";
    }
}