using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour
{
    public AudioClip[] GameAudio;
    // Update is called once per frame
    void Update()
    {
        if (!Overall.Mute)
        {
            GetComponent<AudioSource>().mute = false;
            GetComponent<AudioSource>().clip = GameAudio[0];
            GetComponent<AudioSource>().loop = true;
        }
        else if (Overall.Mute)
        {
            GetComponent<AudioSource>().mute = true;
        }

        //if (InputModel.Fire)
        {
        //    GetComponent<AudioSource>().clip = GameAudio[1];
        //    GetComponent<AudioSource>().loop = false;
        }

        }

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
}
