using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class StandardManager : MonoBehaviour {
    private TimeManager timeManager = new TimeManager();

    [SerializeField]
    UnityEngine.UI.Text label;

    [SerializeField]
    float playTime = 2.5f;

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
        if (timeManager.Time > playTime)
        {
            sign = -1.0f;

            label.text = "Reverse Time";
            label.color = Color.red;
        }
        if (timeManager.Time < 0)
        {
            sign = 1.0f;

            label.text = "Forward Time";
            label.color = Color.green;
        }
    }
}
