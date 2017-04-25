using UnityEngine;
using System.Collections;

public class GOAPToolComponent : MonoBehaviour {

    public float Strength;
	void Start () {
        Strength = 1;
	}
    //使用工具
    public void use(float damage) 
    {
        Strength -= damage;
    }
    //销毁工具
    public bool destroyed() 
    {
        return Strength <= 0;
    }
}
