using System;
using Translate.Domain.Enums;

namespace Translate.Domain.Handlers.CriarFrase.Commands.Requests;

public class CriarFraseRequest
{
    public string Conteudo { get; set; } = string.Empty;
    public IdiomasEnum Idioma { get; set; }
}