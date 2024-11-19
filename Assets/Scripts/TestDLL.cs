using System.Runtime.InteropServices;
using UnityEngine;

public class TestDLL : MonoBehaviour
{
    [DllImport("MyPlagin")]
    private static extern int MyFunction(int a, int b);

    void Start()
    {
        int result = MyFunction(5, 3);
        Debug.Log("Result from DLL: " + result);
    }
}
