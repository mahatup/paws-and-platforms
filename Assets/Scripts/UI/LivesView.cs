using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Heart
{
    public GameObject Whole;
    public GameObject Broken;
}
public class LivesView : MonoBehaviour
{
    private readonly List<Heart> _hearts = new();

    public void AddHeart(GameObject whole, GameObject broken)
    {
        Heart heart = new Heart { Whole = whole, Broken = broken };
        heart.Whole.SetActive(true);
        heart.Broken.SetActive(false);
        _hearts.Add(heart);
    }
   
    public void BreakLastHeart()
    {
        if (_hearts.Count == 0) return;

        Heart last = _hearts.Last();

        if (last.Whole)
        { 
            last.Whole.SetActive(false); 
        }

        if (last.Broken)
        { 
            last.Broken.SetActive(true); 
        }

        _hearts.Remove(last);
    }
}
