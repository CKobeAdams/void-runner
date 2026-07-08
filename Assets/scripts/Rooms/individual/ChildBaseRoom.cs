using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBaseRoom : ParentRoom
{
    public override string roomName
    {
        get { return "Base";  }
    }
}
