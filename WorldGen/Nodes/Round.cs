using WorldGen.DataStructures;
using WorldGen.NoddleLib;
using WorldGen.NoddleLib.Attributes;
using System;

namespace WorldGen.Nodes
{
    public class RoundArrayNode : Node
    {
        [Input]
        public I1DDataReadOnly<double>? InputField;

        [Output]
        public I1DData<int>? RoundedField;

        public override void Process()
        {
            if (InputField == null)
                throw new InvalidOperationException("InputField is not set.");

            var data = InputField.MutableOfType<int>();

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (int)Math.Round(InputField[i]);
            }

            RoundedField = data;
        }
    }
}