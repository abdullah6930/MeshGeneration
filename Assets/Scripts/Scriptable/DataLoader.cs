using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataLoader", menuName = "ScriptableObjects/DataLoader", order = 1)]
public class DataLoader : ScriptableObject
{
    #region Singleton
    private static DataLoader instance = null;
    public static DataLoader Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<DataLoader>("DataLoader");
            return instance;
        }
    }
    #endregion

    public GameObject X_AxisGizmos, Y_AxisGizmos, Z_AxisGizmos;

    [Header("TAGS")]
    public string DEFAULT_GIZMOS;
    public string X_AXIS_GIZMOS , Y_AXIS_GIZMOS, Z_AXIS_GIZMOS;
}
