﻿using System.Collections;
using UnityEngine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public abstract class BougerRandom : Action
{
    protected SerializableQuaternion direction;

    public BougerRandom(float p_duree, Quaternion p_direction) : base(p_duree) {
        direction = p_direction;
    }

    public abstract override IEnumerator execute(Connaissances connaissances);

    public override void mutate(float iMutation)
    {
        float r = Random.Range(0, 1);
        if(r < iMutation)
        {
            direction = getRandomDirection();
        }
        r = Random.Range(0, 1);
        if (r < iMutation)
        {
            duree = getRandomDuree();
        }
    }

    public Quaternion getDirection()
    {
        return direction;
    }

    public void setDirection(Quaternion p_direction)
    {
        direction = p_direction;
    }

    public static float getRandomDuree()
    {
        return Random.Range(0.5f, 3f);
    }

    public static Quaternion getRandomDirection()
    {
        return Quaternion.Euler(0, Random.Range(0, 360), 0);
    }

    public override string ToString()
    {
        return "BougerRandom("+duree+";"+direction.y+")";
    }
}