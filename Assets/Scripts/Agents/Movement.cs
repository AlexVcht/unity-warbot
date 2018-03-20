using System.Collections;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 0f;
    public float m_RaduisDetection = 0f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;
    public LayerMask m_LayerMask;
    public string m_nameObject;
    public Shooting m_Shooting;

    protected Rigidbody m_Rigidbody;
    protected float m_MovementInputValue;
    protected float m_OriginalPitch;
    protected ActionGame[] ADN;
    protected Connaissances connaissances;

    private bool disabled = true;

    public void setADN(ActionGame[] adn)
    {
        ADN = adn;
    }

    public void setConnaissances(Connaissances co)
    {
        connaissances = co;
    }

    protected void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Shooting = GetComponent<Shooting>();
    }

    protected void OnEnable()
    {
        m_Rigidbody.isKinematic = false;

        m_MovementInputValue = 1f;

        disabled = false;

        StartCoroutine(LectureADN());
        StartCoroutine(CheckAround());
    }

    protected void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
        disabled = true;
    }

    protected void Start()
    {
        m_OriginalPitch = m_MovementAudio.pitch;
    }

    // Running every frame
    protected void Update()
    {
        EngineAudio();
    }

    public void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if (Mathf.Abs(m_MovementInputValue) < 0.1f)
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

    public IEnumerator CheckAround()
    {
        while (!disabled)
        {
            Rigidbody rigidbodyTmp = DetectTargetsAround();

            if (rigidbodyTmp != null)
            {
                if (m_nameObject.Equals("SCOUT"))
                    yield return StartCoroutine(PutInConnaissances(rigidbodyTmp));
                else if (m_nameObject.Equals("TANK"))
                    yield return StartCoroutine(DestroyIt(rigidbodyTmp));
            }

            yield return new WaitForSeconds(0.3f);
        }
    }

    public IEnumerator LectureADN()
    {
        if (ADN == null)
            yield return null;
        
        while (!disabled)
        {
            foreach (ActionGame actionGame in ADN)
            {
                if (disabled) break;

                yield return StartCoroutine(actionGame.execute(connaissances));
            }
        }
    }

    public IEnumerator BougerRandom(float duree, Quaternion direction)
    {
        float startTime = Time.time;
        Vector3 movement = new Vector3();

        m_Rigidbody.rotation = direction;

        // On le met a la bonne distance
        while (Time.time <= startTime + duree)
        {
            movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

            yield return null;
        }
    }

    public IEnumerator moveToTarget(Rigidbody toGo)
    {
        m_Rigidbody.transform.LookAt(toGo.transform);

        Vector3 movement = new Vector3();

        // On le met a la bonne distance
        while ((m_Rigidbody.transform.position - toGo.position).magnitude > 10f && connaissances.ContainsCustom(toGo))
        {
            movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
            yield return null;
        }
    }

    public abstract IEnumerator PutInConnaissances(Rigidbody targetRigidbody);
    public abstract IEnumerator DestroyIt(Rigidbody targetRigodbody);

    public Rigidbody DetectTargetsAround()
    {
        Collider[] objectAround = Physics.OverlapSphere(transform.position, m_RaduisDetection, m_LayerMask);

        for (int i = 0; i < objectAround.Length; i++)
        {
            Rigidbody targetRigidbody = objectAround[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            CapsuleCollider capsuleCollider = targetRigidbody.GetComponent<CapsuleCollider>();

            if (capsuleCollider != null)
                return targetRigidbody;
        }

        return null;
    }
}