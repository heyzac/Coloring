using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private GameObject outputer;
    private Outputer outputerComponent;
    private Vector3Int outputerPosV3Int;

    private int colorTheme;
    private Color lineColor;

    private LineRenderer lr;

    public GridScript grid;

    void Start()
    {
        outputer = transform.parent.gameObject;
        lr = GetComponent<LineRenderer>();
        outputerComponent = outputer.GetComponent<Outputer>();

        VariableInitialization();

        lineColor = ColorInitialization(colorTheme);
        lr.startColor = lineColor;

        //LineCarculation();
    }

    private void VariableInitialization()
    {
        colorTheme = 0;
    }

    private Color ColorInitialization(int colorNumber)
    {
        Color color;
        switch (colorNumber)
        {
            case 0:
                color = new Color(166, 166, 166);
                break;
            case 1:
                color = new Color(180, 199, 231);
                break;
            default:
                color = new Color(0, 0, 0);
                break;
        }

        return color;
    }

    /*private void LineCarculation()
    {
        outputerPosV3Int = new Vector3Int(outputerComponent.x, outputerComponent.y, outputerComponent.z);
        lr.positionCount = 1;
        lr.SetPosition(0, outputerPosV3Int);

        Vector3Int tmpV3Int = outputerPosV3Int;
        GameObject tmpObject;
        Object objScp;
        int i;

        switch (outputerComponent.rotate)
        {
            case 0:
                i = outputerComponent.x;
                do
                {
                    tmpV3Int = new Vector3Int(i, outputerComponent.y, 0);
                    tmpObject = grid.GetObjectWithPosition(tmpV3Int);
                    objScp = tmpObject.GetComponent<Object>();
                    i++;
                } while (objScp.objectType != 0);

                break;

            case 1:
                i = outputerComponent.y;
                do
                {
                    tmpV3Int = new Vector3Int(outputerComponent.x, i, 0);
                    tmpObject = grid.GetObjectWithPosition(tmpV3Int);
                    objScp = tmpObject.GetComponent<Object>();
                    i--;
                } while (objScp.objectType != 0);

                break;

            case 2:
                i = outputerComponent.x;
                do
                {
                    tmpV3Int = new Vector3Int(i, outputerComponent.y, 0);
                    tmpObject = grid.GetObjectWithPosition(tmpV3Int);
                    objScp = tmpObject.GetComponent<Object>();
                    i--;
                } while (objScp.objectType != 0);

                break;

            case 3:
                i = outputerComponent.y;
                do
                {
                    tmpV3Int = new Vector3Int(outputerComponent.x, i, 0);
                    tmpObject = grid.GetObjectWithPosition(tmpV3Int);
                    objScp = tmpObject.GetComponent<Object>();
                    i++;
                } while (objScp.objectType != 0);

                break;

            default:
                Debug.Log("범위 내의 로테이션 값이 아닙니다.");
                break;
        }
    }*/

    private bool isWorking(Vector3Int tmpV3Int, GameObject tmpObject, Object objScp)
    {

        return false;
    }
}
