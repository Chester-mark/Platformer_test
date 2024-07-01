using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int fruits = 0 ;

    [SerializeField] private Text fruitsCounter;

    private void Update()
    {
        fruitsCounter.text = "Fruits: " + fruits.ToString();
    }
}

