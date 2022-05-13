using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManagerment : MonoBehaviour
{
    public GameObject btnRestart;
    public GameObject CanVas;
    static public UiManagerment instance;
    private void Awake()
    {
        instance = this;
        this.btnRestart = GameObject.Find("Restart");
        this.btnRestart.SetActive(false);
    }
}
