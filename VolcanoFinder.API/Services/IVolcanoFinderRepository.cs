﻿using VolcanoFinder.API.Models.Entities;

namespace VolcanoFinder.API.Services
{
    public interface IVolcanoFinderRepository
    {
        Task<IEnumerable<Region>> GetRegionsAsync();
        Task<Region?> GetRegionAsync(int regionId, bool includeVolcanoes);
        Task<IEnumerable<Volcano>> GetVolcanoesFromRegionAsync(int regionId);
        Task<Volcano?> GetVolcanoFromRegionAsync(int regionId, int volcanoId);
    }
}
