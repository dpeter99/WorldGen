using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;

namespace WorldGen.Nodes;

public enum MapType
{
    Exact,
    GreaterThan,
    SmallerThan,
}

public class MapValuesNode<TIn, TOut> : Node where TIn : notnull, IComparable<TIn>
{
    [Input] public I1DDataReadOnly<TIn> InputData;

    [Property] public Dictionary<TIn, TOut> MappingDictionary { get; set; }
    [Property] public MapType MapType;

    [Output] public I1DData<TOut> MappedData;

    public override void Process()
    {
        if (InputData == null)
        {
            throw new InvalidOperationException("Input data is not set.");
        }

        MappedData = InputData.MutableOfType<TOut>();

        for (int i = 0; i < InputData.Length; i++)
        {
            TIn originalValue = InputData[i];
            MappedData[i] = MapValue(originalValue);
        }
    }

    private TOut MapValue(TIn value)
    {
        KeyValuePair<TIn, TOut>? res = null;
        
        foreach (var option in MappingDictionary)
        {
            bool isBetterMatch = false;
            
            switch (MapType)
            {
                case MapType.GreaterThan:
                    if (option.Key.CompareTo(value) <= 0)
                        isBetterMatch = !res.HasValue || option.Key.CompareTo(res.Value.Key) > 0;
                    break;
                case MapType.SmallerThan:
                    if (option.Key.CompareTo(value) >= 0)
                        isBetterMatch = !res.HasValue || option.Key.CompareTo(res.Value.Key) < 0;
                    break;
            }
            
            if (isBetterMatch)
            {
                res = option;
            }
        }

        return res.HasValue ? res.Value.Value : default;
    }
}
