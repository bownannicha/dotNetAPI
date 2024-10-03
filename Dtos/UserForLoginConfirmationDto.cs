namespace DotnetAPI.Dtos
{
    // partial class is not public
    public partial class UserForLoginconfirmationDto
    {
        // byte array: byte[] similar to varbinary from db
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        UserForLoginconfirmationDto()
        {
            if (PasswordHash == null)
            {
                PasswordHash = new byte[0];
            }
            if (PasswordSalt == null)
            {
                PasswordSalt = new byte[0];
            }
        }
    }
}