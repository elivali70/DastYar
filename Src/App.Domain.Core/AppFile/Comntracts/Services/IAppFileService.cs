using App.Domain.Core.AppFile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;


    public interface IAppFileService
    {
    Task<int> Add(AppFileDto appFileDto, CancellationToken cancellationToken);
}
