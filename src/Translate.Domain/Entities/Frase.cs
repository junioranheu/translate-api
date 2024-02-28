using System;
using static Translate.Utils.Fixtures.Get;
using Translate.Domain.Enums;

namespace Translate.Domain.Entities;

public class Frase
{
    public Frase(string conteudo, IdiomasEnum idioma)
    {
        Id = Guid.NewGuid();
        Conteudo = conteudo;
        Idioma = idioma;
        QtdCaracteres = conteudo.Length;
        Data = GerarHorarioBrasilia();
    }

    public Guid Id { get; private set; }
    public string Conteudo { get; private set; }
    public IdiomasEnum Idioma { get; private set; }
    public int QtdCaracteres { get; private set; }
    public DateTime Data { get; private set; }
}