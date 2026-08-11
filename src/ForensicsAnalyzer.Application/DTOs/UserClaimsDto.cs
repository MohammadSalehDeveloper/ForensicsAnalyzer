namespace ForensicsAnalyzer.Application.DTOs;

public record UserClaimsDto(
    string UserId,
    string Username,
    string Email,
    List<string> Roles
);
