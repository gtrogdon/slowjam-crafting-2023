using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.Analytics;

public class CitySunScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Color[] ColorsOfDay;
    public float[] IntensitiesOfDay;

    public float Timer;
    public float TimerLength;
    public int Index;

    Light DirLight;
    void Start()
    {
        DirLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer > TimerLength)
        {
            Timer -= TimerLength;
        }
        Timer += Time.deltaTime;
        getColor();
        getIntensity();
    }
    void getColor()
    {
        float ratioToNextColor = (Timer % (TimerLength / ColorsOfDay.Length)) / (TimerLength / ColorsOfDay.Length);
        //Debug.Log(ratioToNextColor);
        Index = (int)((Timer / TimerLength) * ColorsOfDay.Length);
        Color ActiveColor = ColorsOfDay[Index];
        Color NextColor = ColorsOfDay[(Index + 1) % ColorsOfDay.Length];
        //Debug.Log((Index + 1) % ColorsOfDay.Length);
        Vector3 dif = new Vector3(ActiveColor.r - NextColor.r, ActiveColor.g - NextColor.g, ActiveColor.b - NextColor.b);
        //Debug.Log(dif.ToString());
        dif *= ratioToNextColor;
        //Debug.Log(dif.ToString());
        DirLight.color = new Color(ActiveColor.r - dif.x, ActiveColor.g - dif.y, ActiveColor.b - dif.z);
        //Debug.Log(ActiveColor);
        //Debug.Log(DirLight.color);
        //Debug.Log(NextColor);
        /*
        float depth = ((Timer % (TimerLength / ColorsOfDay.Length)) / (TimerLength / ColorsOfDay.Length));
        Index = (int)(Timer / TimerLength * ColorsOfDay.Length);
        Debug.Log(depth);
        Color activeColor = ColorsOfDay[Index];
        Color nextColor = ColorsOfDay[(Index + 1) % ColorsOfDay.Length];
        Color dif = new Color((activeColor.r - nextColor.r)/depth, (activeColor.g - nextColor.g) / depth, (activeColor.b - nextColor.b) / depth, (activeColor.a - nextColor.a) / depth);
        DirLight.color = new Color(activeColor.r - dif.r, activeColor.g - dif.g, activeColor.b - dif.b, activeColor.a - dif.a);
        *///DirLight.color = 
        //Debug.Log(" ");
    }
    void getIntensity()
    {
        float ratioToNextintensity = (Timer % (TimerLength / IntensitiesOfDay.Length)) / (TimerLength / IntensitiesOfDay.Length);
        //Debug.Log(ratioToNextintensity);
        Index = (int)((Timer / TimerLength) * IntensitiesOfDay.Length);
        float ActiveIntensity = IntensitiesOfDay[Index];
        float NextIntensity = IntensitiesOfDay[(Index + 1) % IntensitiesOfDay.Length];
        //Debug.Log((Index + 1) % ColorsOfDay.Length);
        float dif = ActiveIntensity - NextIntensity;// new Vector3(ActiveColor.r - NextColor.r, ActiveColor.g - NextColor.g, ActiveColor.b - NextColor.b);
        //Debug.Log(dif.ToString());
        dif *= ratioToNextintensity;
        //Debug.Log(dif.ToString());
        DirLight.intensity = ActiveIntensity - dif;//new Color(ActiveColor.r - dif.x, ActiveColor.g - dif.y, ActiveColor.b - dif.z);
        //Debug.Log(ActiveColor);
        //Debug.Log(DirLight.color);
        //Debug.Log(NextColor);
    }
}
