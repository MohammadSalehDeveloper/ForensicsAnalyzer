using System.ComponentModel;

namespace ForensicsAnalyzer.Domain.Enums;

public enum SourceType
{
    [Description("Celebrite Physical Analyzer")]
    Celeberite = 0,
    [Description("Fapna Mesbah Analyzer")]
    Fapna = 1,
    [Description("Oxygen Analyzer")]
    Oxygen = 2,
    [Description("Customize Export Data")]
    Custom = 3,
}