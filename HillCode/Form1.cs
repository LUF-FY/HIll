using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HillCode
{
    public partial class Form1 : Form
    {
        private int n;
        private char[] alphabetDecrype;
        private Dictionary<char, int> alphabetEncrype = new Dictionary<char, int>();
        private string inputText;
        private int[,] matrixA;

        public Form1()
        {
            InitializeComponent();
            DGV.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            DGV.Height = 260;
            n = 3;

            for (int i = 0; i < n; i++)
            {
                DGV.Columns.Add("Column" + i.ToString(), "");
                DGV.Rows.Add();
                DGV.Rows[i].Height = DGV.Height / n - 1;
            }

            alphabetDecrype = "ZABCDEFGHIJKLMNOPQRSTUVWXY".ToCharArray();

            for (int i = 0; i < alphabetDecrype.Length; i++)
            {
                alphabetEncrype.Add(alphabetDecrype[i], i);
            }
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            GetMatrixA();
            inputText = textBoxInput.Text.ToUpper();

            while (inputText.Length % n != 0)
                inputText += "Z";

            HillEncrype(inputText);
        }

        private void HillEncrype(string s)
        {
            char[] input = s.ToCharArray();
            int t = s.Length / n;
            string res = "";

            for (int i = 0; i < t; i++)
            {
                var vector = new int[n];
                for (int j = 0; j < n; j++)
                {
                    vector[j] = alphabetEncrype[input[i * n + j]];
                }

                var codeVector = new int[n];
                codeVector = MatrixMultiplication(matrixA, vector);

                for (int j = 0; j < codeVector.Length; j++)
                {
                    res += alphabetDecrype[codeVector[j]];
                }
            }

            textBoxEncrypt.Text = res;
        }

        private int[] MatrixMultiplication(int[,] a, int[] b)
        {
            int[] res = new int[n];
            for (int i = 0; i < n; i++)
            {
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += (a[i, j] * b[j]) % alphabetDecrype.Length;
                }
                res[i] = sum % alphabetDecrype.Length;
            }
            return res;
        }

        public void GetMatrixA()
        {
            matrixA = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    matrixA[i, j] = int.Parse(DGV.Rows[i].Cells[j].Value.ToString());
        }
    }
}
