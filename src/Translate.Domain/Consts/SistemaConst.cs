namespace Translate.Domain.Consts;

public static class SistemaConst
{
    public const string NomeSistema = "Translate.API";
    public const int QtdLimiteMBsImport = 2 * 1_048_576;

    public static string GerarMensagemErroVazio(string prop)
    {
        return $"{prop} não pode ser nulo nem vazio";
    }
}