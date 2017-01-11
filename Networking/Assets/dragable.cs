using UnityEngine;
using UnityEngine.EventSystems;

public class dragable : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler {
    public Transform parentToReturnTo = null;
     
	public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        parentToReturnTo = this.transform.parent;
        this.transform.SetParent(this.transform.parent.parent);
       this.GetComponent<CanvasGroup>().blocksRaycasts = false;
       
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        Debug.Log("OnDrag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        this.transform.SetParent(parentToReturnTo);
        this.GetComponent<CanvasGroup>().blocksRaycasts = true ;
        Debug.Log("OnEndDrag");
    }
}
