using App.Domain.Core.AppFile.Dtos;

public interface IAppFileAppService
{
    Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken);
}
