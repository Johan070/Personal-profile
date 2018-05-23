using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarioWorldRemake
{
    public class FileReader
    {
        /// <summary>
        /// reads the file and returns a 2d string array with the level contents
        /// </summary>
        /// <param name="file">the name of the to read file</param>
        /// <returns></returns>
        public string[,] Read(string file)
        {
            string[] fileText = File.ReadAllLines(file);

            int sizeY = File.ReadLines(file).Count();
            int sizeX = fileText[1].Split(new char[] { ',' }).Length-1;
            string[,] output = new string[sizeX, sizeY];
            for (int j = 0; j < sizeY; j++)
            {
                string[] col = fileText[j].Split(new char[] { ',' });
                sizeX = col.Length;
                for (int i = 0; i < sizeX-1; i++)
                {
                    try
                    {
                        output[i, j] = col[i];
                    }
                    catch(Exception e)
                    {
                        Console.Write("de file is niet juist geformat");
                    }

                }
            }

            return output;
        }

       
    }
}
