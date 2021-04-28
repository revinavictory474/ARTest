using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    private ProgramManager _programManager;

    private void Start()
    {
        _programManager = FindObjectOfType<ProgramManager>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        _programManager.recharging = false;
    }
}
