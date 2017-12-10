using Prova.WCF.DataContracts;
using System.ServiceModel;

namespace Prova.WCF
{
    [ServiceContract]
    public interface IUsuarioService
    {

        [OperationContract]
        Result<Usuario> Autenticar(string email, string password);

        [OperationContract]
        Result<RetornoCadastroUsuario> CadastrarUsuario(Usuario usuario);

        [OperationContract]
        Result<RetornoCadastroUsuario> AlterarNomeUsuario(string nome, int usuarioId);

        [OperationContract]
        Result<RetornoCadastroUsuario> AlterarSenha(string senhaAntiga, string senhaNova, int usuarioId);

        [OperationContract]
        Result<RetornoCadastroUsuario> AdicionarCodigoRecuperacaoSenha(string codigo, int usuarioId);

        [OperationContract]
        Result<RetornoCadastroUsuario> ResetarSenha(string senha, string codigo, int usuarioId);
        
        [OperationContract]
        Result<Usuario> BuscarUsuarioPorEmail(string email);

        [OperationContract]
        bool RecuperarSenha(string codigo, int usuarioId);

        [OperationContract]
        bool ValidarCodigo(string codigo, string email);
    }       
}
