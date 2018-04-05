//------------------------------------------------------------------------------
// Copyright 2016 Baofeng Mojing Inc. All rights reserved.
//------------------------------------------------------------------------------

using UnityEngine;

public class getParameter : MonoBehaviour {

    public delegate void Delegate_OnGetParameterName(string strName);

    public static event Delegate_OnGetParameterName EvnOnGetParameter = null;

    public void getName()
    {
        if (EvnOnGetParameter != null)
            EvnOnGetParameter(name);
    }
}
