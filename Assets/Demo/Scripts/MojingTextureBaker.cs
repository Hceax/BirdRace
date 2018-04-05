using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class MojingTextureBaker : MonoBehaviour
{

    bool startmake = false;
    int tid = 0;
    int sid = 0;
    int mcount = 0;
    MojingSDK.backerInfo[] info = new MojingSDK.backerInfo[2];
    Texture2D[] a = new Texture2D[9];

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct Info
    {
        public int OrderNO;
        public float CpuPercent;

    };
    void Start()
    {    
        a[0] = Resources.Load("star") as Texture2D;
        a[1] = Resources.Load("star") as Texture2D;
        tid = (int)a[0].GetNativeTexturePtr();
        sid = (int)a[1].GetNativeTexturePtr();
        for (int i = 0; i < info.Length; ++i)
        {
            info[i] = new MojingSDK.backerInfo();
        }
        info[0].x = 0;
        info[0].y = 0;
        info[0].texid = (uint)tid;
        info[0].height = 128;
        info[0].width = 128;
        info[0].desX = 0;
        info[0].desY = 0;

        info[1].x = 0;
        info[1].y = 0;
        info[1].texid = (uint)sid;
        info[1].height = 128;
        info[1].width = 128;
        info[1].desX = 128;
        info[1].desY = 128;
    }

void Update()
    {
        if (Time.frameCount == 5)
        {
            MojingSDK.Unity_StartBacker(1024, 1024, info, info.Length);
            if (false == startmake)
            {
                startmake = true;
            }
        }
        if (Time.frameCount > 10)
        {
            bool bret = MojingSDK.Unity_GetBackerTexID(ref tid);
            if (bret)
            {
                // Debug.Log("Unity_GetBackerTexID    " + tid);
                a[2] = Texture2D.CreateExternalTexture(1024, 1024, TextureFormat.RGB24, false, true, new IntPtr(tid));
                a[2].filterMode = FilterMode.Trilinear;
                GetComponent<Renderer>().material.mainTexture = a[2];
            }

        }
        mcount++;
    }
}
