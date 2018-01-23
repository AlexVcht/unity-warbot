using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Ball;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public TankManager m_TankInstance;
    [HideInInspector] public float m_CurrentLaunchForce;               

    public void Fire(bool isShellOrNot)
    {
        // Instantiate and launch the shell.
        Rigidbody ballInstance = Instantiate(m_Ball, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        ballInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

        ShellExplosion shellExplosion = ballInstance.GetComponent<ShellExplosion>();
        shellExplosion.isShell = isShellOrNot;

        shellExplosion.m_TankInstance = m_TankInstance;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        m_CurrentLaunchForce = 0f;
    }
}