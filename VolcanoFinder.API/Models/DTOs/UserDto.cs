namespace VolcanoFinder.API.Models.DTOs
{
    /// <summary>
    /// A user DTO for authentication
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The name of the user
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The password of the user
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
