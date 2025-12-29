using System.Collections.Generic;
using UnityEngine;

public class TagChangeColor : MonoBehaviour
{
     [SerializeField]List<GameObject> listObjs = new List<GameObject>();
    void Start()
    {
        ChangeColor();
    }
    private void ChangeColor()
    {
        foreach (GameObject obj in listObjs)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            
            if(obj.tag == "Red")
            {
                sr.color = Color.red;
            }
            else if(obj.tag == "Green")
            {
                sr.color = Color.green;
            }
            else if(obj.tag == "Blue")
            {
                sr.color = Color.blue;
            }
            else
            {
                sr.color = Color.white;
            }
        }
    }
    void Update()
    {
        
    }
}
