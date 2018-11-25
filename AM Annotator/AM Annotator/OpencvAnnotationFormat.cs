﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AM_Annotator
{
    class OpencvAnnotationFormat
    {
        private string output_directory_raw;
        public OpencvAnnotationFormat()
        {

        }
        public OpencvAnnotationFormat(string output_directory)
        {
            output_directory_raw = output_directory + "\\opencv_annotation\\";
            Directory.CreateDirectory(output_directory_raw);
        }
        public void Add(string image_location, FeatureLabel fl)
        {
            string potential_path = output_directory_raw + "class_" + fl.Id.ToString() + "\\annotation.txt";
            if (!File.Exists(potential_path))
            {
                Directory.CreateDirectory(output_directory_raw + "class_" + fl.Id.ToString());
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(potential_path, false))
                {
                    string textToWrite = image_location + " " + fl.X.ToString() + " " + fl.Y.ToString() + " " + fl.Width.ToString() + " " + fl.Height.ToString();
                    sw.WriteLine(textToWrite);
                }
            }
            else
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(potential_path, true))
                {
                    string textToWrite = image_location + " " + fl.X.ToString() + " " + fl.Y.ToString() + " " + fl.Width.ToString() + " " + fl.Height.ToString();
                    sw.WriteLine(textToWrite);
                }
            }
        }
        public void Add(AnnotationImage ai)
        {
            foreach (FeatureLabel fl in ai.getLabels())
            {
                string potential_path = output_directory_raw + "class_" + fl.Id.ToString() + "//annotation.txt";
                if (!File.Exists(potential_path))
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(potential_path, false))
                    {
                        string textToWrite = ai.getImageLocation() + " " + fl.X.ToString() + " " + fl.Y.ToString() + " " + fl.Width.ToString() + " " + fl.Height.ToString();
                        sw.WriteLine(textToWrite);
                    }
                }
                else
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(potential_path, true))
                    {
                        string textToWrite = ai.getImageLocation() + " " + fl.X.ToString() + " " + fl.Y.ToString() + " " + fl.Width.ToString() + " " + fl.Height.ToString();
                        sw.WriteLine(textToWrite);
                    }
                }
            }
            
        }
        public void remove(string image_location, FeatureLabel fl)
        {
            string potential_path = output_directory_raw + "class_" + fl.Id.ToString() + "\\annotation.txt";
            if (!File.Exists(potential_path))
            {
                return;
            }
            string search_str = image_location + " " + fl.X.ToString() + " " + fl.Y.ToString() + " " + fl.Width.ToString() + " " + fl.Height.ToString();
            //File.ReadLines(potential_path).SkipWhile(line => !line.Contains(temp)).Skip(1).TakeWhile(line => !line.Contains(temp));
            var logFile = File.ReadAllLines(potential_path);
            var logList = new List<string>(logFile);
            List<string> annotations = new List<string>();

            foreach (var log in logList)
            {
                if (log == search_str)
                {
                    continue;
                }
                annotations.Add(log);
            }

            File.WriteAllLines(potential_path, annotations.ToArray());

        }

    }
}