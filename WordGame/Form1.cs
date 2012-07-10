using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WordGame
{
    public partial class Form1 : Form
    {
        WordAnalyzer wrdAnalizer;
        Label[] ansLabels;
        bool fileOpen;
        int questLength;
        int wordLoc;
        int choiceAns;
        int correctAns;
        public Form1()
        {
            InitializeComponent();
            wrdAnalizer = new WordAnalyzer();
            ansLabels = new Label[4];
            ansLabels[0] = labelAns1;
            ansLabels[1] = labelAns2;
            ansLabels[2] = labelAns3;
            ansLabels[3] = labelAns4;
            fileOpen = false;
        }


        /// <summary>
        /// Enters the information from a .csv/.txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        /// <summary>
        /// Advances to next question
        /// </summary>
        public void NextQuestion()
        {
            if (wordLoc < questLength)
            {
                Random roller = new Random();
                List<int> taken = new List<int>();
                int temp;
                correctLabel.Text = String.Empty;
                ansLabels[choiceAns].ForeColor = Color.White;
                choiceAns = 0;
                ansLabels[0].ForeColor = Color.SteelBlue;
                wordLabel.Text = wrdAnalizer.quest[wordLoc];
                correctAns = roller.Next(4);
                taken.Add(wordLoc);
                ansLabels[correctAns].Text = wrdAnalizer.answers[wordLoc];
                for (int i = 0; i < 4; i++)
                {
                    if (i != correctAns)
                    {
                        do
                        {
                            temp = roller.Next(questLength);
                        } while (taken.Contains(temp));
                        taken.Add(temp);
                        ansLabels[i].Text = wrdAnalizer.answers[temp];
                    }
                }
            }
            else
            {
                MessageBox.Show("Finished File!");
                fileOpen = false;
                wordLoc = 0;
                questLength = 0;
            }
        }
        private void ChangeFile()
        {
            wrdAnalizer.Empty();
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileOpen = true;
                StreamReader sr = new StreamReader(dialog.OpenFile());
                String line;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    wrdAnalizer.AddData(line);
                }
            }
            questLength = wrdAnalizer.Length;
            NextQuestion();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (fileOpen)
            {
                if (e.KeyData == Keys.Up && choiceAns > 0)
                {
                    ansLabels[choiceAns].ForeColor = Color.White;
                    choiceAns--;
                    ansLabels[choiceAns].ForeColor = Color.SteelBlue;
                }
                else if (e.KeyData == Keys.Down && choiceAns < 3)
                {
                    ansLabels[choiceAns].ForeColor = Color.White;
                    choiceAns++;
                    ansLabels[choiceAns].ForeColor = Color.SteelBlue;
                }
                else if (e.KeyData == Keys.A || e.KeyCode == Keys.Enter)
                {
                    if (choiceAns == correctAns)
                    {
                        correctLabel.Text = "נכון!";
                        correctLabel.ForeColor = Color.Aqua;
                    }
                    else if (choiceAns != -1)
                    {
                        correctLabel.Text = "לא נכון";
                        correctLabel.ForeColor = Color.Red;
                    }
                }
                else if (e.KeyData == Keys.S)
                {
                    wordLoc++;
                    NextQuestion();
                }
            }
            if (e.KeyData == Keys.W)
            {
                ChangeFile();
            }
        }
    }
}
