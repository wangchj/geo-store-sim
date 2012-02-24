using System;
using System.Collections.Generic;
using System.IO;

namespace ResultReporting
{
    public class ExperimentResults<T>
    {
        private string id;
        private Dictionary<string, List<T>> results;
        private string currentCat;

        public ExperimentResults(string id)
        {
            this.id = id;
            results = new Dictionary<string, List<T>>();
        }

        public void AddCategory(string cat)
        {
            results.Add(cat, new List<T>());
            currentCat = cat;
        }

        public void AddResult(T result)
        {
            results[currentCat].Add(result);
        }

        public void OutputResult()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(id + ".txt");
                bool first = true;

                //Output the column header.
                foreach (string cat in results.Keys)
                {
                    if (first) first = false;
                    else writer.Write('\t');
                    
                    writer.Write(cat);
                }
                writer.WriteLine();

                //Output results
                int trials = results[currentCat].Count;

                for (int i = 0; i < trials; i++)
                {
                    first = true;
                    foreach (string cat in results.Keys)
                    {
                        if (first) first = false;
                        else writer.Write('\t');
                        writer.Write(results[cat][i]);
                    }
                    writer.WriteLine();
                }
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        //public void AddResult(string cat, T result)
        //{
        //    if (!results.ContainsKey(cat))
        //        AddCategory(cat);
        //    results[cat].Add(result);
        //}

        public string CurrentCategory
        {
            get { return currentCat; }
            set { currentCat = value; }
        }

        public IEnumerable<string> Categories
        {
            get { return results.Keys; }
        }

        public List<T> this[string cat]
        {
            get { return results[cat]; }
        }

    }
}
