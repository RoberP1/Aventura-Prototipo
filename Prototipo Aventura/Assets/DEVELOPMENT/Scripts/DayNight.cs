using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    private TicksManager ticksManager;
    public int dayTicks;
    void Start()
    {
        ticksManager = FindObjectOfType<TicksManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler((ticksManager.tick/(dayTicks/180)), 0, 0);
        if (transform.rotation.eulerAngles.x == -180)
        {
            print(ticksManager.tick);
        }
    }
}
