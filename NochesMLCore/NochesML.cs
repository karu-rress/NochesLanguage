using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Enumerable;

namespace NochesMLCore
{
    public class GuessSender
    {
        public string Message { get; set; }
        public ChatModel.ModelInput Input { get; set; }
        public ChatModel.ModelOutput Output { get; set; }
        public string SenderPrediction { get; set; }
        public List<(string sender, float score)> ScoreWithName { get; set; } = new();
        public float[] Scores { get; set; }
        public GuessSender(string msg)
        {
            Message = msg;
            Input = new() { Col1 = msg };
            Output = ChatModel.Predict(Input);


            SenderPrediction = Output.Prediction;
            Scores = Output.Score;
            var names = GetSlotNames(ChatModel.PredictEngine.Value.OutputSchema, "Score");
            foreach (int i in Range(0, Scores.Length))
            {
                ScoreWithName.Add((names[i], Scores[i]));
            }
        }

        private static List<string> GetSlotNames(DataViewSchema schema, string name)
        {
            var column = schema.GetColumnOrNull(name);

            var slotNames = new VBuffer<ReadOnlyMemory<char>>();
            column.Value.GetSlotNames(ref slotNames);
            var names = new string[slotNames.Length];
            var num = 0;
            foreach (var denseValue in slotNames.DenseValues())
            {
                names[num++] = denseValue.ToString();
            }

            return names.ToList();
        }
    }


}
