using CallCentreConsoleProject.Models;
using System.Collections.Generic;

namespace CallCentreConsoleProject.Internal.Comparers
{
    public class DateToRecordComparer : IComparer<CallCentreRecord>
    {
        public int Compare(CallCentreRecord? x, CallCentreRecord? y)
        {
            if (x == null && y != null)
            {
                return -1;
            }
            if (x != null && y == null)
            {
                return 1;
            }
            if (ReferenceEquals(x, y) || x == null && y == null)
            {
                return 0;
            }

            return x!.DateTo.CompareTo(y!.DateTo);
        }
    }
}
