using System.ComponentModel;

namespace ForensicsAnalyzer.Domain.Enums;

public enum MethodType
{
    [Description("Full File System")]
    FFS = 0,
    Logical = 1,
    Backup = 2,
    Agent = 3,
}
