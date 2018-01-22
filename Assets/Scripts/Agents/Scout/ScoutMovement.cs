using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoutMovement : Movement
{

    public void Awake()
    {
        base.Awake();
        m_Speed = 10f;
    }

    public override IEnumerator DestroyIt(Rigidbody targetRigodbody)
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator PutInConnaissances(Rigidbody targetRigidbody)
    {
        Connaissances.Connaissance connaissance = new Connaissances.Connaissance(targetRigidbody);
        connaissances.connaissances.Add(connaissance);

        Debug.Log(connaissances);

        yield return null;
    }
}