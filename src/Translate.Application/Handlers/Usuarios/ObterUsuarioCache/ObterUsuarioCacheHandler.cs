using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Entities;
using Translate.Infrastructure.Repositories.Usuarios;

namespace Translate.Application.Handlers.Usuarios.ObterUsuarioCache;

public class ObterUsuarioCacheHandler(IMemoryCache memoryCache, IUsuarioRepository repository) : IRequestHandler<ObterUsuarioRequest, ObterUsuarioResponse>
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly IUsuarioRepository _repository = repository;

    public async Task<ObterUsuarioResponse> Handle(ObterUsuarioRequest command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command.Email))
        {
            return new ObterUsuarioResponse();
        }

        string keyCache = $"key_ObterUsuarioCacheHandler_{command.Email}";
        if (!_memoryCache.TryGetValue(keyCache, out ObterUsuarioResponse? response))
        {
            var entidade = new Usuario(
                usuarioId: command.UsuarioId,
                email: command.Email
            );

            var linq = await _repository.Obter(entidade);

            if (linq is null)
            {
                return new ObterUsuarioResponse();
            }

            response = new ObterUsuarioResponse()
            {
                UsuarioId = linq.UsuarioId,
                NomeCompleto = linq.NomeCompleto,
                NomeUsuarioSistema = linq.NomeUsuarioSistema,
                Email = linq.Email,
                IsAtivo = linq.IsAtivo,
                Data = linq.Data,
                UsuarioRoles = linq.UsuarioRoles
            };

            _memoryCache.Set(keyCache, response, TimeSpan.FromMinutes(1));
        }

        return response ?? new ObterUsuarioResponse();
    }
}