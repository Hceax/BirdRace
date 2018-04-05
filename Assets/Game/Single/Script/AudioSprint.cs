using UnityEngine;

public class AudioSprint : MonoBehaviour
{
    public static AudioSource audioX;

    // Use this for initialization
    void Start()
    {

        audioX = GetComponent<AudioSource>();
        audioX.clip = Microphone.Start("Bulit-in Microphone", true, 1000, 44100);
        //audioX.clip.
        audioX.pitch = 0.995f;
        //audioX.Play();
        //InvokeRepeating("A", 1, 0.1f);
    }

    // Update is called once per frame
    public static float A()
    {
        if (Microphone.IsRecording("Bulit-in Microphone"))
        {
            // 采样数
            int sampleSize = 512;
            float[] samples = new float[sampleSize];
            int startPosition = Microphone.GetPosition("Bulit-in Microphone") - (sampleSize + 1);
            // 得到数据
            audioX.clip.GetData(samples, startPosition);

            // Getting a peak on the last 128 samples
            float levelMax = 0;
            for (int i = 0; i < sampleSize; ++i)
            {
                float wavePeak = samples[i];
                if (levelMax < wavePeak)
                    levelMax = wavePeak;
            }
            return (levelMax * 99);
        }
        return 0;
    }
}
