using Translate.Domain.Enums;
using static Translate.Utils.Fixtures.Get;

namespace Translate.Domain.Entities;

public sealed class Frase(string conteudo, IdiomasEnum idioma)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Conteudo { get; private set; } = conteudo;
    public IdiomasEnum Idioma { get; private set; } = idioma;
    public int QtdCaracteres { get; private set; } = conteudo.Length;
    public DateTime Data { get; private set; } = GerarHorarioBrasilia();
}