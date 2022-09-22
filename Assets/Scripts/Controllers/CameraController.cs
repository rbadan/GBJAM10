using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform Ant1, Ant2;
    public CinemachineVirtualCamera thisCam;

    private void Update()
    {
        switch (GameController.GC.GetCurrentAnt())
        {
            case Ant.Ant1:
                thisCam.Follow = Ant1;
                break;
            case Ant.Ant2:
                thisCam.Follow = Ant2;
                break;
            default:
                break;
        }
    }
}
