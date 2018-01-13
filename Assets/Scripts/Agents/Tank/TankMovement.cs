using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour, AgentMovement
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 6f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;
    public GameObject[] m_Targets;
    public GameObject m_TargetToKill;

    private Rigidbody m_Rigidbody;
    private float m_MovementInputValue;
    private float m_TurnInputValue;
    private float m_OriginalPitch;
    private TankShooting m_TankShooting;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_TankShooting = GetComponent<TankShooting>();
    }

    private void OnEnable()
    {
        // isKinematic : if force can be apply or not
        m_Rigidbody.isKinematic = false;

        // 1 pour avancer et -1 pour reculer
        m_MovementInputValue = 1f;
        m_TurnInputValue = 0f;
    }

    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_OriginalPitch = m_MovementAudio.pitch;

        StartCoroutine(KillThemAll());
    }

    // Running every frame
    private void Update()
    {
        EngineAudio();
    }

    public void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }

    private IEnumerator KillThemAll()
    {
        Debug.Log("Kill them all");

        for (int i = 0; i < m_Targets.Length; i++)
        {
            yield return StartCoroutine(FindTarget());
            yield return StartCoroutine(KillIt());
        }
    }

    private IEnumerator FindTarget()
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
    }

    private IEnumerator DetroyIt()
    {
        Debug.Log("Destoy it");

        // Force minimum
        float launchForce = 15f;

        // Tant qu'elle est en vie on tire
        while (m_TargetToKill.gameObject.activeSelf)
        {
            Debug.Log("Destoy it - shoot");

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
}