
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class fx_animationevents : MonoBehaviour
{
    public UnityEvent OnHurtPlayer;
    public void HurtPlayer(AnimationEvent evt)
    {
        print("event launched");
        if (evt.animatorClipInfo.weight > 0.5)
        {
            OnHurtPlayer.Invoke();
        }
    }
}
