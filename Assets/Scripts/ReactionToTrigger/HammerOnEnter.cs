using UnityEngine;

public class HammerOnEnter : MonoBehaviour
{
    public AudioSource hammerAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hammerAudio.Play();
        }
    }
}
