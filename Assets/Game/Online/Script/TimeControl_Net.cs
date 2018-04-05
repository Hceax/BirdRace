using UnityEngine;

public class TimeControl_Net : MonoBehaviour {

    //public float GameTime = 307.0f;
    void Start()
    {
        //ScorinSystem_Net.Time = this.GameTime;
    }

    // Update is called once per frame
    void Update () {
        if (!InputModel.Win && ScorinSystem_Net.Time > 0.0f)
        {
            ScorinSystem_Net.Time -= Time.deltaTime;
        }
        else if (ScorinSystem_Net.Time <= 0.0f)
        {
            ScorinSystem_Net.Time = 0.0f;
            InputModel.TimeOut = true;
        }
    }
}