using System;
using UnityEngine;
using Random = UnityEngine.Random;

public interface ActionGame
{
    void execute(GameObject obj, Connaissances connaissances);
}

public interface ActionGenetique
{
    void mutate();
}

public abstract class Action : ActionGame, ActionGenetique
{
    protected long duree;

    public Action(long p_duree)
    {
        this.duree = p_duree;
    }

    internal static Action actionAleatoire()
    {
        int nbActionsPossible = 1;
        float choix = Random.Range(0,nbActionsPossible);
        if(choix < 1)
        {
            return new BougerRandom((int)(Random.Range(0, 3) * 1000), new UnityEngine.Quaternion());
        }
        throw new NotImplementedException();
    }

    public abstract void execute(GameObject obj, Connaissances connaissances);
    public abstract void mutate();

    public long getDuree()
    {
        return duree;
    }
    
    public void setDuree(long p_duree)
    {
        duree = p_duree;
    }
}
