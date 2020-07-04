using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IOnLine
{
    void OnLine(int colorTheme);
}

public interface IInputer
{
    int SoundTheme { get; }
    bool IsActive { get; }

    void CompleteRequirementAddition();
    void Input();
    void SoundAddition();
}
