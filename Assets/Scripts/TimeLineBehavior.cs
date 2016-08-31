using UnityEngine;

using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif


[ExecuteInEditMode]
public class TimeLineBehavior : MonoBehaviour {
    
    public float nowTime;

    [SerializeField]
    bool setZero = false;
    
    [SerializeField]
    bool setNowAsZero = false;
    
    TimeManager timeManager = new TimeManager();

    #if UNITY_EDITOR
    // Use this for initialization
	void Start () {
        EditorApplication.playmodeStateChanged += HandleOnPlayModeChanged;	
	}

    void OnDestroy(){
        EditorApplication.playmodeStateChanged -= HandleOnPlayModeChanged;  
    }

    void UpdateTime()
    {
        var allBehs = GetComponentsInChildren<MonoBehaviour>();
        var allBehsList = new List<MonoBehaviour>(allBehs);
        var timeDependant = allBehsList.OfType<ITimeChanging>();

        timeManager.TimeDependants = timeDependant;

        timeManager.Time = nowTime;
        nowTime = timeManager.Time;
    }

    void CheckSetNowAsZero()
    {
        if (setNowAsZero)
        {
            timeManager.SetTimeBruteForce(0f);
            nowTime = 0f;
            setNowAsZero = false;
        }
    }

    void HandleOnPlayModeChanged()
    {
        // This method is run whenever the playmode state is changed.

        if (EditorApplication.isPlaying) { return; }

        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            nowTime = 0;
            UpdateTime();
        }
    }

    void CheckSetZero()
    {
        if (setZero)
        {
            setZero = false;
            nowTime = 0;
        }
    }
	
	// Update is called once per frame
	public void Update () {
        if (EditorApplication.isPlaying) return;
    
        CheckSetZero();
        UpdateTime();
        CheckSetNowAsZero();
	}
    #endif
}


