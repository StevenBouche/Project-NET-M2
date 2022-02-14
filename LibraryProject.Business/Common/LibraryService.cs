using LibraryProject.Domain.Common;
using LibraryProject.Infrastructure.Repositories.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Business.Common
{
    public class LibraryService<T, K> : ILibraryService<T>
        where T : AuditableEntity
        where K : ILibraryRepository<T>
    {
        protected K Repository { get; }
        protected readonly ILogger Logger;

        public LibraryService(K repository, ILogger logger)
        {
            Repository = repository;
            Logger = logger;
        }
    }
}