using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Line : MonoBehaviour
{
    private GridScript grid;
    private GameObject outputer;
    private Outputer outputerComponent;

    private int _lineDirect;
    private bool _isLineEnd;
    private int _lineColorTheme;
    private Color _lineColor;
    private List<Vector3Int> _linePointList = new List<Vector3Int>();
    private List<Vector3> _lineRealPointList = new List<Vector3>();

    private LineRenderer lineRenderer;

    private void Awake()
    {
        EventInitialization();
    }
    private void Start()
    {

    }
    public void OnEnable()
    {
        Initialization();
    }
    private void OnDestroy()
    {
        PuzzleIn.EventObjectLoad -= this.Pointment;
        PuzzleObject.EventObjectMove -= this.Pointment;
    }

    private void Initialization()
    {
        outputer = transform.parent.gameObject;
        lineRenderer = GetComponent<LineRenderer>();
        outputerComponent = outputer.GetComponent<Outputer>();
        grid = outputerComponent.grid;
    }
    private void VariableInitialization()
    {
        _lineColorTheme = outputerComponent.lineColorTheme;
        _lineDirect = outputerComponent.rotate;
        _isLineEnd = false;

        _lineColor = ColorInitialization(_lineColorTheme);
        lineRenderer.startColor = _lineColor;
        lineRenderer.endColor = _lineColor;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;

        _linePointList.Clear();
        _lineRealPointList.Clear();
        lineRenderer.positionCount = 0;
    }
    private void EventInitialization()
    {
        PuzzleIn.EventObjectLoad += this.Pointment;
        PuzzleObject.EventObjectMove += this.Pointment;
    }
    private Color ColorInitialization(int colorTheme)
    {
        Color color;
        switch (colorTheme)
        {
            //default
            case 0:
                color = new Color(166, 166, 166);
                break;
            //blue
            case 1:
                color = new Color(0.7058824f, 0.7803922f, 0.9058824f);
                break;
            //green
            case 2:
                color = new Color(0.6509804f, 0.8705883f, 0.6352941f);
                break;
            //yellow
            case 3:
                color = new Color(0.7058824f, 0.7803922f, 0.9058824f);
                break;
            //red
            case 4:
                color = new Color(0.7058824f, 0.7803922f, 0.9058824f);
                break;
            //error
            default:
                Debug.LogWarning(colorTheme + " is not allowed color number!");
                color = new Color(0, 0, 0);
                break;
        }

        return color;
    }

    public void Pointment()
    {
        VariableInitialization();

        Vector3Int startPoint = GetStartPoint(outputerComponent.GetPosition());
        _linePointList.Add(startPoint);
        _lineRealPointList.Add(GetLineRealPointment(startPoint));

        for (int i = 1; !_isLineEnd; i++)
        {
            _linePointList.Add(GetAdditionPoint());
            _lineRealPointList.Add(GetLineRealPointment(_linePointList[i]));
        }

        _lineRealPointList[_lineRealPointList.Count - 1] = GetEndPoint();

        for (int i = 0; i < _lineRealPointList.Count; i++)
        {
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(i, _lineRealPointList[i]);
        }
    }

    private Vector3Int GetStartPoint(Vector3Int outputerPoint)
    {
        _lineDirect = outputerComponent.rotate;
        Vector3Int startLinePoint = outputerPoint;
        return startLinePoint;
    }
    private Vector3Int GetAdditionPoint()
    {
        Vector3Int prevLinePoint = _linePointList[_linePointList.Count - 1];

        GameObject tileObject;
        GameObject puzzleObject;
        Vector3Int objectPoint;

        Vector2Int increase = GetIteratorValuement(prevLinePoint);

        for (int i = increase.x; ;)
        {
            i += increase.y;
            objectPoint = GetCompareObjectPosition(prevLinePoint, i);
            tileObject = grid.GetTileWithPosition(objectPoint);
            puzzleObject = grid.GetObjectWithPosition(objectPoint);

            if (IsLineSkewerable(tileObject, puzzleObject))
                continue;
            else {; }

            if (tileObject == null)
            {
                Debug.LogWarning("objectPoint : " + objectPoint + " 's tileObject is Null");
                LineEnd(null);
                return new Vector3Int();
            } else {; }

            LineCarculate(tileObject);
            if (puzzleObject != null)
                LineCarculate(puzzleObject);

            break;
        }

        return objectPoint;
    }
    private Vector3 GetEndPoint()
    {
        Vector3 endRealPoint = _lineRealPointList[_lineRealPointList.Count - 1];

        switch (_lineDirect)
        {
            case 0:
                endRealPoint.x -= (grid.argumentedCellSize) / 2f;
                break;
            case 1:
                endRealPoint.y += (grid.argumentedCellSize) / 2f;
                break;
            case 2:
                endRealPoint.x += (grid.argumentedCellSize) / 2f;
                break;
            case 3:
                endRealPoint.y -= (grid.argumentedCellSize) / 2f;
                break;
            case -1:
                Debug.LogWarning("The Line already has been end");
                break;
        }

        return endRealPoint;
    }
    private Vector3 GetLineRealPointment(Vector3Int v3)
    {
        Vector3 RealPoint = new Vector3(v3.x * grid.argumentedCellSize + PuzzleIn.gridAdjusment,
            v3.y * grid.argumentedCellSize + PuzzleIn.gridAdjusment, 0);
        return RealPoint;
    }
    private Vector3Int GetCompareObjectPosition(Vector3Int prevPos, int iterator)
    {
        switch (_lineDirect)
        {
            case 0:
                goto case 2;
            case 2:
                return new Vector3Int(iterator, prevPos.y, 0);

            case 1:
                goto case 3;
            case 3:
                return new Vector3Int(prevPos.x, iterator, 0);

            default:
                Debug.LogWarning("Not Allowed LineDirection");
                return new Vector3Int(0, 0, 0);
        }
    }
    private Vector2Int GetIteratorValuement(Vector3Int prevLinePoint)
    {
        Vector2Int v2 = new Vector2Int(0, 1);

        switch (_lineDirect)
        {
            case 0:
                v2.x = prevLinePoint.x;
                v2.y = 1;
                break;

            case 1:
                v2.x = prevLinePoint.y;
                v2.y = -1;
                break;

            case 2:
                v2.x = prevLinePoint.x;
                v2.y = -1;
                break;

            case 3:
                v2.x = prevLinePoint.y;
                v2.y = 1;
                break;

            default:
                Debug.LogWarning("Not Allowed LineDirection");
                break;
        }

        return v2;
    }

    private void LineEnd(GameObject obj)
    {
        _isLineEnd = true;
    }
    private void LineCarculate(GameObject obj)
    {
        switch (obj.GetComponent<Object>().objectType)
        {
            //WallTile
            case 1:
                LineEnd(obj);
                break;

            //Outputer
            case 2:
                obj.GetComponent<Outputer>().Output();
                LineEnd(obj);
                break;

            //Inputer
            case 3:
                if(obj.GetComponent<Inputer>().lineColorTheme == _lineColorTheme &&
                    obj.GetComponent<Inputer>().rotate == grid.RotateOverflow(_lineDirect + 2))
                {
                    obj.GetComponent<Inputer>().Input();
                    LineEnd(obj);
                    break;
                }
                LineEnd(obj);
                break;

            //Reflector
            case 4:
                if (obj.GetComponent<Reflector>().Reflect(_lineDirect) == -1)
                {
                    LineEnd(obj);
                    break;
                } else {; }

                _lineDirect = obj.GetComponent<Reflector>().Reflect(_lineDirect);
                obj.GetComponent<Reflector>().SpriteChangeFromLine(_lineColorTheme);
                break;

            //InputerObject
            case 5:

                break;
        }
    }
    private bool IsLineSkewerable(GameObject tileObject, GameObject puzzleObject)
    {
        if (tileObject == null)
            return false;
        else if (tileObject.GetComponent<Object>().objectType != 0)
            return false;
        else if (puzzleObject == null)
            return true;

        return false;
    }
}
