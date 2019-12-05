using CsLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaTool.Tools
{
    public class TemplateManager
    {
        string fullFileName = string.Empty;
        public TemplateManager(string fullFileName)
        {
            this.fullFileName = fullFileName;
            LoadTemplate();
        }

        public Dictionary<string, string> Templates { get; } = new Dictionary<string, string>();

        public void LoadTemplate()
        {
            try
            {
                Templates.Clear();
                string line = string.Empty;
                using (StreamReader file = new StreamReader(fullFileName))
                {
                    Templates.Clear();
                    while ((line = file.ReadLine()) != null)
                    {
                        try
                        {
                            if (!line.StartsWith(@"#") && !(line.Trim() == ""))
                            {
                                string[] lineValues = line.Split(
                                    new string[] { ":::" }
                                    , StringSplitOptions.None
                                    );

                                Templates.Add(
                                    lineValues[0].ToString()
                                    , lineValues[1].ToString()
                                );
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool UpdateTemplate(string key, string value)
        {
            bool isSuccess = false;
            try
            {
                if (Templates.ContainsKey(key))
                {
                    Templates[key] = TranString.EncodeBase64Unicode(value);
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                if (isSuccess)
                    SaveTemplate();
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        public bool InsertTemplate(string key, string value)
        {
            bool isSuccess = false;
            try
            {
                if (!Templates.ContainsKey(key))
                {
                    Templates.Add(key, TranString.EncodeBase64Unicode(value));
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                if (isSuccess)
                    SaveTemplate();
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }


        public bool DeleteTemplate(string key)
        {
            bool isSuccess = false;
            try
            {
                if (Templates.ContainsKey(key))
                {
                    Templates.Remove(key);
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
                if (isSuccess)
                    SaveTemplate();
            }
            catch (Exception)
            {
                throw;
            }
            return isSuccess;
        }

        private void SaveTemplate()
        {
            try
            {
                File.Delete(fullFileName);
                using (StreamWriter file = new StreamWriter(fullFileName))
                {
                    foreach (var a in Templates)
                    {
                        file.WriteLine("{0}:::{1}", a.Key, a.Value);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
