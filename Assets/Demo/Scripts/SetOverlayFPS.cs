using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class SetOverlayFPS : MonoBehaviour {

    Texture tex;
    private float fps = 60;
    public Text text1;
    private int count = 60;
    Transform CenterPointer;
    Camera LCamera;
    Camera RCamera;
    Camera UICamera;
    int texID = 0;
    IntPtr texRendPtr = IntPtr.Zero;
    void Start()
    {
        tex = Resources.Load("star") as Texture;
        LCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Left").GetComponent<Camera>();
        RCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Right").GetComponent<Camera>();
        CenterPointer = GameObject.Find("MojingMain/MojingVrHead/GazePointer").transform;
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();

        RenderTexture UIScreen = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Default);
        UIScreen.anisoLevel = 0;

        UICamera.targetTexture = UIScreen;
        texID = (int)UICamera.targetTexture.GetNativeTexturePtr();
        texRendPtr = UICamera.targetTexture.colorBuffer.GetNativeRenderBufferPtr();
    }

    void Update()
    {
        count++;
        float interp = Time.deltaTime / (0.5f + Time.deltaTime);
        float currentFPS = 1.0f / Time.deltaTime;
        if (count >= 60)
        {
            fps = Mathf.Lerp(fps, currentFPS, interp);
            text1.text = "FPS:" + Mathf.RoundToInt(fps).ToString();
            count = 0;
        }
        if (Mojing.SDK.NeedDistortion)
        {
            if (SystemInfo.graphicsDeviceType == UnityEngine.Rendering.GraphicsDeviceType.Metal)
            {
                MojingSDK.Unity_SetOverlay3D_Metal(3, texRendPtr, 1, 1, CenterPointer.position.magnitude);
            }
            else
            {
                MojingSDK.Unity_SetOverlay3D(3, texID, 1, 1, CenterPointer.position.magnitude);
            }
        }
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 45;
        if (!Mojing.SDK.NeedDistortion && Mojing.SDK.VRModeEnabled)
        {
            GUI.DrawTexture(new Rect(LCamera.WorldToScreenPoint(CenterPointer.position).x - 25, LCamera.WorldToScreenPoint(CenterPointer.position).y - 25, 50, 50), tex);
            GUI.DrawTexture(new Rect(RCamera.WorldToScreenPoint(CenterPointer.position).x - 25, RCamera.WorldToScreenPoint(CenterPointer.position).y - 25, 50, 50), tex);
            GUI.Label(new Rect(LCamera.WorldToScreenPoint(CenterPointer.position).x - 300, LCamera.WorldToScreenPoint(CenterPointer.position).y - 300, 300, 50), text1.text);
            GUI.Label(new Rect(RCamera.WorldToScreenPoint(CenterPointer.position).x - 300, RCamera.WorldToScreenPoint(CenterPointer.position).y - 300, 300, 50), text1.text);
        }
        else if (!Mojing.SDK.VRModeEnabled)
        {
            GUI.DrawTexture(new Rect(0.5f * Screen.width - 25, 0.5f * Screen.height - 25, 50, 50), tex);
            GUI.Label(new Rect(0.5f * Screen.width - 300, 0.5f * Screen.height - 300, 300, 50), text1.text);
        }
    }

    void OnDestroy()
    {
        MojingSDK.Unity_SetOverlay3D(3, 0, 1, 1, 1);
    }
}
