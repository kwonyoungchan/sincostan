using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 사용자의 입력에따라 앞뒤좌우로 이동하고싶다.

    //-크기
    public float speed =5;
    public float gravity =-9.81f;
    public float jumpPower =10;
    float yVelocity;
    float h=0,v=0;
    
    CharacterController cc;
    

    public enum MoveStates
    {
        Nomal,
        Climb,
        Exit
    }
    public MoveStates state;
    float currentTime=0;
    
    
    
    void Start()
    {
        cc=GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir=Vector3.zero;
        //1. y속도에 중력을 계속 더하고 싶다.
        yVelocity+=gravity*Time.deltaTime;
        //2. 만약 사용자가 점프버튼을 누르면 y속도에 뀌는힘을 대입하고 싶다.
        if(Input.GetButtonDown("Jump"))
        {
            yVelocity=jumpPower;
        }
        if(state==MoveStates.Nomal)
        {   
            h =Input.GetAxis("Horizontal");
            v =Input.GetAxis("Vertical");
            dir = Vector3.right*h+Vector3.forward*v;
            dir=Camera.main.transform.TransformDirection(dir);
            dir.Normalize();
            dir.y=yVelocity;

        }

        

         if(state==MoveStates.Climb)
         {
            v =Input.GetAxis("Vertical");
            dir= new Vector3(0,v,0);

            
         }

         if(state==MoveStates.Exit)
         {
             StartCoroutine("endClimb");
         }
        
         
         cc.Move(speed*dir*Time.deltaTime);
         
    }

    IEnumerator endClimb()
    {
        Vector3 Pos = transform.position;
        while(true)
        {
            currentTime += Time.deltaTime;
            if(currentTime<1f)
            {
                Pos+= jumpPower*2*Vector3.up*currentTime;
                yield return null;
            }
            else
            {
                state=MoveStates.Nomal;
                yield break;
            }
            
        }
        
        
        
    }
}
