using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    void Start()
    {
       anim =  GetComponent<Animator>();
    }

   
    public void DodgeAnim()
    {
        ARDebugManager.Instance.LogInfo("dodge");
        anim.SetBool("dodge", true);
        Invoke("ReturnAnim", 1.433f);
        //leantween.delayedcall(1.2f, Return);
    }

    public void JumpAnim()
    {
        ARDebugManager.Instance.LogInfo("jump");
        anim.SetBool("jump", true);
        Invoke("ReturnAnim", 2.433f);
    }

    public void UppercutAnim()
    {
        ARDebugManager.Instance.LogInfo("uppercut");
        anim.SetBool("uppercut", true);
        Invoke("ReturnAnim", 1.2f);
    }

    public void DanceAnim()
    {
        ARDebugManager.Instance.LogInfo("dance");
        anim.SetBool("dance", true);
        Invoke("ReturnAnim", 21.8f);
    }

    public void AttackAnim()
    {
        ARDebugManager.Instance.LogInfo("attack");
        anim.SetBool("attack", true);
        Invoke("ReturnAnim", 2.7f);
    }

    public void ReturnAnim()
    {
        anim.SetBool("dodge", false);
        anim.SetBool("jump", false);
        anim.SetBool("uppercut", false);
        anim.SetBool("dance", false);
        anim.SetBool("attack", false);
    }

    public void AnimationController(RuntimeAnimatorController animation)
    {
        anim.runtimeAnimatorController = animation;
        
    }
}
