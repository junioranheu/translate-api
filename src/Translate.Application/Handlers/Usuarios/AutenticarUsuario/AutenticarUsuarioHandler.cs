using MediatR;
using Translate.Application.Commands.Usuarios.AutenticarUsuario;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.Application.Handlers.Usuarios.AutenticarUsuario;

public sealed class AutenticarUsuarioHandler : IRequestHandler<AutenticarUsuarioRequest, AutenticarUsuarioResponse>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IObterUsuarioCondicaoArbitrariaUseCase _obterUsuarioCondicaoArbitrariaUseCase;

    public AutenticarUsuarioHandler(IJwtTokenGenerator jwtTokenGenerator, IObterUsuarioCondicaoArbitrariaUseCase obterUsuarioCondicaoArbitrariaUseCase)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _obterUsuarioCondicaoArbitrariaUseCase = obterUsuarioCondicaoArbitrariaUseCase;
    }

    public async Task<AutenticarUsuarioResponse> Handle(AutenticarUsuarioRequest command, CancellationToken cancellationToken)
    {
        var (usuario, senha) = await _obterUsuarioCondicaoArbitrariaUseCase.Execute(command?.Login ?? string.Empty);
        AutenticarUsuarioOutput? output = _map.Map<AutenticarUsuarioOutput>(usuario);
        string senhaCriptografada = senha;

        if (output is null)
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.UsuarioNaoEncontrado));
        }

        if (!VerificarCriptografia(senha: command?.Senha ?? string.Empty, senhaCriptografada: senhaCriptografada))
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.UsuarioSenhaIncorretos));
        }

        if (!output.IsAtivo)
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.ContaDesativada));
        }

        output!.Token = _jwtTokenGenerator.GerarToken(nomeCompleto: output.NomeCompleto!, email: output.Email!, listaClaims: null);
        output = await GerarRefreshToken(_jwtTokenGenerator, _criarRefreshTokenUseCase, output, output.UsuarioId);

        return output;
    }
}