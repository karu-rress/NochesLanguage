using NochesMLCore;
using System.Text;

namespace Noches_Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
            this.textBox1.KeyPress += new KeyPressEventHandler(CheckEnterKeyPress);
        }

        private void button1_Click(object o, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("문장을 입력하세요.", "프로그램 터진다", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GuessSender predict = new(textBox1.Text);
            StringBuilder sb = new();

            sb.AppendLine($"입력된 문장: {textBox1.Text}");
            sb.AppendLine();

            float max = predict.Scores.Max();
            sb.AppendLine(max switch {
                > 0.90f => $"⇒ 이건 빼박 {predict.SenderPrediction}의 말투입니다.",
                > 0.80f => $"⇒ 이건 {predict.SenderPrediction}의 말투일 확률이 매우 높습니다.",
                > 0.60f => $"⇒ 이건 {predict.SenderPrediction}의 말투일 확률이 높습니다.",
                _ => $"⇒ 이건 {predict.SenderPrediction}의 말투로 추측됩니다.",
            });

            foreach (var (sender, score) in predict.ScoreWithName)
                sb.AppendLine($"{sender}: {score * 100:0.00}%");

            if (string.IsNullOrEmpty(textBox2.Text))
                textBox2.Text = sb.ToString();
            else
            {
                StringBuilder sb2 = new();
                sb2.AppendLine();
                sb2.AppendLine("━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━");
                sb2.AppendLine();
                textBox2.AppendText(sb2.ToString());
                textBox2.AppendText(sb.ToString());
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = String.Empty;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                button1.PerformClick();
            }
        }
    }
}