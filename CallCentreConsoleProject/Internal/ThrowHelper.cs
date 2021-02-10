using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CallCentreConsoleProject.Internal
{
    internal static class ThrowHelper
    {
        public static void ThrowIfInvalidFilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                throw new ArgumentException(null, nameof(filePath));
            }
        }
    }
}
