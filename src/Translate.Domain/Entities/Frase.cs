using System;
using static Translate.Utils.Fixtures.Get;

namespace Translate.Domain.Entities;

public class Frase
{
    public Frase(string conteudo)
    {
        Id = Guid.NewGuid();
        Conteudo = conteudo;
        Data = GerarHorarioBrasilia();
    }

    public Guid Id { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime Data { get; private set; }
}