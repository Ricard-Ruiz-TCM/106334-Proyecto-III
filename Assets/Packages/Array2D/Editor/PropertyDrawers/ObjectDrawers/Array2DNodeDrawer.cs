using UnityEditor;

namespace Array2DEditor 
{
    [CustomPropertyDrawer(typeof(Array2DNodeEnum))]
    public class Array2DNodeDrawer : Array2DEnumDrawer<nodeType> {
    }
}
