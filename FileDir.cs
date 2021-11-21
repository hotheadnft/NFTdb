using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace NFTdb_init
{
    class FileHelper
    {
        public static List<string> GetFilesRecursive(string b)
        {

            //All files found get added here. This gets passed back to main 
            List<string> result = new List<string>();

            // All the directories get added here.  Process these until all dirs have been read
            Stack<string> stack = new Stack<string>();

            // Add initial directory.
            stack.Push(b);

            // Continue while there are directories to process
            while (stack.Count > 0)
            {

                // Get top directory
                
                string dir = stack.Pop();
         
                try
                {
                    // Add all files at this directory to the files List (result).
                    result.AddRange(Directory.GetFiles(dir, "*.*"));



                    // Add all directories to stack
                    foreach (string dn in Directory.GetDirectories(dir))
                    {
                        stack.Push(dn);
                    }
                }
                catch(IOException msg)
                {
                    Console.WriteLine(msg.ToString());
                    // Could not open the directory
                }
            }
            return result;
        }
    }
}
