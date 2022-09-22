using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlockType
{
    Grabbable,
    Ungrabbable
}
public class Block : MonoBehaviour
{
    [SerializeField] private BlockType thisBlockType;

    [SerializeField] private bool isGrabbed;

    public bool GetIsGrabbed()
    {
        return isGrabbed;
    }
    public void SetIsGrabbed(bool _isGrabbed)
    {
        isGrabbed = _isGrabbed;
    }
}
