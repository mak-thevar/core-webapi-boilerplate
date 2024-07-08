namespace CoreWebApiBoilerPlate.Application.DTO.Response
{
    public record UserResponseDTO(int Id, string Name, string Username, string? RoleDescription, bool IsActive);
}
