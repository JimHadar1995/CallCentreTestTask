using System;

namespace CallCentreConsoleProject.Models
{
    public sealed record CallCentreRecord(DateTime DateFrom, DateTime DateTo, string ProjectName, string Operator, string State, long Duration);
}
