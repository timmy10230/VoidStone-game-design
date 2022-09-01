using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemColdDown 
{

    public  float coldTime = 2.0f;//定义技能的冷却时间
    public  float timer = 0;//定义计时器
    public  Image filledImage;//定义一个填充图片，下面从start方法里获取实例中的填充图片
    public  bool isStartTime = false;//定义标志量，决定是否开始计时
    // Use this for initialization
    void Start()
    {
       //filledImage = transform.Find("FillImage").GetComponent<Image>();//用transform中的Find方法获取名为FillImage物体中Image组件；
    }

    // Update is called once per frame
    void Update()
    {
        /////////////////////////////////////////////
        /////////////////////////////////////////////
      /*if (Input.GetKeyDown(KeyCode.Alpha1))//当按下键盘上的数字1键，触发技能
            isStartTime = true;
        if (isStartTime)//当开始计时时执行下列代码
        {
            timer += Time.deltaTime;
            filledImage.fillAmount = (coldTime - timer) / coldTime;//按照时间比例显示图片，刚开始点击时，timer小，应该显示的背景图大
            if (timer >= coldTime)//判断是否达到冷却时间
            {
                filledImage.fillAmount = 0;//冷却图不显示
                timer = 0;//重置计时器
                isStartTime = false;//结束计时
            }
        }*/

    }
    /*public void OnShow()//定义一个点击按妞触发的方法
    {
        isStartTime = true;//当点击时开始计时；
    }*/

    public void ColdDown()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1) && timer==0)//当按下键盘上的数字1键，触发技能
        isStartTime = true;
        if (isStartTime)//当开始计时时执行下列代码
        {
            while (timer < coldTime)
            {
                timer += Time.deltaTime;
                filledImage.fillAmount = (coldTime - timer) / coldTime;//按照时间比例显示图片，刚开始点击时，timer小，应该显示的背景图大
            }
            if (timer >= coldTime)//判断是否达到冷却时间
            {
                filledImage.fillAmount = 0;//冷却图不显示
                timer = 0;//重置计时器
                isStartTime = false;//结束计时
            }
        }
    }
} 


