using Translate.Domain.Consts;
using Translate.Domain.Enums;

namespace Translate.Domain.Entities;

public sealed class Frase
{
    public Frase(Guid id, string conteudo, IdiomasEnum idioma, DateTime data)
    {
        if (string.IsNullOrEmpty(conteudo))
        {
            throw new ArgumentException(SistemaConst.GerarMensagemErroVazio(nameof(conteudo)), nameof(conteudo));
        }

        Id = id;
        Conteudo = conteudo;
        Idioma = idioma;
        QtdCaracteres = conteudo.Length;
        Data = data;
    }

    public Guid Id { get; private set; }
    public string Conteudo { get; private set; }
    public IdiomasEnum Idioma { get; private set; }
    public int QtdCaracteres { get; private set; }
    public DateTime Data { get; private set; }
}