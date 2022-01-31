using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour
{
    [Header("Mvt Setup")]
    [Tooltip("unit °")]
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_RotationSpeed;
    Rigidbody m_Rigidbody;
    //[SerializeField] GameObject m_BallPrefab;
    //[SerializeField] GameObject m_BallSpawnPos;
    //[SerializeField] float m_BallInitSpeed;
    //[SerializeField] float m_BallLifeDuration;
    //[SerializeField] float m_ShootingPeriod;
    //float m_TimeNextShoot;

    // Start is called before the first frame update
    /**void ShootBall()
    {
        GameObject newBallGo = Instantiate(m_BallPrefab);
        newBallGo.transform.position = m_BallSpawnPos.transform.position;
        newBallGo.GetComponent<Rigidbody>().velocity =
            m_BallSpawnPos.transform.forward * m_BallInitSpeed;
        Destroy(newBallGo, m_BallLifeDuration);
    }
    **/
    void Start()
    {
       // m_TimeNextShoot = Time.time;
    }
    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input Axis : Classique
        float vInput = Input.GetAxis("Vertical");   //-1 et 1

        float hInput = Input.GetAxis("Horizontal");



        Vector3 moveVect = m_TranslationSpeed * Time.deltaTime * transform.forward * vInput;

        transform.Translate(moveVect, Space.World);

        //Rotation
        float deltaAngle = m_RotationSpeed * Time.deltaTime * hInput;
        //transform.Rotate(transform.up, deltaAngle, Space.World);
        Quaternion qRot = Quaternion.AngleAxis(deltaAngle, transform.up);
        transform.rotation = qRot * transform.rotation;

    }
    private void FixedUpdate()
    {//Dynamique

        float vInput = Input.GetAxis("Vertical"); // entre -1 et 1
        float hInput = Input.GetAxisRaw("Horizontal"); // entre -1 et 1

        // MODE POSITIONNEL
        //"pseudo dynamique" (on se déplace par téléportation, exactement comme en cinématique) ... plus proche d'un comportement cinématique
        // va nous permettre tout de même de prednre en compte les collisions
        // MovePosition et MoveRotation

        //Debug.Log("BEFORE " + transform.position * 1000);
        //Vector3 worldMoveVect = vInput * m_TranslationSpeed * Time.fixedDeltaTime* Vector3.ProjectOnPlane(transform.forward,Vector3.up).normalized;
        //m_Rigidbody.MovePosition(transform.position + worldMoveVect);

        //Quaternion qRotUpright = Quaternion.FromToRotation(transform.up, Vector3.up);
        //Quaternion qOrientSlightlyUpright = Quaternion.Slerp(transform.rotation, qRotUpright * transform.rotation, Time.fixedDeltaTime * 4);

        //float deltaAngle = hInput * m_RotationSpeed * Time.fixedDeltaTime;
        //Quaternion qRot = Quaternion.AngleAxis(deltaAngle, transform.up);
        //m_Rigidbody.MoveRotation(qRot * qOrientSlightlyUpright);

        //m_Rigidbody.velocity = Vector3.zero;
        //m_Rigidbody.angularVelocity = Vector3.zero;

        //Debug.Log("AFTER " + transform.position * 1000);

        // MODE VELOCITY
        Vector3 targetVelocity = vInput * m_TranslationSpeed * Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Vector3 velocityChange = targetVelocity - m_Rigidbody.velocity;
        m_Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        Vector3 targetAngularVelocity = hInput * m_RotationSpeed * transform.up;
        Vector3 angularVelocityChange = targetAngularVelocity - m_Rigidbody.angularVelocity;
        m_Rigidbody.AddTorque(angularVelocityChange, ForceMode.VelocityChange);

        Quaternion qRotUpright = Quaternion.FromToRotation(transform.up, Vector3.up);
        Quaternion qOrientSlightlyUpright = Quaternion.Slerp(transform.rotation, qRotUpright * transform.rotation, Time.fixedDeltaTime * 4);
        m_Rigidbody.MoveRotation(qOrientSlightlyUpright);

        // MODE ACCELERATION
        /*        Vector3 acceleration = vInput * Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * 1;
                m_Rigidbody.AddForce(acceleration, ForceMode.Acceleration);
                Vector3 angularAcceleration = hInput * transform.up * 10;
                m_Rigidbody.AddTorque(angularAcceleration, ForceMode.Acceleration);

                Quaternion qRotUpright = Quaternion.FromToRotation(transform.up, Vector3.up);
                Quaternion qOrientSlightlyUpright = Quaternion.Slerp(transform.rotation, qRotUpright * transform.rotation, Time.fixedDeltaTime * 4);
                m_Rigidbody.MoveRotation(qOrientSlightlyUpright);*/

        // MODE FORCE PURE
        /*
        Vector3 force = vInput * Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * 20;
        m_Rigidbody.AddForce(force, ForceMode.Force);
        Vector3 torque = hInput * transform.up * 20;
        m_Rigidbody.AddTorque(torque, ForceMode.Force);
        Quaternion qRotUpright = Quaternion.FromToRotation(transform.up, Vector3.up);
        Quaternion qOrientSlightlyUpright = Quaternion.Slerp(transform.rotation, qRotUpright * transform.rotation, Time.fixedDeltaTime * 4);
        m_Rigidbody.MoveRotation(qOrientSlightlyUpright);
        */

        /**
        bool isFiring = Input.GetButton("Fire1");
        if (isFiring && Time.time>m_TimeNextShoot){
            ShootBall();
            m_TimeNextShoot= Time.time + m_ShootingPeriod;
        }
        **/
        /**
        float vInput = Input.GetAxis("Vertical");   //-1 et 1

        float hInput = Input.GetAxis("Horizontal");



        Vector3 moveVect = m_TranslationSpeed * Time.fixedDeltaTime * transform.forward * vInput;

        transform.Translate(moveVect, Space.World);
        

        float deltaAngle = m_RotationSpeed * Time.fixedDeltaTime * hInput;
        Quaternion qRot = Quaternion.AngleAxis(deltaAngle, transform.up);
        m_Rigidbody.MovePosition(transform.position + moveVect);
        m_Rigidbody.MoveRotation(qRot * transform.rotation);
    }
   **/
    }
}
