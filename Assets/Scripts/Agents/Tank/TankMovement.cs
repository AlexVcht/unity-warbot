using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankMovement : Movement
{
    private void Awake()
    {
        base.Awake();
        m_Speed = 5f;
        m_RaduisDetection = 15f;
        m_nameObject = "TANK";
    }

    public override IEnumerator DestroyIt(Rigidbody targetRigodbody)
    {
        transform.LookAt(targetRigodbody.transform);

        // Tant qu'elle est en vie on tire
        while (targetRigodbody.gameObject.activeSelf)
        {
            m_Shooting.m_CurrentLaunchForce = 17f;

            m_Shooting.Fire(true);

            yield return new WaitForSeconds(1f);
        }

        if (!targetRigodbody.gameObject.activeSelf && connaissances.ContainsCustom(targetRigodbody))
            connaissances.RemoveCustom(targetRigodbody);
    }

    public override IEnumerator PutInConnaissances(Rigidbody targetRigidbody)
    {
        Debug.Log("Tank movement - PutInConnaissance : not good");
        throw new System.NotImplementedException();
    }
}