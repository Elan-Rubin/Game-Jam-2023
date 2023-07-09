using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    #region singleton
    private static CameraManager _instance;

    public static CameraManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    #endregion
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShakeCamera()
    {
        transform.DOShakePosition(0.25f);
    }
    public void BounceCamera()
    {
        GetComponent<Camera>().DOOrthoSize(4.8f, 0.1f).OnComplete(() => GetComponent<Camera>().DOOrthoSize(5, 0.1f));
    }
}
