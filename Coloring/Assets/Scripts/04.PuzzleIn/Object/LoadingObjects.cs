using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LoadingObject
{
    public int x;
    public int y;
    public int rotate;
    public int objectType;
    public int lineColorTheme;

    public void Init(int _x, int _y, int _rotate, int _objectType)
    {
        x = _x;
        y = _y;
        rotate = _rotate;
        objectType = _objectType;
    }
}

[System.Serializable]
public class LoadingEmpty : LoadingObject
{
    public LoadingEmpty(EmptyTile empty)
    {

    }
}

[System.Serializable]
public class LoadingWall : LoadingObject
{
    public LoadingWall(WallTile empty)
    {

    }
}

[System.Serializable]
public class LoadingOutputer : LoadingObject
{
    public LoadingOutputer(Outputer outputer)
    {
        this.lineColorTheme = outputer.lineColorTheme;
    }

    public void ReInit()
    {

    }
}

[System.Serializable]
public class LoadingInputer : LoadingObject
{
    public LoadingInputer(Inputer inputer)
    {
        this.lineColorTheme = inputer.lineColorTheme;
    }

    public void ReInit()
    {

    }
}

[System.Serializable]
public class LoadingReflector : LoadingObject
{
    public LoadingReflector(Reflector reflector)
    {

    }
}

[System.Serializable]
public class LoadingInputerObject : LoadingObject
{
    public LoadingInputerObject(InputerObject inputerObject)
    {

    }
}
