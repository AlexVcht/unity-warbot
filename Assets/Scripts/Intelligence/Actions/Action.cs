using System;
using System.Collections;
using Random = UnityEngine.Random;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public interface ActionGame
{
    IEnumerator execute(Connaissances connaissances);
}

public interface ActionGenetique
{
    void mutate(float iMutation);
}

[System.Serializable]
public abstract class Action : ActionGame, ActionGenetique
{
    protected float duree;

    public static TankMovement tank = null;
    public static ScoutMovement scout = null;
    
    public Action(float p_duree)
    {
        if (tank == null || scout == null)
        {
            throw new NotImplementedException();
        }
        duree = p_duree;
    }

    internal static Action actionAleatoire(bool isTank)
    {
        float choix = Random.Range(0, 2);
        if (choix == 0)
        {
            if (isTank)
                return BougerRandomTank.createRandom();
            else
                return BougerRandomScout.createRandom();
        }
        else if (choix == 1)
        {
            if (isTank)
                return MoveToTarget.createRandom();
            else
                return BougerRandomScout.createRandom();
        }
        throw new NotImplementedException();
    }

    public abstract IEnumerator execute(Connaissances connaissances);

    public abstract void mutate(float iMutation);

    public static float getRandomFrequence()
    {
        return Random.Range(0f, 1f);
    }
}
