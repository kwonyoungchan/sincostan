using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotate : MonoBehaviour
{
    
    float rx=0;
    float ry=0;
    public float rotSpeed =200;
    float wheelValue=0;

    
    Vector3 nearPosition;
    Vector3 farPosition;
    void Start()
    {
        //카메라에 가장 가까운 위치(플레이어 위치를 기준좌표계로 삼는다.)
       nearPosition = transform.localPosition;
       

       
    }

    // Update is called once per frame
    void Update()
    {
        
        //1. 마우스의 입력값을 이용해서
        float mx=Input.GetAxis("Mouse X");
        float my=Input.GetAxis("Mouse Y");

        rx+=my*Time.deltaTime*rotSpeed;
        ry+=mx*Time.deltaTime*rotSpeed;
        //rx의 회전 각을 제한하고 싶다.
        rx = Mathf.Clamp(rx,-80,80);
        //카메라 정면기준(정면 기준이 주기적으로 바뀌므로 update) 
        //뒤쪽방향으로 5미터지점
        farPosition= nearPosition+transform.forward*-5;

        transform.eulerAngles=new Vector3(-rx,ry,0);
        //마우스 스크롤휠 값 업데이트
         wheelValue -= Input.GetAxis("Mouse ScrollWheel");
        //마우스 스크롤휠 값을 0과 1로 제한
         wheelValue=Mathf.Clamp(wheelValue,0,1.0f);
        //카메라 위치를 선형보간법으로 지정
        Vector3 camPosition=Vector3.Lerp(nearPosition,farPosition,wheelValue);
        //보간된 카메라위치를 현재위치로 지정
        transform.localPosition=camPosition;

        

    }
}
