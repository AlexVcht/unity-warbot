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
        int nbActionsPossible = 1;
        float choix = Random.Range(0,nbActionsPossible);
        if(choix < 1)
        {
            if (isTank)
                return BougerRandomTank.createRandom();
            else
                return BougerRandomScout.createRandom(); 
        }
        throw new NotImplementedException();
    }

    public abstract IEnumerator execute(Connaissances connaissances);

    public abstract void mutate(float iMutation);

    public long getDuree()
    {
        return duree;
    }
}
