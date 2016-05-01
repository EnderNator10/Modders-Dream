using UnityEngine;
using System.Collections;
using System.Reflection;
using System;
using System.IO;
namespace AngelScript
{
    public class Angel
    {

        public string[] names;
        public void ScanClasses()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().ToString().EndsWith("_AngelFunc"))
                {
                    int i = 0;
                    foreach (string name in names)
                    {
                        i++;
                    }
                    names[i] = assembly.GetName().ToString();
                }
            }
        }
        public object Parser(string code)
        {
            string[] code_splitted = code.Split(" ".ToCharArray());
            if (code.EndsWith(";"))
            {
                if (code.StartsWith("int"))
                {
                    string var_name = code_splitted[1];
                    int var_content = int.Parse(code_splitted[2]);
                    return var_content;
                }
                if (code.StartsWith("str"))
                {
                    string var_name = code_splitted[1];
                    string var_content = code_splitted[2];
                    return var_content;
                }


            }
            if (code.StartsWith("if"))
            {
                if (code.EndsWith("then"))
                {
                    string var_name = code_splitted[2];
                }
                else
                {
                    return null;
                }
            }
            if (code.StartsWith("csfunc"))
            {
                Assembly.GetExecutingAssembly().CreateInstance(code_splitted[1] + "_AngelFunc");
                ScanClasses();
            }
            return null;
        }
        public void DoStr(string code)
        {
            Parser(code);
        }
        public void DoFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    foreach (string code in File.ReadAllLines(path))
                    {
                        Parser(code);
                    }
                }
            }
            catch
            {
                Exception e = new Exception("Code File Not Found!");

            }
        }
    }
}