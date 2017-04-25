using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GOAPBaseAction : MonoBehaviour
{


    #region 不知道HashSet和KeyValuePair是什么鬼
    private HashSet<KeyValuePair<string, object>> preconditions;  //前置条件
    private HashSet<KeyValuePair<string, object>> effects;         //特效
    #endregion


    private bool inRange = false;//是否到了范围之内
    public float cost = 1f;      //代价权重
    public GameObject target;  //目标物体，对应的位置

    /**
     * 构造函数中进行初始化
    */
    public GOAPBaseAction() //构造函数中进行初始化
    {
        preconditions = new HashSet<KeyValuePair<string, object>>();
        effects = new HashSet<KeyValuePair<string, object>>();
    }

    /*
     * 重置值初始化对象
     */
    public void doReset() //重置值初始化对象
    {
        inRange = false;
        target = null;
        reset();
    }
    /**
    * 重置所有需要重置的变量，然后在开启计划
    */
    public abstract void reset();

    /**
     * 行动是否执行完成
     */
     
    public abstract bool isDone();
     
    /**
     * 检查程序的前置条件，是否满足
     */
    public abstract bool checkProceduralPrecondition(GameObject agent);


    /**
     * 执行动作，成功与否则返回True或false
     */
    public abstract bool perform(GameObject agent);


    /**
     * 这个动作是否需要在范围内被执行
     */
    public abstract bool requiresInRange();

    /*
     * 是否在范围内
     */

    public bool isInRange() 
    {
        return inRange;
    }

    /*
     * 设置范围
     */

    public void setInRange( bool inrange) 
    {
        this.inRange = inrange;
    }
    /*
     * 增加前置条件
     */
    public void addPrecondition(string key, object value) 
    {
        preconditions.Add(new KeyValuePair<string, object>(key, value));
    }

    /*
     * 移除前置条件
     */
    public void removePrecondition(string key) 
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string,object> kvp in preconditions)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }

        if (!default(KeyValuePair<string, object>).Equals(remove))
            preconditions.Remove(remove);
    }

    public void addEffect(string key, object value)
    {
        effects.Add(new KeyValuePair<string, object>(key, value));
    }


    public void removeEffect(string key)
    {
        KeyValuePair<string, object> remove = default(KeyValuePair<string, object>);
        foreach (KeyValuePair<string, object> kvp in effects)
        {
            if (kvp.Key.Equals(key))
                remove = kvp;
        }
        if (!default(KeyValuePair<string, object>).Equals(remove))
            effects.Remove(remove);
    }

    public HashSet<KeyValuePair<string, object>> Preconditions
    {
        get
        {
            return preconditions;
        }
    }

    public HashSet<KeyValuePair<string, object>> Effects
    {
        get
        {
            return effects;
        }
    }
}
