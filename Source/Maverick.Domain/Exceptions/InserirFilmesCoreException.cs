using System.Runtime.Serialization;
using Otc.DomainBase.Exceptions;

namespace Maverick.Domain.Exceptions
{
    internal class InserirFilmesCoreException : CoreException<BuscarFilmesCoreError>
    {
        public InserirFilmesCoreException(
            BuscarFilmesCoreError buscarFilmesCoreError)
        {
            AddError(buscarFilmesCoreError);
        }

        protected InserirFilmesCoreException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        public override string Key => "BuscarFilmesCoreException";
    }

    public class InserirFilmesCoreError : CoreError
    {
        public static InserirFilmesCoreError LimiteDeRequisicoesAtingido =>
            new InserirFilmesCoreError("LimiteDeRequisicoesAtingido",
                "O limite de requisições ao banco foi atingido, " +
                "tente novamente mais tarde.");

        protected InserirFilmesCoreError(string key, string message)
            : base(key, message)
        {
        }
    }
}
