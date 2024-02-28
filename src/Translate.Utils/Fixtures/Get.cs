using System;
using TimeZoneConverter;

namespace Translate.Utils.Fixtures;

public static class Get
{
    /// <summary>
    /// Obtém o horário atual, forçando ao horário de Brasilia;
    /// </summary>
    public static DateTime GerarHorarioBrasilia()
    {
        TimeZoneInfo timeZone = TZConvert.GetTimeZoneInfo("E. South America Standard Time");
        return TimeZoneInfo.ConvertTime(DateTime.UtcNow, timeZone);
    }

    /// <summary>
    /// Detalha em texto a data e hora atual;
    /// </summary>
    public static string ObterDetalhesDataHora()
    {
        DateTime horarioBrasilia = GerarHorarioBrasilia();
        return $"{horarioBrasilia:dd/MM/yyyy} às {horarioBrasilia:HH:mm:ss}";
    }
}