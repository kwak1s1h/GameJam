using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DEFINE
{
    private static Camera _mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if(_mainCam == null)
                _mainCam = Camera.main;
            return _mainCam;
        }
    }

    private static CinemachineVirtualCamera _cmVCam = null;
    public static CinemachineVirtualCamera CmVCam
    {
        get 
        {
            if(_cmVCam == null)
                _cmVCam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            return _cmVCam;
        }
    }
}
