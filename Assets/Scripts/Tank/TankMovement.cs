using UnityEngine;
using UnityEngine.UI;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public float m_TurnSpeed = 180f;
    public AudioSource m_MovementAudio;
    public AudioClip m_EngineIdling;
    public AudioClip m_EngineDriving;
    public float m_PitchRange = 0.2f;
    public GameObject[] m_Targets;


    private string m_MovementAxisName;
    private string m_TurnAxisName;
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
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }

    // Running every frame
    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        KillThemAll();

        EngineAudio();
    }

    private void EngineAudio()
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

    private void FixedUpdate()
    {
        // Move and turn the tank. 
        /*Move();
        Turn();*/
    }

    private void KillThemAll()
    {
        // Pour chaque cible dans l'ordre, a changer !
        foreach (GameObject target in m_Targets)
        {
            Move(target);
            DetroyIt(target);
        }
    }

    private void Move(GameObject target)
    {
        m_Rigidbody.transform.LookAt(target.transform);

        Vector3 distanceVector3 = m_Rigidbody.transform.position - target.transform.position;
        Vector3 movement = new Vector3();

        // On le met a la bonne distance
        while (distanceVector3.magnitude > 50)
        {
            movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

            m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

            distanceVector3 = m_Rigidbody.transform.position - target.transform.position;
        }
    }

    private void DetroyIt(GameObject target)
    {
        // On shoot
        float launchForce = 15;

        while (target.gameObject.activeSelf)
        {
            launchForce = 15;

            while (launchForce < 22)
                m_TankShooting.m_CurrentLaunchForce = launchForce;

            m_TankShooting.Fire();
        }
    }

    /*private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply this rotation to the rigidbody's rotation.
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }*/
}