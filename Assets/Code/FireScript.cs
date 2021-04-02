using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    private Button _button;
    private GameObject _beam;
    private Rigidbody _beamRB;
    public float force;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Fire);
    }

    void Fire()
    {
        _beam = GameObject.Find("Beam");
        _beamRB = _beam.GetComponent<Rigidbody>();
        _beamRB.AddForce(_beamRB.transform.up * force, ForceMode.Impulse);
    }
}
