using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PuzzleObject : Object,
    IBeginDragHandler, IDragHandler, IEndDragHandler,
    IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private static bool _isMouseDragging = false;
    private static bool _isMousePressing = false;

    //다유동 변수
    private Ray _rayment;
    private int _xTemp;
    private int _yTemp;

    public void ObjectStatement()
    {
        ObjectSpritement();
        ObjectMovement();
        SaveObject();
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

        //GameObject tileGB = grid.tileMapArray[v3.x, v3.y];
        //GameObject objGB = grid.objectMapArray[v3.x, v3.y];

        //if (tileGB == null) return false;
        //else if (objGB == null) return true;
        //else if (objGB.GetComponent<Object>().objectType != 0) return true;
        //else if (tileGB.GetComponent<Object>().objectType != 0) return true;

        //return false;

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
        }
    }
    private void ObjectMovement()
    {
        Debug.Log("IsMoveablePosition");

        xReal = (x + (float)0.5) * grid.argumentedCellSize;
        yReal = (y + (float)0.5) * grid.argumentedCellSize;

        Vector3 realPos = new Vector3(xReal, yReal, z);
        GetComponent<RectTransform>().position = realPos;
    }
    private void ObjectSpritement()
    {
        Sprite img;

        if (!_isMousePressing)
            img = spriteNormal;
        else
            img = spriteSelect;

        GetComponent<Image>().sprite = img;
    }

    private void ObjectRotatement()
    {
        if (!_isMouseDragging)
        {
            if (++rotate == 4)
                rotate = 0;

            Quaternion quart;
            quart = Quaternion.Euler(0, 0, 90 * rotate);
            GetComponent<Transform>().rotation = quart;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ObjectRotatement();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _isMousePressing = true;
        MouseDown();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        _isMousePressing = false;
        MouseDown();
    }
    public void OnDrag(PointerEventData eventData)
    {
        ObjectPositionSetMouse();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _isMouseDragging = true;
        MouseDrag();
        Debug.Log("오브젝트 드래그 시작 ");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _isMouseDragging = false;
        MouseDrag();
        Debug.Log("오브젝트 드래그 종료");
    }

    private void MouseDrag()
    {

    }

    private void MouseDown()
    {
        ObjectSpritement();
        ZPositionSetting();
    }

    private void ZPositionSetting()
    {
        if (_isMousePressing)
        {
            transform.position = new Vector3(xReal, yReal, -1);
        }
        else
        {
            transform.position = new Vector3(xReal, yReal, 0);
        }
    }
}
