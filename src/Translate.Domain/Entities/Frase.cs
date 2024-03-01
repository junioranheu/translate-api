using System.ComponentModel.DataAnnotations;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Validate;

namespace Translate.Domain.Entities;

public sealed class Frase
{
    public Frase(Guid id, string conteudo, IdiomasEnum idioma, DateTime data)
    {
        ValidarParamsEntidade(GetType().Name, [conteudo, idioma], nameof(conteudo), nameof(idioma));

        Id = id;
        Conteudo = conteudo;
        Idioma = idioma;
        QtdCaracteres = conteudo.Length;
        Data = data;
    }

    [Key]
    public Guid Id { get; private set; }

    public string Conteudo { get; private set; }

    public IdiomasEnum Idioma { get; private set; }

    public int QtdCaracteres { get; private set; }

    public DateTime Data { get; private set; }
}