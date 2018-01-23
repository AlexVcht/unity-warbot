using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoutMovement : Movement
{

    public void Awake()
    {
        base.Awake();
        m_Speed = 10f;
        m_RaduisDetection = 15f;
        m_nameObject = "SCOUT";
    }

    public override IEnumerator DestroyIt(Rigidbody targetRigodbody)
    {
        Debug.Log("Scout movement - DestroyIt : not good");
        yield return null;
    }

    public override void PutInConnaissances(Rigidbody targetRigidbody)
    {
        if (!connaissances.ContainsCustom(targetRigidbody))
            connaissances.connaissances.Add(new Connaissances.Connaissance(targetRigidbody));
    }
}