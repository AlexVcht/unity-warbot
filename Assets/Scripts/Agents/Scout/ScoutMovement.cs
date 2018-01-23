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
        throw new System.NotImplementedException();
    }

    public override IEnumerator PutInConnaissances(Rigidbody targetRigidbody)
    {
        // Tant qu'elle est en vie on tire
        if (!connaissances.ContainsCustom(targetRigidbody))
        {
            Vector3 t = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            transform.LookAt(targetRigidbody.transform);

            m_Shooting.m_CurrentLaunchForce = 17f;
            m_Shooting.Fire(false);

            transform.LookAt(t);

            connaissances.connaissances.Add(new Connaissances.Connaissance(targetRigidbody));

            Debug.Log("### after add : " + connaissances.connaissances.Count);


            yield return new WaitForSeconds(1f);
        }
    }
}