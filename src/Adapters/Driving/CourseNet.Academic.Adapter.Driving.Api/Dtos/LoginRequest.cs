using System.ComponentModel.DataAnnotations;

namespace CourseNet.Academic.Adapter.Driving.Api.Dtos;

/// <summary>Dados de login do aluno.</summary>
public record LoginRequest(
    [Required(ErrorMessage = "E-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    string Email,

    [Required(ErrorMessage = "Senha é obrigatória.")]
    string Senha);
