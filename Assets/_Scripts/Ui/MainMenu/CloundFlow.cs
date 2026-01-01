using System.Collections.Generic;
using UnityEngine;

public class CloundFlow : MonoBehaviour
{
    [SerializeField] private List<Sprite> cloundSprites = new List<Sprite>();
    private int index;
    private Vector3 startPosition;
    [SerializeField] private Vector3 offsetStart;

    private SpriteRenderer spr;
    
    private Vector3 endPosition;
    [SerializeField] private Vector3 offsetEnd;
    [SerializeField] private float speed;
    private bool isCanResetFlag;
    void Awake()
    {
        spr = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        index = 0;
        isCanResetFlag = true;

        startPosition = Camera.main.ViewportToWorldPoint(new Vector3(1,1,1)) + offsetStart;
        endPosition = Camera.main.ViewportToWorldPoint(new Vector3(0,1,1)) + offsetEnd;

        if(cloundSprites.Count != 0)
        {
            spr.sprite = cloundSprites[index];
        }
        this.transform.position = startPosition;
    }
    void Update()
    {
        if(this.transform.position.x <= endPosition.x && isCanResetFlag)
        {
            this.transform.position = startPosition;
            this.spr.sprite = cloundSprites[GetSpriteIndex()];
            isCanResetFlag = false;
        }
        MoveTheClound();
    }
    private void MoveTheClound()
    {
        if(!isCanResetFlag)
            isCanResetFlag = true;
        
        this.transform.Translate(new Vector3(-1,0,0) * speed * Time.deltaTime);
    }

    private int GetSpriteIndex()
    {
        index ++;
        if(index > cloundSprites.Count - 1)
            index = 0;
        return index;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(offsetStart + startPosition,.1f);
        Gizmos.DrawSphere(offsetEnd + endPosition,.1f);
    }
}
