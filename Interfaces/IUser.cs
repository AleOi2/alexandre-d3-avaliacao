using alexandre_d3_avaliacao.Models;

namespace alexandre_d3_avaliacao.Interfaces
{
    public interface IUser
    {
        //Create Read Update Delete - CRUD

        List<User> ReadAll();

        void Create(User newUser);

        void Update(User user);

        void Delete(string idProduct);

        User ValidateUser(string email, string password);

    }
}
