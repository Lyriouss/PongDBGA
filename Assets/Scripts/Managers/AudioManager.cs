using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        //Creates a singleton instance of this class
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void PlaySFX(AudioClip clip)
    {
        //Gets an inactive object from Audio object pool
        AudioSource source = AudioObjectPooler.instance.GetPooledObject().GetComponent<AudioSource>();
        source.clip = clip;                     //Sets the audio clip to the pooled object
        source.gameObject.SetActive(true);      //Activates the object
        source.Play();                          //Then plays the audio clip

        StartCoroutine(DeactivateAfterPlaying(source.gameObject, clip.length));
    }

    private IEnumerator DeactivateAfterPlaying(GameObject obj, float delay)
    {
        //Waits the duration of audio clip before deactivating the object
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
    }
}
