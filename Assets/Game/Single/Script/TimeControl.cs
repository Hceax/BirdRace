using UnityEngine;

public class TimeControl : MonoBehaviour {

    //public float GameTime = 307.0f;
    void Start()
    {
        //ScorinSystem.Time = this.GameTime;
    }

    // Update is called once per frame
    void Update () {
        if (!InputModel.paused && !InputModel.Win && ScorinSystem.Time > 0.0f)
        {
            ScorinSystem.Time -= Time.deltaTime;
        }
        else if (ScorinSystem.Time <= 0.0f)
        {
            ScorinSystem.Time = 0.0f;
            InputModel.TimeOut = true;
        }
    }
}