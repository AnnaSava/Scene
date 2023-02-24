namespace SavaDev.Base.User.Data.Models.Interfaces
{
    public interface IUserModel
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }
    }
}
