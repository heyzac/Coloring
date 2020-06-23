using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleObject : Object,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public delegate void LineStatement();
    public static event LineStatement OutputerLining;

    //다유동 변수
    private Ray _rayment;
    private int _xTemp;
    private int _yTemp;

    //스프라이트 값 관련
    [SerializeField] protected Sprite spriteNormal;
    [SerializeField] protected Sprite spriteSelect;
    [SerializeField] protected Sprite spriteOnLine;

    protected void ObjectStatement()
    {
        EventInitialization();
        ObjectSpriteInit();
        ObjectMovement();

        SaveObject();
    }

    private void ObjectSpriteInit()
    {
        GetComponent<Image>().sprite = spriteNormal;
    }

    private void EventInitialization()
    {
        PuzzleIn.LineBegin += ObjectSpriteInit;
    }

    private void SaveObject()
    {
        grid.objectList.Add(gameObject);
        grid.elementList.Add(gameObject);
    }

    private bool IsMoveablePosition(Vector3Int v3)
    {
        GameObject ob = grid.GetObjectWithPosition(v3);
        GameObject tb = grid.GetTileWithPosition(v3);

        if (tb == null)
            return false;
        else if (tb.GetComponent<Object>().objectType == 0 && ob == null)
            return true;
        //else if ()        //예외 처리 추가 예정
        //    return true;

        return false;
    }
    private void ObjectPositionSetMouse()
    {
        _rayment = Camera.main.ScreenPointToRay(Input.mousePosition);
        _xTemp = tilemap.WorldToCell(_rayment.origin).x;
        _yTemp = tilemap.WorldToCell(_rayment.origin).y;

        if (_xTemp != x || _yTemp != y)
        {
            if(!IsMoveablePosition(new Vector3Int(_xTemp, _yTemp, 0)))
                return;

            x = _xTemp;
            y = _yTemp;

            ObjectMovement();
            return;
        }
    }

    private void ObjectMovement()
    {
        xReal = (x * grid.argumentedCellSize) + PuzzleIn.gridAdjusment;
        yReal = (y * grid.argumentedCellSize) + PuzzleIn.gridAdjusment;

        Vector3 realPos = new Vector3(xReal, yReal, z);
        GetComponent<RectTransform>().position = realPos;
    }
    public void ObjectSpritement()
    {
        Sprite img;

        if (!PuzzleIn.isMousePressing)
            img = spriteNormal;
        else
            img = spriteSelect;

        GetComponent<Image>().sprite = img;
    }
    private void ObjectRotatement()
    {
        if (!PuzzleIn.isMouseDragging)
        {
            rotate = grid.RotateOverflow(++rotate);
            GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, -90 * rotate);
        }
    }
    private void ZPositionSetting()
    {
        if (PuzzleIn.isMousePressing)
        {
            z = 1;
            GetPosition();
        }
        else
        {
            z = 0;
            GetPosition();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ObjectRotatement();
        MouseClick();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PuzzleIn.isMousePressing = true;
        MouseClick();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        PuzzleIn.isMousePressing = false;
        MouseClick();
    }
    public void OnDrag(PointerEventData eventData)
    {
        ObjectPositionSetMouse();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        PuzzleIn.isMouseDragging = true;
        MouseDrag();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        PuzzleIn.isMouseDragging = false;
        MouseDrag();
    }
    private void MouseDrag()
    {

    }
    private void MouseClick()
    {
        ObjectSpritement();
        ZPositionSetting();

        PuzzleIn.Lines();
        OutputerLining();
    }
}
