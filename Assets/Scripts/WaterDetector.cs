using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDetector : MonoBehaviour
{
    public Transform player;
    public TorchController torch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag) && torch != null)
            torch.DecreaseTorch();
    }
}
