using MediatR;
using Translate.Application.Commands.Frases.ListarFrase;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Entities;
using Translate.Domain.Enums;
using Translate.Infrastructure.Repositories.Frases;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Frases.ListarFrase;

public class ListarFraseHandler(IFraseRepository repository) : IRequestHandler<ListarFraseRequest, List<ListarFraseResponse>>
{
    private readonly IFraseRepository _repository = repository;

    public async Task<List<ListarFraseResponse>> Handle(ListarFraseRequest command, CancellationToken cancellationToken)
    {
        var entidade = new Frase(
            fraseOriginal: command.FraseOriginal,
            idiomaOriginal: command.IdiomaOriginal,
            fraseTraduzida: command.FraseTraduzida,
            idiomaTraduzido: command.IdiomaTraduzido
        );

        IEnumerable<Frase> linq = await _repository.Listar(entidade) ?? throw new Exception(ObterDescricaoEnum(CodigoErroEnum.NaoEncontrado));

        List<ListarFraseResponse> list = [];

        var resultUsuario = new ObterUsuarioResponse();

        foreach (var item in linq)
        {
            if (item.Usuarios is not null)
            {
                resultUsuario = new ObterUsuarioResponse()
                {
                    UsuarioId = item.Usuarios.UsuarioId,
                    NomeCompleto = item.Usuarios.NomeCompleto,
                    NomeUsuarioSistema = item.Usuarios.NomeUsuarioSistema,
                    Email = item.Usuarios.Email,
                    IsAtivo = item.Usuarios.IsAtivo,
                    Data = item.Usuarios.Data,
                    UsuarioRoles = item.Usuarios.UsuarioRoles
                };
            }

            var result = new ListarFraseResponse()
            {
                FraseId = item.FraseId,
                FraseOriginal = item.FraseOriginal,
                IdiomaOriginal = item.IdiomaOriginal,
                FraseTraduzida = item.FraseTraduzida,
                IdiomaTraduzido = item.IdiomaTraduzido,
                Usuarios = resultUsuario,
                Data = item.Data
            };

            list.Add(result);
        }

        return list;
    }
}