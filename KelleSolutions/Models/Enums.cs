// File: Enums.cs
using System;

namespace KelleSolutions.Models {
    public enum OperatorEnum : short {
        Operator1 = 0,
        Operator2 = 1
        // Additional operators as needed
    }

    public enum OriginatorEnum : short {
        Originator1 = 0,
        Originator2 = 1
        // Additional originators as needed
    }

    public enum TeamEnum : short {
        TeamA = 0,
        TeamB = 1
        // Additional teams as needed
    }

    public enum VisibilityEnum : byte {
        Low = 0,
        Medium = 1,
        High = 2
        // Additional visibility levels as needed
    }
}
