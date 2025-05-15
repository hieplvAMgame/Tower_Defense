using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AnimationCallbackHandle animHandle;
    [Button]
    public void PlayAnim(string name)
    {
        anim.Play(name);
    }
    private void Awake()
    {
        animHandle.AddEvent(AnimName.DYING, () => gameObject.SetActive(false));
    }
}
