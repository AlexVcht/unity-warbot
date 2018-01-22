using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankMovement : Movement
{
    private TankShooting m_TankShooting;

    private void Awake()
    {
        base.Awake();
        m_Speed = 5f;
        m_RaduisDetection = 15f;
        m_TankShooting = GetComponent<TankShooting>();
    }
  
    public override IEnumerator DestroyIt(Rigidbody targetRigodbody)
    {
        // Force minimum
        float launchForce = 15f;

        transform.LookAt(targetRigodbody.transform);

        // Tant qu'elle est en vie on tire
        while (targetRigodbody.gameObject.activeSelf)
        {
            launchForce = 15f;

            while (launchForce < 17f)
            {
                launchForce++;
                m_TankShooting.m_CurrentLaunchForce = launchForce;

                yield return null;
            }

            m_TankShooting.Fire();

            yield return new WaitForSeconds(1f);
        }
    }

    public override IEnumerator PutInConnaissances(Rigidbody targetRigidbody)
    {
        throw new System.NotImplementedException();
    }

    /*private IEnumerator FindTarget()
    {
        Debug.Log("find target");

        foreach (GameObject target in m_Targets)
        {
            if (target.gameObject.activeSelf)
                m_TargetToKill = target;
            yield return new WaitForSeconds(0);
        }

        yield return new WaitForSeconds(0f);
    }

    private IEnumerator KillIt()
    {
        Debug.Log("Kill it : " + m_TargetToKill);

        if (m_TargetToKill != null)
        {
            // Tant que la cible est en vie on va la détruire
            while (m_TargetToKill.gameObject.activeSelf)
            {
                // On se déplace jusqu'a elle
                yield return StartCoroutine(Move());
                yield return StartCoroutine(DetroyIt());

                yield return null;
            }
        }
    }
*/
}