using UnityEngine;
using UnityEngine.EventSystems;

public class dropzone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    int x = 0;
    public int max;
   
    public void OnDrop(PointerEventData eventData)
    {
       Debug.Log (eventData.pointerDrag.name +"was dropped on " + gameObject.name);
        dragable d = eventData.pointerDrag.GetComponent<dragable>();
        if (d != null){
            x = this.transform.childCount;
            if (x < max)
            {
                d.parentToReturnTo = this.transform;
                
            }
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("OnExit " );

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       Debug.Log("OnEnter");

    }
    // Use this for initialization
    
}
