using ProjectRenamerTool.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectRenamerTool.Services
{
    public class RenameService
    {
        /// <summary>
		/// Replaces occurences of a string with another string in the specified file
		/// using a regular expression.
		/// </summary>
		/// <param name="file"></param>
		/// <param name="searchFor"></param>
		/// <param name="replaceWith"></param>
		private void Replace(string file, string searchFor, string replaceWith)
        {
            string fileContent;

            using (StreamReader reader = new StreamReader(file))
            {
                fileContent = reader.ReadToEnd();
            }

            // Use regular expressions to search and replace text
            fileContent = Regex.Replace(fileContent, searchFor, replaceWith);

            // Write modified file content back to file
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.Write(fileContent);
            }
        }

        private void ReplaceOccurencesInNameSpaces()
        {
            string[] files;
            string searchFor;
            string replaceWith;

            // Special treatment of C++ Projects
            if (context.OldProject.ProjectType.Equals(ProjectType.Cplusplus))
            {
                files = Directory.GetFiles(context.NewProject.ProjectDirectory, "*", SearchOption.AllDirectories);

                searchFor = string.Format("{0} {1} {2}", "namespace", context.OldProject.ProjectName, "{");
                replaceWith = string.Format("{0} {1} {2}", "namespace", context.NewProject.ProjectName, "{");

                foreach (string file in files)
                {
                    // Skip these files..
                    if (!file.EndsWith(".rc") && !file.EndsWith(".ico") && !file.EndsWith(".resX"))
                    {
                        Replace(file, searchFor, replaceWith);
                    }
                }

                return;
            }
        }
}
