using System.ComponentModel;

namespace Translate.Domain.Enums;

public enum IdiomasEnum
{
    [Description("-")]
    Default = 0,

    [Description("Português")]
    BR = 1,

    [Description("Inglês")]
    EN = 2, 

    [Description("Espanhol")]
    ES = 3  
}