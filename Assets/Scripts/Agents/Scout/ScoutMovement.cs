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

    /*private IEnumerator Move()
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