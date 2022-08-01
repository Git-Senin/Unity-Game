using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBox : MonoBehaviour
{
    public AudioClip clip;
    public float volume = 1.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volume);
            GetComponent<TheBox>().enabled = false;
            Destroy(gameObject);
        }

    }
}
