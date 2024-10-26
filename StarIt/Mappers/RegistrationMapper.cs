using StarIt.Models;
using StarIt.ViewModels;

namespace StarIt.Mappers;

public class RegistrationMapper
{
    public static UserModel MapUserViewModelToDataModel(UserViewModel userViewModel)
    {
        return new UserModel
        {
            Email = userViewModel.Email,
            Password = userViewModel.Password,
            Nickname = userViewModel.Nickname,
        };
    }
}