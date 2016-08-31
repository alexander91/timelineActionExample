using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StandardManager : MonoBehaviour {
    private TimeManager timeManager = new TimeManager();

    [SerializeField]
    float playTime = 3.0f;

    float sign = 1.0f;

    public TimeManager TimeManager
    {
        get
        {
            return timeManager;
        }
    }

    // Use this for initialization
    void Start () {


        var allBehs = GetComponentsInChildren<MonoBehaviour>();
        var allBehsList = new List<MonoBehaviour>(allBehs);
        var timeDependant = allBehsList.OfType<ITimeChanging>();

        timeManager.TimeDependants = timeDependant;

    }
	
	// Update is called once per frame
	void Update () {
        timeManager.Time += sign * Time.deltaTime;
        if (timeManager.Time > playTime) sign = -1.0f;
        if (timeManager.Time < 0) sign = 1.0f;
    }
}
