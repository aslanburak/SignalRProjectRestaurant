namespace SignalRWebUI.Dtos.IdentityDtos
{
    public class UserEditDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public String ConfirmPassword { get; set; }
    }
}
