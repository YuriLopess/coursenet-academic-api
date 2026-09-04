namespace CourseNet.Academic.Adapter.Driving.Api.Dtos;

/// <summary>
/// Dados de acesso enviados para autenticação de um aluno.
/// </summary>
/// <param name="Email">E-mail cadastrado do aluno.</param>
/// <param name="Senha">Senha em texto puro, verificada via hash no servidor.</param>
public record LoginRequest(string Email, string Senha);
