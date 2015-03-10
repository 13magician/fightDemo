﻿using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {//主要玩家控制角色

    // Use this for initialization
    public float moveMaxForce=10;//玩家移动速度
    public float moveForce = 7;//移动时每次增加的力
    [HideInInspector]//属性不显示在Inspector。虽然不显示，但是仍然会取属性不显示在Inspector面板中的默认值···记得去掉查看/修改
    public Animator anim;
    private Rigidbody2D rigid;//角色的刚体组件
    private ActionState playState;//保存玩家的状态
    void Start () {
        anim = GetComponent<Animator>();
        rigid =GetComponent<Rigidbody2D>();
        playState = GetComponent<ActionState>();//角色的行动状态
        //添加技能组件
        //gameObject.AddComponent<Attack2>();//添加组件脚本，attack2。实际上控制技能2
        gameObject.AddComponent<Attack1>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playState.rightArrow && !playState.rightSide) Flip();//如果玩家按下右方向，并且面相不是右边。调用反转函数
        else if (playState.leftArrow && playState.rightSide) Flip();//否则判断如果玩家按下←方向，并且面相是右边。调用反转函数
        anim.SetBool("run", playState.rightArrow || playState.leftArrow);//按下左右任意一个都true

    }
    void FixedUpdate()
    {
        LRMove();//角色左右移动
         //遍历动画函数
        //Attack2();//技能
    }
    void LRMove()//左右移动
    {
        if (IsName("run"))//如果播放的是跑步状态
        {
            if (playState.leftArrow)
            {
                rigid.AddForce(new Vector2(-moveForce, 0));//给角色添加力
            }
            else if (playState.rightArrow)
            {
                rigid.AddForce(new Vector2(moveForce, 0));
            }
            if (Mathf.Abs(rigid.velocity.x) > moveMaxForce)//如果X轴力大于限制的速度。就设为最大速度
            {
                rigid.velocity = new Vector2(Mathf.Sign(rigid.velocity.x) * moveMaxForce, rigid.velocity.y);
            }
        }

    }

    bool IsName(string name)//判断当前播放的是否某个动画名称
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    void Flip()//面向反转函数
    {
        Vector3 vt3 = transform.localScale;
        vt3.x *= -1;
        transform.localScale = vt3;//修改父物体x缩放为反方向
       playState.rightSide = !playState.rightSide;//设置角色面相相反
    }
}