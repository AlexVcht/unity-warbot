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

    public IEnumerator Move()
    {
        Debug.Log("Move");

        m_Rigidbody.transform.LookAt(m_TargetToKill.transform);

        Vector3 distanceVector3 = m_Rigidbody.transform.position - m_TargetToKill.transform.position;
        Vector3 movement = new Vector3();

        // On le met a la bonne distance
        while (distanceVector3.magnitude > 15f)
        {
            // On vérifie toujours si elle est en vie sinon ca sert a rien d'aller vers elle
            if (!m_TargetToKill.gameObject.activeSelf)
                StopCoroutine(Move());

            movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

            distanceVector3 = m_Rigidbody.transform.position - m_TargetToKill.transform.position;

            yield return null;
        }
    }*/
}