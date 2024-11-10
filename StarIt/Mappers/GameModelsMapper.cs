using StarIt.Models;
using StarIt.ViewModels;

namespace StarIt.Mappers;

public class GameModelsMapper
{
    public static GameModel MapGameViewModelToDataModel(GameViewModel viewModel)
    {
        return new GameModel
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
        };
    }
}