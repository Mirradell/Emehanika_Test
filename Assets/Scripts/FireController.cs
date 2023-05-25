using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public Transform player;
    public TorchController torch;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(player.tag) && torch != null)
        {
            torch.IncreaseTorch();
            // огонек исчезает, когда его "собрали"
            //Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(player.tag) && torch != null)
        {
            // огонек исчезает, когда его "собрали"
            Destroy(gameObject);
        }
    }
}
