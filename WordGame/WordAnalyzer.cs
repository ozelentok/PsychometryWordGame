using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordGame
{
    class WordAnalyzer
    {
        public List<String> quest
        {   get; private set; }
        public List<String> answers
        {   get; private set; }
        public int Length
        {   get; private set; }

        public WordAnalyzer()
        {
            quest = new List<String>();
            answers = new List<String>();
            Length = 0;
        }

        /// <summary>
        /// Adds from a string in this format[question, answer]
        /// </summary>
        /// <param name="dataRow">Line including a question and an answer</param>
        public void AddData(String dataRow)
        {
            int loc = dataRow.IndexOf(',');
            if (loc != -1)
            {
                quest.Add(dataRow.Substring(0, loc));
                answers.Add(dataRow.Substring(loc + 1));
                Length++;
            }
        }
        /// <summary>
        /// Empties the word bank
        /// </summary>
        public void Empty()
        {
            quest.RemoveRange(0, Length);
            answers.RemoveRange(0, Length);
            Length = 0;
        }
    }
}
