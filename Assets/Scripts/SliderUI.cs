using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SliderUI : MonoBehaviour {

    public Image image;
    [SerializeField] private float updateVaule;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        image.fillAmount += updateVaule * Time.deltaTime;
        if (image.fillAmount == 1) image.color = Color.blue;
        else image.color = Color.red;
    }
}
