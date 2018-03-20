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
            float speedTmp = m_Speed;
            m_Speed = 0;

            Quaternion q = transform.rotation;

            transform.LookAt(targetRigidbody.transform);

            m_Shooting.m_CurrentLaunchForce = 22f;
            m_Shooting.Fire(false);

            yield return new WaitForSeconds(0.8f);
            if (IsColored(targetRigidbody))
                connaissances.connaissances.Add(new Connaissances.Connaissance(targetRigidbody));

            transform.rotation = q;

            m_Speed = speedTmp;

            yield return null;
        }
    }

    public bool IsColored(Rigidbody rigidbody)
    {
        MeshRenderer[] renderers = rigidbody.GetComponentsInChildren<MeshRenderer>();

        if (!renderers[0].material.color.Equals(Color.white))
            return true;

        return false;       
    }
}