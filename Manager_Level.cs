using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Level : MonoBehaviour
{
    public List<GameObject> Okots;
    private int OkotsRescued = 0;

    private void Start()
    {
        Manager_Game.Instance.ref_Level = this;
    }
    private void OnDestroy()
    {
        if (Manager_Game.Instance)
        {
            if (Manager_Game.Instance.ref_Level == this)
                Manager_Game.Instance.ref_Level = null;
        }

    }


    public void Okot_Rescue()
    {
        print("Okot rescued!!!!");
        OkotsRescued++;
        if (OkotsRescued == (Okots.Count - 1)) Manager_Game.Instance.EndGame();

    }
}
