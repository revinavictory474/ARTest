using System;
using UnityEngine;
using UnityEngine.UI;

public class ThrowShell : MonoBehaviour
{
    private ProgramManager _programManager;
    private TrajectoryRenderer _trajRen;

    [SerializeField] private GameObject _shell;
    private GameObject _shellObj;
    private Rigidbody _shellRB;
    private Vector3 _speed;

    private GameObject fieldObj;
    private InputField field;
    private string forceString;
    private float force;
    private Rigidbody colisionRB;

    public AudioClip audioClip;
    private AudioSource audioSource;

    private void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();
        _trajRen = FindObjectOfType<TrajectoryRenderer>();
        fieldObj = GameObject.Find("InputField");
        field = fieldObj.GetComponent<InputField>();
    }
    private void Update()
    {
        forceString = field.text;
        force = float.Parse(forceString);
        _speed = transform.forward * 2f  + transform.up * force;
        _trajRen.ShowTrajectory(transform.position + new Vector3(0, 0.25f, 0), _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _shellObj = Instantiate(_shell, transform.position + new Vector3(0, 0.25f, -0.05f), _shell.transform.rotation);
        _shellRB = _shellObj.GetComponent<Rigidbody>();
        _shellRB.AddForce(_speed, ForceMode.Impulse);

        colisionRB = collision.rigidbody;
        colisionRB.AddForce(colisionRB.transform.up * (-1), ForceMode.Impulse);

        _programManager.recharging = true;

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip, 1.0f);
    }

}
