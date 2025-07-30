using Demo.Domain.Entities;
using Demo.Domain.Shared.Enums;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Demo.SqlSugarCore.DataSeeds
{
    public class LibraryStoreDataSeed : IDataSeedContributor, ITransientDependency
    {
        private ISqlSugarRepository<LibraryAggregateRoot> _LibraryRepository;
        private IGuidGenerator _guidGenerator;
        public LibraryStoreDataSeed(ISqlSugarRepository<LibraryAggregateRoot> repository, IGuidGenerator guidGenerator)
        {
            _LibraryRepository = repository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (!await _LibraryRepository.IsAnyAsync(x => true))
            {
                await _LibraryRepository.InsertAsync(
                    new LibraryAggregateRoot
                    {
                        BookId = _guidGenerator.Create(),
                        Name = "1984",
                        Location = "Main Library",
                        Stock = 1
                    },
                    autoSave: true
                );
                await _LibraryRepository.InsertAsync(
                    new LibraryAggregateRoot
                    {
                        BookId = _guidGenerator.Create(),
                        Name = "The Hitchhiker's Guide to the Galaxy",
                        Location = "Science Fiction Section",
                        Stock = 5
                    },
                    autoSave: true
                );
            }
        }


    }
}

