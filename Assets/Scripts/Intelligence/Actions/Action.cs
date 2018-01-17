using System;
using System.Collections;
using Random = UnityEngine.Random;

public interface ActionGame
{
    IEnumerator execute(Connaissances connaissances);
}

public interface ActionGenetique
{
    void mutate(float iMutation);
}

public abstract class Action : ActionGame, ActionGenetique
{
    protected long duree;

    public static TankMovement tank = null;
    public static ScoutMovement scout = null;
    
    public Action(long p_duree)
    {
        if (tank == null || scout == null)
        {
            throw new NotImplementedException();
        }
        duree = p_duree;
    }

    internal static Action actionAleatoire(bool isTank)
    {
        float choix = Random.Range(0,2);
        if(choix < 1)
        {
            if (isTank)
                return BougerRandomTank.createRandom();
            else
                return BougerRandomScout.createRandom(); 
        }else if(choix < 2)
        {
            return MoveToTarget.createRandom();
        }
        throw new NotImplementedException();
    }

    public abstract IEnumerator execute(Connaissances connaissances);

    public abstract void mutate(float iMutation);

    public static float getRandomFrequence()
    {
        return Random.Range(0, 1);
    }
}
