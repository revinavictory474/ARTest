using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strike : MonoBehaviour
{
    private ProgramManager _programmManager;
    private bool killed = false;

    private void Start()
    {
        _programmManager = FindObjectOfType<ProgramManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!killed && collision.gameObject.name == "Shell(Clone)")
        {
            _programmManager.strikes += 1;
            killed = true;
        }
    }
}
