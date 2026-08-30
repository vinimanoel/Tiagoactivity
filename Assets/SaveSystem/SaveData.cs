using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int fase;

    public bool checkpointAtivado;

    public float checkpointX;
    public float checkpointY;
    public float checkpointZ;

    public int moedasNoCheckpoint;

    public List<string> moedasColetadas =
        new List<string>();
}